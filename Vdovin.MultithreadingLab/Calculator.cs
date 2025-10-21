using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;

namespace Vdovin.MultithreadingLab
{
    public class Calculator
    {
        public int varAddTwo;
        public int varFact1;
        public int varFact2;
        public int varLoopValue;
        public double varTotalCalculations = 0; // ← УБРАН static

        public async Task<BigInteger> ComputeFactorialAsync(int n)
        {
            return await Task.Run(() =>
            {
                BigInteger result = 1;
                for (int i = 1; i <= n; i++)
                {
                    result *= i;
                    lock (this)
                    {
                        varTotalCalculations++;
                    }
                }
                return result;
            });
        }

        public async Task<BigInteger> ComputeFactorialMinusOneAsync(int n)
        {
            int limit = Math.Max(0, n - 1);
            return await Task.Run(() =>
            {
                BigInteger result = 1;
                for (int i = 1; i <= limit; i++)
                {
                    result *= i;
                    lock (this)
                    {
                        varTotalCalculations++;
                    }
                }
                return result;
            });
        }

        public async Task<int> ComputeAddTwoAsync(int value)
        {
            return await Task.Run(() =>
            {
                lock (this)
                {
                    varTotalCalculations++;
                }
                return value + 2;
            });
        }

        public async Task<double> ComputeLoopAsync(int iterations)
        {
            return await Task.Run(() =>
            {
                for (int i = 1; i <= iterations; i++)
                {
                    for (int j = 1; j <= 500; j++)
                    {
                        lock (this)
                        {
                            varTotalCalculations++;
                        }
                    }
                }
                return varTotalCalculations;
            });
        }
    }
}