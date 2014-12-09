using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RandomGeneratorProblem
{
    public class RandomGenerator
    {
        RandomOrg NetRandom;
        System.Random LocalRandom = new System.Random();
        int MaxValue { get; set; }
        int MinValue { get; set; }
        int _permanentCount; //Number of elements, which must always be in Queue
        int PermanentCount
        {
            set
            {
                if (value > 0)
                    _permanentCount = value;
                else
                    throw new ArgumentException("PermanentCount must be more then zero!!!");
            }
        }

        public RandomGenerator(int minValue, int maxValue, int permanentCount = 10)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            PermanentCount = permanentCount;
            NetRandom = new RandomOrg(minValue, maxValue);
        }

        public IEnumerable<int> GetNumbers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (!NetRandom.NetworkError && NetRandom.Count > 0)
                {
                    //Return NetRandom number and add 10000 for visualization random numbers from RandomOrg
                    yield return NetRandom.Next + 10000;
                    //yield return NetRandom.Next
                }
                else
                if (!NetRandom.NetworkError && NetRandom.Count == 0)
                {
                    //Request for load new numbers
                    NetRandom.GetNewNumbers(_permanentCount);
                    //Return number from local Random
                    yield return LocalRandom.Next(MinValue, MaxValue);
                }
                else
                    yield return LocalRandom.Next(MinValue, MaxValue);
            }
            //Load random numbers for future using 
            if (!NetRandom.NetworkError) NetRandom.GetNewNumbers(_permanentCount);
        }

    }
}
