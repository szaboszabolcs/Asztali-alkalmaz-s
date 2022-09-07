using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellenorzés
{
    internal class Record
    {
        public string rendszam;
        public int bOra;
        public int bPerc;
        public int bMp;
        public int bEmp;
        public int kOra;
        public int kPerc;
        public int kMp;
        public int kEmp;

        public Record(string sor)
        {
            string[] Mezok = sor.Split(' ');
            this.rendszam = Mezok[0];
            this.bOra = int.Parse(Mezok[1]);
            this.bPerc = int.Parse(Mezok[2]);
            this.bMp = int.Parse(Mezok[3]);
            this.bEmp = int.Parse(Mezok[4]);
            this.kOra = int.Parse(Mezok[5]);
            this.kPerc = int.Parse(Mezok[6]);
            this.kMp = int.Parse(Mezok[7]);
            this.kEmp = int.Parse(Mezok[8]);
        }
    }
}
