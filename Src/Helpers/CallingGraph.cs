using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExceptionReflector
{
    public class CallingGraph : IDisposable
    {
        public class Node : IDisposable
        {
            #region [ Inner Classes ]

            public class Calls : List<Node>
            {
                public Calls() : base() { }
            }

            public class Caller
            {
                private Node _nodeRef = null;
                private int _callOpMtdOffset = int.MinValue;

                public Node NodeRef
                {
                    private set { _nodeRef = value; }
                    get { return _nodeRef; }
                }

                public int CallOpMtdOffset
                {
                    private set { _callOpMtdOffset = value; }
                    get { return _callOpMtdOffset; }
                }

                public Caller(Node nodeRef, int callOpAddr)
                {
                    NodeRef = nodeRef;
                    CallOpMtdOffset = callOpAddr;
                }
            }

            #endregion

            #region [ Local Variables ]

            private MethodBase _mtdBase = null;
            private Caller _callerMethod = null;
            private Calls _calledMethods = null;
            private List<int> _rethrowMtdOffsets = new List<int>();

            #endregion

            #region [ Properties ]

            public MethodBase MtdBase
            {
                set { _mtdBase = value; }
                get { return _mtdBase; }
            }

            public Caller CallerMethod
            {
                private set { _callerMethod = value; }
                get { return _callerMethod; }
            }

            private Calls CalledMethods
            {
                set { _calledMethods = value; }
                get { return _calledMethods; }
            }

            public List<int> RethrowMtdOffsets
            {
                set { _rethrowMtdOffsets = value; }
                get { return _rethrowMtdOffsets; }
            }

            #endregion

            #region [ Constructors ]

            protected internal Node(MethodBase mtdBase) : this(mtdBase, null) { }
            public Node(MethodBase mtdBase, Caller callerMethod)
            {
                MtdBase = mtdBase;
                CallerMethod = callerMethod;
                CalledMethods = new Calls();
                RethrowMtdOffsets = new List<int>();
            }

            #endregion

            #region [ IDisposable ]

            public void Dispose()
            {
                foreach (Node n in CalledMethods)
                    n.Dispose();
                CalledMethods.Clear();
                RethrowMtdOffsets.Clear();
            }

            #endregion

            #region [ Operations ]

            public Node AddCalledMethod(MethodBase methodBase, int callInstructionOffset)
            {
                Node calledMethod = new CallingGraph.Node(methodBase, new CallingGraph.Node.Caller(this, callInstructionOffset));
                CalledMethods.Add(calledMethod);
                return calledMethod;
            }

            #endregion
        }

        private CallingGraph.Node _root = null;

        public CallingGraph.Node Root
        {
            private set { _root = value; }
            get { return _root; }
        }

        public CallingGraph(MethodBase firstMethod)
        {
            Root = new Node(firstMethod);
        }

        public void Dispose()
        {
            Root.Dispose();
        }
    }
}
