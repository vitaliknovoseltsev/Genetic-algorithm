using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    class Population
    {
        int populationSize;
        double maxFunctionValue;
        double mediumFunctionValue;
        double summFunctionValue;
        double mutationChance;
        public List<Chrom> gensList;
        public List<Chrom> parentsList;
        public List<Chrom> childrensList;


        public int POP_SIZE
        {
            get { return populationSize; }
            set { populationSize = value; }
        }
        public double MAX_VAL
        {
            get { return maxFunctionValue; }
            set { maxFunctionValue = value; }
        }
        public double MEDIUM_VAL
        {
            get { return mediumFunctionValue; }
            set { mediumFunctionValue = value; }
        }

        public double SUMM_VAL { get => summFunctionValue; set => summFunctionValue = value; }

        public Chrom getGen(int i)
        {
            return gensList[i];
        }
        public void setChanceValue()
        {
            foreach (Chrom gen in gensList)
            {
                gen.CHANCE = gen.FUN / this.summFunctionValue;
            }
        }

        public Population(List<Chrom> newGensList, int populationSize)
        {
            this.mutationChance = 0.5;
            this.populationSize = populationSize;
            gensList = new List<Chrom>();
            parentsList = new List<Chrom>();
            childrensList = new List<Chrom>();
            foreach (Chrom gen in newGensList)
            {
                gensList.Add(gen);
                this.summFunctionValue += gen.FUN;
            }
            this.mediumFunctionValue = this.summFunctionValue / this.populationSize;
            this.setChanceValue();
        }
        public void sortGens()
        {
            gensList.Sort((a, b) => b.FUN.CompareTo(a.FUN));
            Random rand = new Random();
            double summOfChance;
            for (int i = 0; i < 30; i++)
            {
                Chrom gen = getGen(i);
                double randomVal = rand.NextDouble();
                summOfChance = 0;
                for (int j = 0; j < 30; j++)
                {
                    Chrom newGen = getGen(j);
                    summOfChance += newGen.CHANCE;
                    if (randomVal <= summOfChance)
                    {
                        parentsList.Add(newGen);
                        break;
                    }
                }
            }
        }
        // Оператор Кроссинговера
        public void createChilds(int []weight)
        {
            Random randK = new Random();
            for (int i = 0; i < parentsList.Count; i+=2)
            {
                int crossingoverDot = randK.Next(0, parentsList[i].GEN.Length);
                if (i + 1 >= parentsList.Count) break;
                string father = parentsList[i].GEN;
                string mother = parentsList[i + 1].GEN;
                string childA = father.Substring(0,crossingoverDot) + mother.Substring(crossingoverDot);
                string childB = mother.Substring(0, crossingoverDot) + father.Substring(crossingoverDot);
                childrensList.Add(new Chrom(childA, weight));
                childrensList.Add(new Chrom(childB, weight));
            }
        }
        public void mutation(int[] weight) {
            Random mutationRandom = new Random();
            Random genRandom = new Random();
            List<Chrom> tmpList = new List<Chrom>(childrensList);
            childrensList.Clear();
            for (int i = 0; i < tmpList.Count; i++)
            {
                double canMutatuin = mutationRandom.NextDouble();
                if (canMutatuin <= mutationChance)
                {
                    int genMutationPosition = genRandom.Next(0, tmpList[i].GEN.Length);
                    tmpList[i].setGen(genMutationPosition, tmpList[i].getGen(0) == 1 ? 0 : 1);
                }
                string newGen = "";
                for (int j = 0; j < tmpList[i].genotip.Length; j++)
                {
                    newGen += tmpList[i].getGen(j).ToString();
                }
                childrensList.Add(new Chrom(newGen, weight));
            }
        }
    }
}
