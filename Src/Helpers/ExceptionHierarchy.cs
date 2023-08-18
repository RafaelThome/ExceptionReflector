using System;
using System.Collections.Generic;
using System.Linq;

namespace ExceptionReflector
{
    public class ExceptionHierarchy
    {
        private TypeHierarchy _hierarchyGraph = new TypeHierarchy();
        private HashSet<Type> _forbidenExceptions = new HashSet<Type>();
        private HashSet<Type> _alreadyIncludedExceptions = new HashSet<Type>();

        public ExceptionHierarchy() { }

        public bool Contains(Type exceptionType)
        {
            return _alreadyIncludedExceptions.Contains(exceptionType);
        }

        public void AddForbidenException(Exception forbidenException)
        {
            AddForbidenException(forbidenException.GetType());
        }
        public void AddForbidenException(Type forbidenExceptionType)
        {
            //Este condicional foi feito pensando em C#, que apenas permite throw de tipos derivados de exception. 
            //Mas a MSIL permite throw de qualquer tipo de objeto. Como uma dependência pode não ter sido feita em C#, passamos a permitir tipos não derivados de exception...
            //if (typeof(Exception).IsAssignableFrom(forbidenExceptionType))
                _forbidenExceptions.Add(forbidenExceptionType);
            //else
            //    throw new ArgumentException("forbidenExceptionType must (directly or not) inherit Exception", "forbidenExceptionType");
        }

        //TODO: [Backlog] Criar um Dicionary<Type, List<string>> que associe cada exception a todas as pilhas e instruções que podem dispará-la. Retornar essas pilhas para a interface para que em cada catch elas apareçam comentadas. Isso ajuda a decidir como e se uma exception deve ser tratada.
        public void AddThrownException(Exception thrownException)
        {
            AddThrownException(thrownException.GetType());
        }
        public void AddThrownException(Type thrownExceptionType)
        {
            if (!_alreadyIncludedExceptions.Contains(thrownExceptionType))
            {
                _alreadyIncludedExceptions.Add(thrownExceptionType);
                //Este condicional foi feito pensando em C#, que apenas permite throw de tipos derivados de exception. 
                //Mas a MSIL permite throw de qualquer tipo de objeto. Como uma dependência pode não ter sido feita em C#, passamos a permitir tipos não derivados de exception...
                //if (typeof(Exception).IsAssignableFrom(thrownExceptionType))
                _hierarchyGraph.Root.AddSubType(thrownExceptionType);
                //else
                //    throw new ArgumentException("thrownExceptionType must (directly or not) inherit Exception", "thrownExceptionType");
            }
        }

        public List<Type> GetMinimalCatchTypeList()
        {
            return GetMinimalCatchTypeList(_hierarchyGraph.Root);
        }
        private List<Type> GetMinimalCatchTypeList(TypeHierarchy.Node node)
        {
            List<Type> ret = new List<Type>();
            if (_forbidenExceptions.Contains(node.InternalType))
                foreach (TypeHierarchy.Node n in node.GetSubTypes())
                    ret.AddRange(GetMinimalCatchTypeList(n));
            else
                ret.Add(node.InternalType);
            return ret;
        }

        public override string ToString()
        {
            return GetMinimalCatchTypeList().Select(item => item.FullName).Aggregate((i1, i2) =>  i1 + Environment.NewLine + i2);
        }
    }
}
