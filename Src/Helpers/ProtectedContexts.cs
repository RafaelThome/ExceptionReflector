using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ExceptionReflector
{
    public class ProtectedContexts : Dictionary<Type, ProtectedContext>
    {
        public ProtectedContexts() : base() { }

        //public void RemoveProtectedContext(ExceptionHandlingClause exHndl)
        //{
        //    RemoveProtectedContext(new ProtectedContext(exHndl));
        //}
        //public void RemoveProtectedContext(ProtectedContext protectedContext)
        //{
        //    base.Remove(protectedContext.ExceptionType);
        //}

        public void AddProtectedContext(ExceptionHandlingClause exHndl)
        {
            AddProtectedContext(new ProtectedContext(exHndl));
        }
        public void AddProtectedContext(ProtectedContext protectedContext)
        {
            if(!base.ContainsKey(protectedContext.ExceptionType))
            {
                base.Add(protectedContext.ExceptionType, protectedContext);
            }
        }

        public void AddProtectedContexts(IEnumerable<ExceptionHandlingClause> exHndls)
        {
            foreach(ExceptionHandlingClause exHndl in exHndls)
                if(exHndl.Flags == ExceptionHandlingClauseOptions.Clause)
                    AddProtectedContext(exHndl);
        }
        public void AddProtectedContexts(IEnumerable<ProtectedContext> protectedContexts)
        {
            foreach(ProtectedContext protectedContext in protectedContexts)
                AddProtectedContext(protectedContext);
        }

        //public bool IsProtected(Type exceptionType, int offset)
        //{
        //    if (base.ContainsKey(exceptionType))
        //    {
        //        if (base[exceptionType].OffsetRange.IsInRange(offset))
        //            return true;
        //        else
        //            return false;
        //    }
        //    else
        //        return false;
        //}
        public bool IsProtected(Type exceptionType, int offset)
        {
            if (base.ContainsKey(exceptionType))
            {
                if (base[exceptionType].OffsetRange.IsInRange(offset))
                    return true;
                else
                {
                    //Se o offset já saiu do contexto protegido, já remove o tipo de exceção
                    base.Remove(exceptionType);
                    return false;
                }
            }
            else
                return false;
        }
    }
}
