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
        void copyObject(Chrom bestChrom, Chrom gen)
        {
            bestChrom.FUN = gen.FUN;
            bestChrom.GEN = gen.GEN;
            bestChrom.CHANCE = gen.CHANCE;
            bestChrom.LENGTH = gen.LENGTH;
            bestChrom.TMPGEN = gen.TMPGEN;
        }
        void reproduction(Population population, int []weight)
        {
            population.sortGens();
            population.createChilds(weight);
        }
        void migration(Population Population1, Population Population2)
        {
            List<Chrom> tmpChromList1 = new List<Chrom>();
            List<Chrom> tmpChromList2 = new List<Chrom>();
            for (int i = Population1.gensList.Count - 1; i > 0; i--)
            {
                if (i > Population1.gensList.Count - 26)
                {
                    tmpChromList1.Add(Population1.gensList[i]);
                    tmpChromList2.Add(Population2.gensList[i]);
                } else
                {
                    break;
                }
            }
            Population1.gensList.RemoveRange(74, 25);
            Population2.gensList.RemoveRange(74, 25);
            for (int i = 0; i < tmpChromList1.Count; i++)
            {
                Population1.gensList.Add(tmpChromList2[i]);
                Population2.gensList.Add(tmpChromList1[i]);
            }
        }
        List<Chrom> ChromList1 = new List<Chrom>();
        List<Chrom> ChromList2 = new List<Chrom>();
        Chrom bestChrom = new Chrom();
        int [] weight;
        int maxWeight = 300;
        int populationSize = 100;
        bool isStop;
        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int length = 10;
            isStop = true;
            Random rand = new Random();
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            ChromList1.Clear();
            ChromList2.Clear();
            weight = new Int32[10] { rand.Next(10,50) , rand.Next(10, 50), rand.Next(10, 50),
                rand.Next(10,100), rand.Next(10,50), rand.Next(10,50) ,rand.Next(10,50), rand.Next(10,50), rand.Next(10,100), rand.Next(10,100) };
            dataGridView1.ColumnCount = 6;
            dataGridView2.ColumnCount = 9;

            for (int i = 0; i < populationSize; i++)
            {
                ChromList1.Add(new Chrom(rand, length, weight));
                ChromList2.Add(new Chrom(rand, length, weight));
            }
            Population Population1 = new Population(ChromList1, populationSize);
            Population Population2 = new Population(ChromList2, populationSize);
            bestChrom.FUN = Population1.getGen(0).FUN;
            for (int s = 0; s < 3; s++)
            {
                dataGridView1.Rows.Add(s + "-ая итерация");
                dataGridView2.Rows.Add(s + "-ая итерация");
            
                reproduction(Population1, weight);
                reproduction(Population2, weight);

                Population1.mutation(weight);
                Population2.mutation(weight);
                for (int i = 0; i < populationSize; i++)
                {
                    dataGridView1.Rows.Add(Population1.getGen(i).GEN, Population1.getGen(i).FUN, Population1.getGen(i).CHANCE,
                       Population1.parentsList[i].GEN, Population1.parentsList[i].FUN, Population1.childrensList[i].TMPGEN,
                       Population1.childrensList[i].GEN, Population1.gensList[i].GEN);

                    dataGridView2.Rows.Add(Population2.getGen(i).GEN, Population2.getGen(i).FUN, Population2.getGen(i).CHANCE,
                       Population2.parentsList[i].GEN, Population2.parentsList[i].FUN, Population2.childrensList[i].TMPGEN,
                       Population2.childrensList[i].GEN, Population2.gensList[i].GEN);
                }
                Population1.createNewPopulation();
                Population2.createNewPopulation();
                for (int i = 0; i < Population1.gensList.Count; i++)
                {
                    if (i > populationSize)
                    {
                        dataGridView1.Rows.Add("", "", "", "", "", "", "" , Population1.gensList[i].GEN);
                    }
                }
                for (int i = 0; i < Population2.gensList.Count; i++)
                {
                    if (i > populationSize)
                    {
                        dataGridView2.Rows.Add("", "", "", "", "", "", "", Population2.gensList[i].GEN);
                    }
                }
                List<Chrom> tmpList1 = new List<Chrom>();
                List<Chrom> tmpList2 = new List<Chrom>();
                for (int i = 0; i < Population1.gensList.Count; i++)
                {
                    tmpList1.Add(Population1.gensList[i]);
                }
                for (int i = 0; i < Population2.gensList.Count; i++)
                {
                    tmpList2.Add(Population2.gensList[i]);
                }
                Population1 = new Population(tmpList1, populationSize);
                Population2 = new Population(tmpList2, populationSize);
                if (rand.NextDouble() <= 0.5)
                {
                    migration(Population1, Population2);
                }
                tmpList1.Clear();
                tmpList2.Clear();
                Population1.clearPopulation();
                Population2.clearPopulation();

                for (int i = 0; i < Population1.gensList.Count; i++)
                {
                    if (bestChrom.FUN <= Population1.getGen(i).FUN && Population1.getGen(i).FUN < maxWeight)
                    {
                        copyObject(bestChrom, Population1.getGen(i));
                    }
                    if (bestChrom.FUN <= Population2.getGen(i).FUN && Population2.getGen(i).FUN < maxWeight)
                    {
                        copyObject(bestChrom, Population2.getGen(i));
                    }
                }
                if (bestChrom.FUN <= maxWeight && bestChrom.FUN >= (maxWeight-1))
                {
                    MessageBox.Show("Лучшая хромосома: " + bestChrom.GEN + "\nФункция = " + bestChrom.FUN.ToString(), "Решение найдено на " + (s + 1) + " итерации");
                    isStop = false;
                    break;
                }
            }
            if (isStop)
            {
                MessageBox.Show("Лучшая хромосома: " + bestChrom.GEN + "\nФункция = " + bestChrom.FUN.ToString(), "Все итерации выполненые");
            }
        }
    }
}
