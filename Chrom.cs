using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    class Chrom
    {
        public int[] genotip;
        string gen;
        int lenght;
        int function;
        double chanceValue;
        string tmpgen;

        public int LENGTH
        {
            get { return lenght; }
            set { lenght = value; }
        }
        public string GEN
        {
            get { return gen; }
            set { gen = value; }
        }
        public string TMPGEN
        {
            get { return tmpgen; }
            set { tmpgen = value; }
        }
        public int FUN
        {
            get { return function; }
            set { function = value; }
        }

        public int getGen(int i) {
            return genotip[i];
        }
        public void setGen(int i, int value) {
            genotip[i] = value;
        }
        public double CHANCE {
            get => chanceValue; set => chanceValue = value;
        }
        public Chrom(Random rand, int lenght, int []weight)
        {
            function = 0;
            this.lenght = lenght;
            genotip = new Int32[lenght];
            for (int i = 0; i < lenght; i++)
            {
                genotip[i] = rand.Next(0, 2);
                gen += genotip[i].ToString();
                function += (genotip[i] == 1) ? weight[i] : 0;
            }
        }
        public Chrom() {
            this.CHANCE = 0;
            this.FUN = 0;
            this.GEN = "0000000";
        }
        public Chrom(string str, int []weight)
        {
            genotip = str.Select(c => c - '0').ToArray(); ;
            gen = str;
            for (int i = 0; i < genotip.Length; i++)
            {
                function += (genotip[i] == 1) ? weight[i] : 0;
            }
        }
    }
}
