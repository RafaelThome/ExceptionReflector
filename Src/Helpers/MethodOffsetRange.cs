using System;
using System.Collections.Generic;

namespace ExceptionReflector
{
    public class MethodOffsetRange : IEquatable<MethodOffsetRange>
    {

        #region [ Local Variables ]

        private int _offsetInicial = int.MaxValue;
        private int _offsetFinal = int.MinValue;

        public int OffsetInicial
        {
            private set { _offsetInicial = value; }
            get { return _offsetInicial; }
        }

        public int OffsetFinal
        {
            private set { _offsetFinal = value; }
            get { return _offsetFinal; }
        }

        public MethodOffsetRange(int offsetInicial, int offsetFinal)
        {
            if (offsetInicial < 0)
                throw new ArgumentOutOfRangeException("offsetInicial", "offsetInicial can not be negative");

            if (offsetInicial < 0)
                throw new ArgumentOutOfRangeException("offsetFinal", "OffsetFinal can not be negative");

            if (offsetInicial > offsetFinal)
                throw new ArgumentOutOfRangeException("offsetFinal", "OffsetFinal must be grater or equal than offset Inicial");

            OffsetInicial = offsetInicial;
            OffsetFinal = offsetFinal;
        }

        #endregion

        #region [ Operations ]

        public bool IsInRange(int Offset)
        {
            return IsInRange(Offset, Offset);
        }
        public bool IsInRange(int offsetInicial, int offsetFinal)
        {
            return (offsetInicial > OffsetInicial && offsetFinal < OffsetFinal);
        }
        public bool IsInRange(MethodOffsetRange otherRange)
        {
            return IsInRange(otherRange.OffsetInicial, otherRange.OffsetFinal);
        }

        #endregion

        #region [ Operators ]

        public static implicit operator MethodOffsetRange(int methodOffset)
        {
            return new MethodOffsetRange(methodOffset, methodOffset);
        }

        public static bool operator ==(MethodOffsetRange range1, MethodOffsetRange range2)
        {
            if (((object)range1) == null)
            {
                if (((object)range2) == null)
                    return true;
                else
                    return false;
            }
            else
                return range1.Equals(range2);
        }

        public static bool operator !=(MethodOffsetRange range1, MethodOffsetRange range2)
        {
            if (((object)range1) == null)
            {
                if (((object)range2) == null)
                    return false;
                else
                    return true;
            }
            else
                return !range1.Equals(range2);
        }

        #endregion

        #region [ IEquatable<MethodOffsetRange> ]

        public bool Equals(MethodOffsetRange other)
        {
            if (other == null)
                return false;

            return (this.OffsetInicial == other.OffsetInicial && this.OffsetFinal == other.OffsetFinal);
        }

        #endregion

        #region [ Object Overrides ]

        public override bool Equals(object obj)
        {
            MethodOffsetRange offsetRange = obj as MethodOffsetRange;
            return Equals(offsetRange);
        }

        public override int GetHashCode()
        {
            int a = OffsetFinal << 16;

            return OffsetInicial ^ OffsetFinal ;
        }

        #endregion
    }
}
