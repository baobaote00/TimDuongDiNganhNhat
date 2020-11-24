using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloydTest
{
    class Node:IComparable<Node>
    {
        private int name;
        private Node par;
        private int g;
        private int h;

        public Node(int name, int g, int h)
        {
            this.Name = name;
            this.G = g;
            this.H = h;
        }

        public Node(int name)
        {
            this.name = name;
        }

        public int Name { get => name; set => name = value; }
        public int G { get => g; set => g = value; }
        public int H { get => h; set => h = value; }
        internal Node Par { get => par; set => par = value; }

        public Node()
        {
        }

        public int CompareTo(Node obj)
        {
            return (this.G + this.H) - (obj.G + obj.H);
        }

        public void display()
        {
            Console.WriteLine(this.Name+" "+this.G+" "+this.H);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return this.Name == ((Node) obj).Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
