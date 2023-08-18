using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Emit;

namespace ExceptionReflector
{
    public static class OpCodeExtensions
    {
        //Método criado a partir da ECMA335. Sujeito a alterações em novas versões do framework .Net.
        public static ReadOnlyCollection<Type> Throws(this OpCode opCode)
        {
            List<Type> ret = new List<Type>();

            if
                (
                        opCode == OpCodes.Conv_Ovf_I1
                    ||
                        opCode == OpCodes.Conv_Ovf_I2
                    ||
                        opCode == OpCodes.Conv_Ovf_I4
                    ||
                        opCode == OpCodes.Conv_Ovf_I8
                    ||
                        opCode == OpCodes.Conv_Ovf_U1
                    ||
                        opCode == OpCodes.Conv_Ovf_U2
                    ||
                        opCode == OpCodes.Conv_Ovf_U4
                    ||
                        opCode == OpCodes.Conv_Ovf_U8
                    ||
                        opCode == OpCodes.Conv_Ovf_I
                    ||
                        opCode == OpCodes.Conv_Ovf_U
                    ||
                        opCode == OpCodes.Conv_Ovf_I1_Un
                    ||
                        opCode == OpCodes.Conv_Ovf_I2_Un
                    ||
                        opCode == OpCodes.Conv_Ovf_I4_Un
                    ||
                        opCode == OpCodes.Conv_Ovf_I8_Un
                    ||
                        opCode == OpCodes.Conv_Ovf_U1_Un
                    ||
                        opCode == OpCodes.Conv_Ovf_U2_Un
                    ||
                        opCode == OpCodes.Conv_Ovf_U4_Un
                    ||
                        opCode == OpCodes.Conv_Ovf_U8_Un
                    ||
                        opCode == OpCodes.Conv_Ovf_I_Un
                    ||
                        opCode == OpCodes.Conv_Ovf_U_Un
                    ||
                        opCode == OpCodes.Add_Ovf
                    ||
                        opCode == OpCodes.Add_Ovf_Un
                    ||
                        opCode == OpCodes.Mul_Ovf
                    ||
                        opCode == OpCodes.Mul_Ovf_Un
                    ||
                        opCode == OpCodes.Sub_Ovf
                    ||
                        opCode == OpCodes.Sub_Ovf_Un
                )
            {
                ret.Add(typeof(OverflowException));
            }
            else if (opCode == OpCodes.Call)
            {
                ret.Add(typeof(System.Security.SecurityException));
                ret.Add(typeof(MethodAccessException));
                ret.Add(typeof(MissingMethodException));
            }
            else if (opCode == OpCodes.Calli)
            {
                ret.Add(typeof(System.Security.SecurityException));
            }
            else if (opCode == OpCodes.Ckfinite)
            {
                ret.Add(typeof(ArithmeticException));
            }
            else if
                (
                        opCode == OpCodes.Ldind_I1
                    ||
                        opCode == OpCodes.Ldind_I2
                    ||
                        opCode == OpCodes.Ldind_I4
                    ||
                        opCode == OpCodes.Ldind_I8
                    ||
                        opCode == OpCodes.Ldind_U1
                    ||
                        opCode == OpCodes.Ldind_U2
                    ||
                        opCode == OpCodes.Ldind_U4
                    ||
                        opCode == OpCodes.Ldind_R4
                    ||
                        opCode == OpCodes.Ldind_R8
                    ||
                        opCode == OpCodes.Ldind_I
                    ||
                        opCode == OpCodes.Ldind_Ref
                    ||
                        opCode == OpCodes.Stind_Ref
                    ||
                        opCode == OpCodes.Stind_I1
                    ||
                        opCode == OpCodes.Stind_I2
                    ||
                        opCode == OpCodes.Stind_I4
                    ||
                        opCode == OpCodes.Stind_I8
                    ||
                        opCode == OpCodes.Stind_R4
                    ||
                        opCode == OpCodes.Stind_R8
                    ||
                        opCode == OpCodes.Stind_I
                    ||
                        opCode == OpCodes.Cpblk
                    ||
                        opCode == OpCodes.Initblk
                    ||
                        opCode == OpCodes.Ldlen
                    ||
                        opCode == OpCodes.Throw
                )
            {
                ret.Add(typeof(NullReferenceException));
            }
            else if (opCode == OpCodes.Div || opCode == OpCodes.Rem)
            {
                ret.Add(typeof(ArithmeticException));
                ret.Add(typeof(DivideByZeroException));
            }
            else if (opCode == OpCodes.Div_Un || opCode == OpCodes.Rem_Un)
            {
                ret.Add(typeof(DivideByZeroException));
            }
            else if (opCode == OpCodes.Ldftn)
            {
                ret.Add(typeof(MethodAccessException));
            }
            else if
                (
                        opCode == OpCodes.Ldloc
                    ||
                        opCode == OpCodes.Ldloc_S
                    ||
                        opCode == OpCodes.Ldloc_0
                    ||
                        opCode == OpCodes.Ldloc_1
                    ||
                        opCode == OpCodes.Ldloc_2
                    ||
                        opCode == OpCodes.Ldloc_3
                    ||
                        opCode == OpCodes.Ldloca
                    ||
                        opCode == OpCodes.Ldloca_S
                )
            {
                ret.Add(typeof(System.Security.VerificationException));
            }
            else if (opCode == OpCodes.Localloc)
            {
                ret.Add(typeof(StackOverflowException));
            }
            else if (opCode == OpCodes.Box)
            {
                ret.Add(typeof(OverflowException));
                ret.Add(typeof(TypeLoadException));
            }
            else if (opCode == OpCodes.Callvirt)
            {
                ret.Add(typeof(MethodAccessException));
                ret.Add(typeof(MissingMethodException));
                ret.Add(typeof(NullReferenceException));
                ret.Add(typeof(System.Security.SecurityException));
            }
            else if (opCode == OpCodes.Box || opCode == OpCodes.Refanyval || opCode == OpCodes.Castclass)
            {
                ret.Add(typeof(InvalidCastException));
                ret.Add(typeof(TypeLoadException));
            }
            else if (opCode == OpCodes.Cpobj || opCode == OpCodes.Ldobj || opCode == OpCodes.Stobj)
            {
                ret.Add(typeof(NullReferenceException));
                ret.Add(typeof(TypeLoadException));
            }
            else if (opCode == OpCodes.Isinst || opCode == OpCodes.Mkrefany)
            {
                ret.Add(typeof(TypeLoadException));
            }
            else if
                (
                        opCode == OpCodes.Ldelem
                    ||
                        opCode == OpCodes.Ldelem_I1
                    ||
                        opCode == OpCodes.Ldelem_I2
                    ||
                        opCode == OpCodes.Ldelem_I4
                    ||
                        opCode == OpCodes.Ldelem_I8
                    ||
                        opCode == OpCodes.Ldelem_U1
                    ||
                        opCode == OpCodes.Ldelem_U2
                    ||
                        opCode == OpCodes.Ldelem_U4
                    ||
                        opCode == OpCodes.Ldelem_R4
                    ||
                        opCode == OpCodes.Ldelem_R8
                    ||
                        opCode == OpCodes.Ldelem_I
                    ||
                        opCode == OpCodes.Ldelem_Ref
                )
            {
                ret.Add(typeof(IndexOutOfRangeException));
                ret.Add(typeof(NullReferenceException));
            }
            else if 
                (
                        opCode == OpCodes.Ldelema
                    || 
                        opCode == OpCodes.Stelem
                    ||
                        opCode == OpCodes.Stelem_I
                    ||
                        opCode == OpCodes.Stelem_I1
                    ||
                        opCode == OpCodes.Stelem_I2
                    ||
                        opCode == OpCodes.Stelem_I4
                    ||
                        opCode == OpCodes.Stelem_I8
                    ||
                        opCode == OpCodes.Stelem_R4
                    ||
                        opCode == OpCodes.Stelem_R8
                    ||
                        opCode == OpCodes.Stelem_Ref
                )
            {
                ret.Add(typeof(IndexOutOfRangeException));
                ret.Add(typeof(NullReferenceException));
                ret.Add(typeof(ArrayTypeMismatchException));
            }
            else if (opCode == OpCodes.Ldfld || opCode == OpCodes.Stfld)
            {
                ret.Add(typeof(FieldAccessException));
                ret.Add(typeof(MissingFieldException));
                ret.Add(typeof(NullReferenceException));
            }
            else if (opCode == OpCodes.Ldflda)
            {
                ret.Add(typeof(FieldAccessException));
                ret.Add(typeof(MissingFieldException));
                ret.Add(typeof(NullReferenceException));
                ret.Add(typeof(InvalidOperationException));
            }
            else if (opCode == OpCodes.Ldsfld || opCode == OpCodes.Ldsflda || opCode == OpCodes.Stsfld)
            {
                ret.Add(typeof(FieldAccessException));
                ret.Add(typeof(MissingFieldException));
            }
            else if (opCode == OpCodes.Ldvirtftn)
            {
                ret.Add(typeof(MethodAccessException));
                ret.Add(typeof(NullReferenceException));
            }
            else if (opCode == OpCodes.Newarr)
            {
                ret.Add(typeof(OutOfMemoryException));
                ret.Add(typeof(OverflowException));
            }
            else if (opCode == OpCodes.Newobj)
            {
                ret.Add(typeof(OutOfMemoryException));
                ret.Add(typeof(MissingMethodException));
                ret.Add(typeof(MethodAccessException));
                ret.Add(typeof(InvalidOperationException));
            }
            else if (opCode == OpCodes.Unbox)
            {
                ret.Add(typeof(InvalidCastException));
                ret.Add(typeof(TypeLoadException));
                ret.Add(typeof(NullReferenceException));
            }
            else if (opCode == OpCodes.Unbox_Any)
            {
                ret.Add(typeof(InvalidCastException));
                ret.Add(typeof(NullReferenceException));
            }

            return ret.AsReadOnly();
        }
    }
}
