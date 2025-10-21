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
        public static double varTotalCalculations = 0;

        // События: результат + общее число вычислений + время (мс)
        public event Action<BigInteger, double, double> FactorialComplete;
        public event Action<BigInteger, double, double> FactorialMinusOneComplete;
        public event Action<int, double, double> AddTwoComplete;
        public event Action<double, int, double> LoopComplete;

        // Методы запуска задач
        public async Task StartFactorialAsync()
        {
            var sw = Stopwatch.StartNew();
            BigInteger result = 1;
            double totalNow = 0;
            for (int i = 1; i <= varFact1; i++)
            {
                result *= i;
                lock (this)
                {
                    varTotalCalculations++;
                    totalNow = varTotalCalculations;
                }
            }
            sw.Stop();
            FactorialComplete?.Invoke(result, totalNow, sw.Elapsed.TotalMilliseconds);
        }

        public async Task StartFactorialMinusOneAsync()
        {
            var sw = Stopwatch.StartNew();
            BigInteger result = 1;
            double totalNow = 0;
            int limit = Math.Max(0, varFact2 - 1);
            for (int i = 1; i <= limit; i++)
            {
                result *= i;
                lock (this)
                {
                    varTotalCalculations++;
                    totalNow = varTotalCalculations;
                }
            }
            sw.Stop();
            FactorialMinusOneComplete?.Invoke(result, totalNow, sw.Elapsed.TotalMilliseconds);
        }

        public async Task StartAddTwoAsync()
        {
            var sw = Stopwatch.StartNew();
            int result = varAddTwo + 2;
            double totalNow;
            lock (this)
            {
                varTotalCalculations++;
                totalNow = varTotalCalculations;
            }
            sw.Stop();
            AddTwoComplete?.Invoke(result, totalNow, sw.Elapsed.TotalMilliseconds);
        }

        public async Task StartLoopAsync()
        {
            var sw = Stopwatch.StartNew();
            double totalNow = 0;
            for (int i = 1; i <= varLoopValue; i++)
            {
                for (int j = 1; j <= 500; j++)
                {
                    lock (this)
                    {
                        varTotalCalculations++;
                        totalNow = varTotalCalculations;
                    }
                }
            }
            sw.Stop();
            sw.Stop();
            LoopComplete?.Invoke(totalNow, varLoopValue, sw.Elapsed.TotalMilliseconds);
        }
    }
}