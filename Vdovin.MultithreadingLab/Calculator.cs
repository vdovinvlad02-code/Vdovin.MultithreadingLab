using System;
using System.Numerics;
using System.Threading.Tasks;

namespace Vdovin.MultithreadingLab
{
    public class Calculator
    {
        public double varTotalCalculations = 0;

        // Универсальный метод: принимает делегат, оборачивает в Task и считает операции
        private async Task<T> RunComputationAsync<T>(Func<T> computation)
        {
            return await Task.Run(() =>
            {
                // Выполняем делегат
                return computation.Invoke(); 
            });
        }

        // Вспомогательный метод для потокобезопасного инкремента
        private void IncrementCounter(int count = 1)
        {
            lock (this)
            {
                varTotalCalculations += count;
            }
        }

        public Task<BigInteger> ComputeFactorialAsync(int n)
        {
            return RunComputationAsync(() =>
            {
                BigInteger res = 1;
                for (int i = 1; i <= n; i++)
                {
                    res *= i;
                    IncrementCounter();
                }
                return res;
            });
        }

        public Task<BigInteger> ComputeFactorialMinusOneAsync(int n)
        {
            int limit = Math.Max(0, n - 1);
            return RunComputationAsync(() =>
            {
                BigInteger res = 1;
                for (int i = 1; i <= limit; i++)
                {
                    res *= i;
                    IncrementCounter();
                }
                return res;
            });
        }

        public Task<int> ComputeAddTwoAsync(int value)
        {
            return RunComputationAsync(() =>
            {
                IncrementCounter();
                return value + 2;
            });
        }

        public Task<double> ComputeLoopAsync(int iterations)
        {
            return RunComputationAsync(() =>
            {
                for (int i = 1; i <= iterations; i++)
                {
                    for (int j = 1; j <= 500; j++)
                    {
                        IncrementCounter();
                    }
                }
                return varTotalCalculations;
            });
        }
    }
}