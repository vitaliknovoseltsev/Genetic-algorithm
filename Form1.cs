using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{   
    public partial class Form1 : Form
    {
        void reproduction(Population population, int []weight)
        {
            population.sortGens();
            for (int i = 0; i < 30; i++)
            {
                dataGridView2.Rows.Add();
                dataGridView2[0, i].Value = population.getGen(i).GEN;
                dataGridView2[1, i].Value = population.getGen(i).FUN;
                dataGridView2[2, i].Value = population.getGen(i).CHANCE;
                dataGridView2[4, i].Value = population.parentsList[i].GEN;
                dataGridView2[5, i].Value = population.parentsList[i].FUN;
            }
            population.createChilds(weight);
            for (int i = 0; i < 30; i++)
            {
                dataGridView2[6, i].Value = population.childrensList[i].GEN;
            }
        }

        List<Chrom> ChromList = new List<Chrom>();
        List<Chrom> newChromList = new List<Chrom>();
        int [] weight;
        int maxWeight = 400;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int length = 10;
            weight = new Int32[10] { 10, 20, 30, 40, 50, 60 ,70, 80, 90, 100 };
            Random rand = new Random();
            dataGridView1.ColumnCount = 6;
            dataGridView2.ColumnCount = 7;

            for (int i = 0; i < 30; i++)
            {
                ChromList.Add(new Chrom(rand, length, weight));

                dataGridView1.Rows.Add();
            }
            Population Population = new Population(ChromList, 30);
            for (int i = 0; i < 30; i++)
            {
                Chrom gen = Population.getGen(i);
                dataGridView1[0, i].Value = gen.GEN;
                dataGridView1[1, i].Value = gen.FUN;
                dataGridView1[2, i].Value = gen.CHANCE;
                dataGridView1[3, i].Value = Math.Round(Population.MEDIUM_VAL, 3);
                dataGridView1[4, i].Value = Population.SUMM_VAL;
            }
            reproduction(Population, weight);

        }
    }
}
