using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using ExceptionReflector.ILReader;

using ExceptionReflector.Config;

namespace ExceptionReflector
{
    public static class ExceptionEnumerator
    {
        private static bool bypassOpCodeExceptions = false;
        
        private static void Setup(ExceptionHierarchy exceptionTypes)
        {
            Type[] tst = new Type[] {
                                            typeof(System.Exception),
                                            typeof(SystemException),
                                            typeof(ApplicationException),
                                            typeof(System.IO.InternalBufferOverflowException),
                                            typeof(ExecutionEngineException),
                                            typeof(OutOfMemoryException),
                                            typeof(InsufficientMemoryException),
                                            typeof(StackOverflowException),
                                            typeof(BadImageFormatException),
                                            typeof(EntryPointNotFoundException),
                                            typeof(MissingFieldException),
                                            typeof(MissingMemberException),
                                            typeof(MissingMethodException),
                                            typeof(NotSupportedException),
                                            typeof(TypeLoadException),
                                            typeof(TypeUnloadedException),                                                  
                                    };

            bypassOpCodeExceptions = ExceptionReflectorSettings.BypassOpCodeExceptions;

            foreach(Type tp in ExceptionReflectorSettings.ForbidenExceptionTypes)
                exceptionTypes.AddForbidenException(tp);
        }

        public static ReadOnlyCollection<Type> GetAllExceptions(this MethodBase method)
        {
            //var exceptionTypes = new HashSet<Type>();
            ExceptionHierarchy exceptionTypes = new ExceptionHierarchy();
            Setup(exceptionTypes);

            var visitedMethods = new HashSet<MethodBase>();
            var localVars = new Type[ushort.MaxValue];
            var stack = new Stack<Type>();
            var protectedContexts = new ProtectedContexts();

            GetAllExceptions(protectedContexts, method, exceptionTypes, visitedMethods, localVars, stack, 0);

            protectedContexts.Clear();
            stack.Clear();
            visitedMethods.Clear();

            return exceptionTypes.GetMinimalCatchTypeList().AsReadOnly();
        }

        public static bool HasAnyExceptions(this MethodBase method)
        {
            var visitedMethods = new HashSet<MethodBase>();
            var localVars = new Type[ushort.MaxValue];
            var stack = new Stack<Type>();

            //TODO: [Backlog] Melhorar isso. Criar forma de sar do método assim que encontra a primeira exception.
            return GetAllExceptions(method).Any();
        }

