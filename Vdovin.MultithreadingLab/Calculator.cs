using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Vdovin.MultithreadingLab
{
    public class Calculator
    {
        private readonly object sync = new object();
        public int AddValue;
        public int FactorialValue;
        public int FactorialMinusValue;
        public int LoopIterations;
        public static double OperationCount = 0; // ← ЕДИНСТВЕННОЕ ИЗМЕНЕНИЕ: имя переменной

        // -------------------- Синхронные методы --------------------
        public BigInteger ComputeFactorialSync()
        {
            BigInteger res = 1;
            lock (sync)
            {
                for (int i = 1; i <= FactorialValue; i++)
                {
                    res *= i;
                    OperationCount++; 
                }
            }
            return res;
        }

        public BigInteger ComputeFactorialMinusOneSync()
        {
            BigInteger res = 1;
            lock (sync)
            {
                for (int i = 1; i < FactorialMinusValue; i++)
                {
                    res *= i;
                    OperationCount++; // ← замена
                }
            }
            return res;
        }

        public int ComputeAddTwoSync()
        {
            lock (sync)
            {
                OperationCount++; // ← замена
            }
            return AddValue + 2;
        }

        public double ExecuteLoopSync()
        {
            double currentTotal = 0;
            for (int i = 0; i < LoopIterations; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    lock (sync)
                    {
                        OperationCount++; // ← замена
                        currentTotal = OperationCount; // ← замена
                    }
                }
            }
            return currentTotal;
        }

        // -------------------- Асинхронные методы через Task.Run --------------------
        public Task<BigInteger> ComputeFactorialTask()
        {
            return Task.Run(() => ComputeFactorialSync());
        }

        public Task<BigInteger> ComputeFactorialMinusOneTask()
        {
            return Task.Run(() => ComputeFactorialMinusOneSync());
        }

        public Task<int> ComputeAddTwoTask()
        {
            return Task.Run(() => ComputeAddTwoSync());
        }

        public Task<double> ExecuteLoopTask()
        {
            return Task.Run(() => ExecuteLoopSync());
        }

        // -------------------- Методы через Thread --------------------
        public Task<BigInteger> ComputeFactorialThread()
        {
            var tcs = new TaskCompletionSource<BigInteger>();
            new System.Threading.Thread(() =>
            {
                var res = ComputeFactorialSync();
                tcs.SetResult(res);
            }).Start();
            return tcs.Task;
        }

        public Task<BigInteger> ComputeFactorialMinusOneThread()
        {
            var tcs = new TaskCompletionSource<BigInteger>();
            new System.Threading.Thread(() =>
            {
                var res = ComputeFactorialMinusOneSync();
                tcs.SetResult(res);
            }).Start();
            return tcs.Task;
        }

        public Task<int> ComputeAddTwoThread()
        {
            var tcs = new TaskCompletionSource<int>();
            new System.Threading.Thread(() =>
            {
                var res = ComputeAddTwoSync();
                tcs.SetResult(res);
            }).Start();
            return tcs.Task;
        }

        public Task<double> ExecuteLoopThread()
        {
            var tcs = new TaskCompletionSource<double>();
            new System.Threading.Thread(() =>
            {
                var res = ExecuteLoopSync();
                tcs.SetResult(res);
            }).Start();
            return tcs.Task;
        }

        // -------------------- Настоящие Async методы --------------------
        public async Task<BigInteger> ComputeFactorialAsync()
        {
            BigInteger res = 1;
            await Task.Yield();
            for (int i = 1; i <= FactorialValue; i++)
            {
                res *= i;
                lock (sync)
                {
                    OperationCount++; // ← замена
                }
            }
            return res;
        }

        public async Task<BigInteger> ComputeFactorialMinusOneAsync()
        {
            BigInteger res = 1;
            await Task.Yield();
            for (int i = 1; i < FactorialMinusValue; i++)
            {
                res *= i;
                lock (sync)
                {
                    OperationCount++; // ← замена
                }
            }
            return res;
        }

        public async Task<int> ComputeAddTwoAsync()
        {
            await Task.Yield();
            lock (sync)
            {
                OperationCount++; // ← замена
            }
            return AddValue + 2;
        }

        public async Task<double> ExecuteLoopAsyncMethod()
        {
            double currentTotal = 0;
            await Task.Yield();
            for (int i = 0; i < LoopIterations; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    lock (sync)
                    {
                        OperationCount++; // ← замена
                        currentTotal = OperationCount; // ← замена
                    }
                }
            }
            return currentTotal;
        }
    }
}