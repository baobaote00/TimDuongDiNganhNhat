using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloydTest
{
    class Dinhs
    {
        private int dinh;
        private int trongSo;

        public Dinhs(int dinh, int trongSo)
        {
            this.dinh = dinh;
            this.trongSo = trongSo;
        }
        public Dinhs( int trongSo)
        {
            this.trongSo = trongSo;
        }

        public int TrongSo
        {
            get { return trongSo; }
            set { trongSo = value; }
        }

        public int Dinh
        {
            get { return dinh; }
            set { dinh = value; }
        }
    }
}