        //TODO: [Backlog] No futuro, para ter registro de quem chamou quem e quem disparou que exception e acelerar mais, voltar com a idéa do CallingGraph, mas de forma a cachear o resultado dos métodos já visitados (não poderá ser mais uma árvore. Terá que ser um grafo mesmo).
        private static void GetAllExceptions(ProtectedContexts protectedContexts, MethodBase method, ExceptionHierarchy exceptionTypes, HashSet<MethodBase> visitedMethods, Type[] localVars, Stack<Type> stack, int depth)
        {
            if (method != null)
            {
                var ilReader = new ILReader.ILReader(method);
                ILInstruction[] allInstructions = ilReader.ToArray();

                MethodBody mtdBody = method.GetMethodBody();
                if (mtdBody != null)
                {
                    IList<ExceptionHandlingClause> exHndlClauses = mtdBody.ExceptionHandlingClauses;
                    if (exHndlClauses != null)
                    {
                        protectedContexts.AddProtectedContexts
                            (
                                exHndlClauses.Where
                                    (
                                            exHndl
                                        =>
                                                exHndl.Flags == ExceptionHandlingClauseOptions.Clause
                                            &&
                                                !allInstructions.Where(i => i.OpCode.Value == -486).Select<ILInstruction, int>(opc => opc.Offset).Where
                                                    (
                                                            rethrowOffset
                                                        =>
                                                                rethrowOffset >= exHndl.HandlerOffset
                                                            &&
                                                                rethrowOffset <= (exHndl.HandlerOffset + exHndl.HandlerLength)
                                                    ).Any()
                                    )
                            );
                    }
                }

                for (int i = 0; i < allInstructions.Length; i++)
                {
                    ILInstruction instruction = allInstructions[i];

                    //Adiciona todas as exceptions que a própria CLR pode gerar ao executar a instrução
                    if(!bypassOpCodeExceptions)
                        foreach (Type exTp in instruction.OpCode.Throws())
                            if (!exceptionTypes.Contains(exTp))
                                if (!protectedContexts.IsProtected(exTp, i))
                                    exceptionTypes.AddThrownException(exTp);
                        
                    var inlineMethodInstruction = instruction as InlineMethodInstruction;
                    if (inlineMethodInstruction != null)
                    {
                        InlineMethodInstruction methodInstruction = inlineMethodInstruction;

                        if (!visitedMethods.Contains(methodInstruction.Method))
                        {
                            visitedMethods.Add(methodInstruction.Method);
                            GetAllExceptions
                                                (
                                                        protectedContexts
                                                    ,
                                                        methodInstruction.Method
                                                    ,
                                                        exceptionTypes
                                                    ,
                                                        visitedMethods
                                                    ,
                                                        localVars
                                                    ,
                                                        stack
                                                    ,
                                                        depth + 1
                                                );
                        }

                        MethodBase curMethod = methodInstruction.Method;
                        if (curMethod is ConstructorInfo)
                        {
                            stack.Push(curMethod.DeclaringType);
                        }
                        else if (method is MethodInfo)
                        {
                            stack.Push(((MethodInfo)curMethod).ReturnParameter.ParameterType);
                        }
                    }
                    else
                    {
                        var inlineFieldInstruction = instruction as InlineFieldInstruction;
                        if (inlineFieldInstruction != null)
                        {
                            InlineFieldInstruction fieldInstruction = inlineFieldInstruction;
                            stack.Push(fieldInstruction.Field.FieldType);
                        }
                        else if (instruction is ShortInlineBrTargetInstruction)
                        {
                        }
                        else if (instruction is InlineBrTargetInstruction)
                        {
                        }
                        else
                        {
                            switch (instruction.OpCode.Value)
                            {
                                // ld*
                                case 0x06:
                                    stack.Push(localVars[0]);
                                    break;
                                case 0x07:
                                    stack.Push(localVars[1]);
                                    break;
                                case 0x08:
                                    stack.Push(localVars[2]);
                                    break;
                                case 0x09:
                                    stack.Push(localVars[3]);
                                    break;
                                case 0x11:
                                    {
                                        var index = (ushort)allInstructions[i + 1].OpCode.Value;
                                        stack.Push(localVars[index]);
                                        break;
                                    }
                                // st*
                                case 0x0A:
                                    localVars[0] = stack.Pop();
                                    break;
                                case 0x0B:
                                    localVars[1] = stack.Pop();
                                    break;
                                case 0x0C:
                                    localVars[2] = stack.Pop();
                                    break;
                                case 0x0D:
                                    localVars[3] = stack.Pop();
                                    break;
                                case 0x13:
                                    {
                                        var index = (ushort)allInstructions[i + 1].OpCode.Value;
                                        localVars[index] = stack.Pop();
                                        break;
                                    }
                                // throw
                                case 0x7A:
                                    if (stack.Peek() == null)
                                    {
                                        //System.Diagnostics.Debugger.Break();
                                        break;
                                    }
                                    if (!typeof(Exception).IsAssignableFrom(stack.Peek()))
                                    {
                                        //para debug
                                        //var ops = allInstructions.Select(f => f.OpCode).ToArray();

                                        //Este condicional foi feito pensando em C#, que apenas permite throw de tipos derivados de exception. 
                                        //Mas a MSIL permite throw de qualquer tipo de objeto. Como uma dependência pode não ter sido feita em C#, passamos a permitir tipos não derivados de exception...
                                        //break;
                                    }
                                    if (!exceptionTypes.Contains(stack.Peek()))
                                    {
                                        if (!protectedContexts.IsProtected(stack.Peek(), i))
                                            exceptionTypes.AddThrownException(stack.Pop());
                                        else
                                        {
                                            stack.Pop();
                                            //System.Diagnostics.Debugger.Break();
                                        }
                                    }
                                    else
                                        stack.Pop();
                                    break;
                                default:
                                    switch (instruction.OpCode.StackBehaviourPop)
                                    {
                                        case StackBehaviour.Pop0:
                                            break;
                                        case StackBehaviour.Pop1:
                                        case StackBehaviour.Popi:
                                        case StackBehaviour.Popref:
                                        case StackBehaviour.Varpop:
                                            if (stack.Count > 0)
                                            {
                                                stack.Pop();
                                            }
                                            break;
                                        case StackBehaviour.Pop1_pop1:
                                        case StackBehaviour.Popi_pop1:
                                        case StackBehaviour.Popi_popi:
                                        case StackBehaviour.Popi_popi8:
                                        case StackBehaviour.Popi_popr4:
                                        case StackBehaviour.Popi_popr8:
                                        case StackBehaviour.Popref_pop1:
                                        case StackBehaviour.Popref_popi:
                                            stack.Pop();
                                            stack.Pop();
                                            break;
                                        case StackBehaviour.Popref_popi_pop1:
                                        case StackBehaviour.Popref_popi_popi:
                                        case StackBehaviour.Popref_popi_popi8:
                                        case StackBehaviour.Popref_popi_popr4:
                                        case StackBehaviour.Popref_popi_popr8:
                                        case StackBehaviour.Popref_popi_popref:
                                            stack.Pop();
                                            stack.Pop();
                                            stack.Pop();
                                            break;
                                    }

                                    switch (instruction.OpCode.StackBehaviourPush)
                                    {
                                        case StackBehaviour.Push0:
                                            break;
                                        case StackBehaviour.Push1:
                                        case StackBehaviour.Pushi:
                                        case StackBehaviour.Pushi8:
                                        case StackBehaviour.Pushr4:
                                        case StackBehaviour.Pushr8:
                                        case StackBehaviour.Pushref:
                                        case StackBehaviour.Varpush:
                                            stack.Push(null);
                                            break;
                                        case StackBehaviour.Push1_push1:
                                            stack.Push(null);
                                            stack.Push(null);
                                            break;
                                    }

                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}