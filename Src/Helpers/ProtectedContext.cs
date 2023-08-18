using System;
using System.Reflection;

namespace ExceptionReflector
{
    public class ProtectedContext
    {
        private Type _exceptionType;
        private MethodOffsetRange _offsetRange;

        public Type ExceptionType
        {
            private set { _exceptionType = value; }
            get { return _exceptionType; }
        }

        public MethodOffsetRange OffsetRange
        {
            private set { _offsetRange = value; }
            get { return _offsetRange; }
        }

        public ProtectedContext(Type exceptionType, MethodOffsetRange offsetRange)
        {
            ExceptionType = exceptionType;
            OffsetRange = offsetRange;
        }
        public ProtectedContext(ExceptionHandlingClause exHndl)
        {
            if (exHndl.Flags != ExceptionHandlingClauseOptions.Clause)
                throw new ArgumentException("A propriedade Flags da ExceptionHandlingClauseOptions recebida no parâmetro exHndl precisa valer ExceptionHandlingClauseOptions.Clause", "exHndl");

            ExceptionType = exHndl.CatchType;
            OffsetRange = new MethodOffsetRange(exHndl.TryOffset, exHndl.TryOffset + exHndl.TryLength);
        }

        public static implicit operator ProtectedContext(ExceptionHandlingClause exHndl)
        {
            return new ProtectedContext(exHndl);
        }
    }
}
