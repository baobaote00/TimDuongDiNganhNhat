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
        private int weight;
        private int heuristic;

        public Node(int name, int g, int h)
        {
            Name = name;
            Weight = g;
            Heuristic = h;
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
        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        public int Heuristic
        {
            get { return heuristic; }
            set { heuristic = value; }
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
            return (Weight + Heuristic) - (obj.Weight + obj.Heuristic);
        }

        public override string ToString()
        {
            return Name + " " + Weight + " " + Heuristic;
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
