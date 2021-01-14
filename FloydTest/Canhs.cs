using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloydTest
{
    class Canhs
    {
        private int dinhDau;
        private int trongSo;
        private int dinhCuoi;

        public int DinhCuoi
        {
            get { return dinhCuoi; }
            set { dinhCuoi = value; }
        }

        public Canhs(int dinhDau,int dinhCuoi, int trongSo)
        {
            this.dinhDau = dinhDau;
            this.dinhCuoi = dinhCuoi;
            this.trongSo = trongSo;
        }
       

        public int TrongSo
        {
            get { return trongSo; }
            set { trongSo = value; }
        }

        public int DinhDau
        {
            get { return dinhDau; }
            set { dinhDau = value; }
        }

        public override string ToString()
        {
            return dinhCuoi+"";
        }

        public string print()
        {
            return dinhCuoi + "\t" + trongSo;
        }
    }
}
