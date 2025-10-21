using System;
using System.Threading;

namespace Vdovin.MultithreadingLab
{
    public class Calculator
    {
        // Общие переменные
        public int varAddTwo;
        public int varFact1;
        public int varFact2;
        public int varLoopValue;
        public static double varTotalCalculations = 0;

        // События для обновления UI
        public event Action<double, double> FactorialComplete;
        public event Action<double, double> FactorialMinusOneComplete;
        public event Action<int, double> AddTwoComplete;
        public event Action<double, int> LoopComplete;

        // Методы вычислений
        public void Factorial()
        {
            double result = 1;
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
            FactorialComplete?.Invoke(result, totalNow);
        }

        public void FactorialMinusOne()
        {
            double result = 1;
            double totalNow = 0;
            for (int i = 1; i <= varFact2 - 1; i++)
            {
                result *= i;
                lock (this)
                {
                    varTotalCalculations++;
                    totalNow = varTotalCalculations;
                }
            }
            FactorialMinusOneComplete?.Invoke(result, totalNow);
        }

        public void AddTwo()
        {
            int result = varAddTwo + 2;
            double totalNow;
            lock (this)
            {
                varTotalCalculations++;
                totalNow = varTotalCalculations;
            }
            AddTwoComplete?.Invoke(result, totalNow);
        }

        public void RunALoop()
        {
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
            LoopComplete?.Invoke(totalNow, varLoopValue);
        }
    }
}