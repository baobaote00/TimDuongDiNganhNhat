/**
 * tên : nguyen le trong tien
 * lop 19211tt4165
 */
using System;
namespace FloydTest
{
    class Node : IComparable<Node>
    {
        private int name;
        private Node par;
        private int g;
        private int h;

        public Node(int name, int g, int h)
        {
            Name = name;
            G = g;
            H = h;
        }

        public Node(int name)
        {
            this.name = name;
        }

        public int Name
        {
            get { return name; }
            set { name = value; }
        }
        public int G
        {
            get { return g; }
            set { g = value; }
        }
        public int H
        {
            get { return h; }
            set { h = value; }
        }
        internal Node Par
        {
            get { return par; }
            set { par = value; }
        }

        public Node()
        {
        }

        public int CompareTo(Node obj)
        {
            return (G + H) - (obj.G + obj.H);
        }

        public override string ToString()
        {
            return Name + " " + G + " " + H;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Name == ((Node)obj).Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
