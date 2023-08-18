using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionReflector
{
    public class TypeHierarchy
    {
        public class Node : IEquatable<Node>
        {
            #region [ Internal Types ]

            //Created for internal use of the HashSet<Node> SubTypes
            public class TypeHierarchyNodeEqualityComparer : IEqualityComparer<Node>
            {
                public bool Equals(Node x, Node y)
                {
                    return x.Equals(y);
                }

                public int GetHashCode(Node obj)
                {
                    return obj.GetHashCode();
                }
            }

            #endregion

            #region [ Local Variables ]

            private Type _internalType = null;
            private HashSet<Node> _subTypes = null;
            private Node _superType = null;

            #endregion

            #region [ Methods ]

            #region [ Properties ]

            public Type InternalType
            {
                private set { _internalType = value; }
                get { return _internalType; }
            }

            private HashSet<Node> SubTypes
            {
                set { _subTypes = value; }
                get { return _subTypes; }
            }

            public Node SuperType
            {
                set { _superType = value; }
                get { return _superType; }
            }

            #endregion

            #region [ Constructors ]

            public Node(Type internalType) : this(internalType, null) { }
            public Node(Type internalType, Node superType)
            {
                SuperType = superType;
                InternalType = internalType;
                SubTypes = new HashSet<Node>(new TypeHierarchyNodeEqualityComparer());
            }

            #endregion

            #region [ Operators ]

            public static bool operator ==(Node node1, Node node2)
            {
                if (((object)node1) == null)
                {
                    if (((object)node2) == null)
                        return true;
                    else
                        return false;
                }
                else
                    return node1.Equals(node2);
            }

            public static bool operator !=(Node node1, Node node2)
            {
                if (((object)node1) == null)
                {
                    if (((object)node2) == null)
                        return false;
                    else
                        return true;
                }
                else
                    return !node1.Equals(node2);
            }

            #endregion

            #region [ Created for use of TypeHierarchyNodeEqualityComparer ]

            #region [ IEquatable<Node> ]

            public bool Equals(Node other)
            {
                if (other == null)
                    return false;

                if (InternalType == null)
                {
                    if (other.InternalType == null)
                        return true;
                    else
                        return false;
                }
                else
                    if (other.InternalType == null)
                        return false;
                    else
                        return InternalType.FullName.Equals(other.InternalType.FullName);
            }

            #endregion

            #region [ Object Overrides ]

            public override bool Equals(object obj)
            {
                Node nd = obj as Node;
                return Equals(nd);
            }

            public override int GetHashCode()
            {
                return ((InternalType == null)?(0):(InternalType.GetHashCode()));
            }

            #endregion

            #endregion

            #region [ Operations ]

            public Node AddSubType(Type x)
            {
                if (this.InternalType.FullName.Equals(x.FullName))
                    return this;

                Type superClass = x.BaseType;
                if (superClass != null)
                {
                    if (InternalType.FullName.Equals(superClass.FullName))
                    {
                        Node subNode = new Node(x);
                        if (!this.SubTypes.Contains(subNode))
                        {
                            subNode.SuperType = this;
                            this.SubTypes.Add(subNode);
                            return subNode;
                        }
                        else
                            return this.SubTypes.Single(n => n.Equals(subNode));
                    }
                    else
                    {
                        Node superNode = AddSubType(superClass);
                        if (superNode != null)
                        {
                            Node nd = new Node(x);
                            if (!superNode.SubTypes.Contains(nd))
                            {
                                nd.SuperType = superNode;
                                superNode.SubTypes.Add(nd);
                                return nd;
                            }
                            else
                                return superNode.SubTypes.Single(n => n.Equals(nd));
                        }
                        return null;
                    }
                }
                else
                    return null;
            }

            public IEnumerable<Node> GetSubTypes()
            {
                return SubTypes.ToArray();
            }

            #endregion

            #endregion
        }

        private Node _root = null;

        public Node Root
        {
            get { return _root; }
            private set { _root = value; }
        }

        public TypeHierarchy()
        {
            Root = new Node(typeof(Exception));
        }

        private string InheritanceChain(Type tp)
        {
            string ret = tp.FullName;
            if (tp.BaseType != null)
                ret += " : " + InheritanceChain(tp.BaseType);
            return ret;
        }

        private string ToString(int identLevel, Node node)
        {
            StringBuilder ret = new StringBuilder(new string(Convert.ToChar("\t"), identLevel) + InheritanceChain(node.InternalType) + Environment.NewLine);
            foreach (Node child in node.GetSubTypes())
                ret.Append(ToString(identLevel + 1, child));
            return ret.ToString();   
        }

        public override string ToString()
        {
            return ToString(0, Root);
        }
    }
}
