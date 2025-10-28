using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;
using Vdovin.MultithreadingLab;

namespace Vdovin.MultithreadingLab
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnAllMethods_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtValue.Text, out int value) || value < 0)
            {
                MessageBox.Show("Введите корректное неотрицательное целое число.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Сбрасываем общий счётчик перед каждым запуском
            Calculator.OperationCount = 0;

            // Для честного сравнения — каждый метод должен работать с "чистым" состоянием
            // Поэтому создаём новый калькулятор для КАЖДОГО вызова
            // Но так как OperationCount — статическое, его сброс делает сравнение корректным

            int loopIters = Math.Max(1, value / 10); // избегаем 0

            // ==================== Факториал ====================
            var sw = Stopwatch.StartNew();
            var calc1 = new Calculator { FactorialValue = value };
            var factSync = calc1.ComputeFactorialSync();
            sw.Stop();
            var syncTime = sw.ElapsedMilliseconds;

            Calculator.OperationCount = 0;
            sw.Restart();
            var calc2 = new Calculator { FactorialValue = value };
            var factTask = await calc2.ComputeFactorialTask();
            sw.Stop();
            var taskTime = sw.ElapsedMilliseconds;

            Calculator.OperationCount = 0;
            sw.Restart();
            var calc3 = new Calculator { FactorialValue = value };
            var factThread = await calc3.ComputeFactorialThread();
            sw.Stop();
            var threadTime = sw.ElapsedMilliseconds;

            Calculator.OperationCount = 0;
            sw.Restart();
            var calc4 = new Calculator { FactorialValue = value };
            var factAsync = await calc4.ComputeFactorialAsync();
            sw.Stop();
            var asyncTime = sw.ElapsedMilliseconds;

            lblFactorial.Text = $"Sync: {syncTime} мс | Task: {taskTime} мс | Thread: {threadTime} мс | Async: {asyncTime} мс";

            // ==================== Факториал-1 ====================
            Calculator.OperationCount = 0;
            sw.Restart();
            var calc5 = new Calculator { FactorialMinusValue = value };
            var factMinusSync = calc5.ComputeFactorialMinusOneSync();
            sw.Stop();
            var fms = sw.ElapsedMilliseconds;

            Calculator.OperationCount = 0;
            sw.Restart();
            var calc6 = new Calculator { FactorialMinusValue = value };
            var factMinusTask = await calc6.ComputeFactorialMinusOneTask();
            sw.Stop();
            var fmtask = sw.ElapsedMilliseconds;

            Calculator.OperationCount = 0;
            sw.Restart();
            var calc7 = new Calculator { FactorialMinusValue = value };
            var factMinusThread = await calc7.ComputeFactorialMinusOneThread();
            sw.Stop();
            var fmthread = sw.ElapsedMilliseconds;

            Calculator.OperationCount = 0;
            sw.Restart();
            var calc8 = new Calculator { FactorialMinusValue = value };
            var factMinusAsync = await calc8.ComputeFactorialMinusOneAsync();
            sw.Stop();
            var fmasync = sw.ElapsedMilliseconds;

            lblFactorialMinus.Text = $"Sync: {fms} мс | Task: {fmtask} мс | Thread: {fmthread} мс | Async: {fmasync} мс";

            // ==================== Добавить 2 ====================
            Calculator.OperationCount = 0;
            sw.Restart();
            var calc9 = new Calculator { AddValue = value };
            var addSync = calc9.ComputeAddTwoSync();
            sw.Stop();
            var ats = sw.ElapsedMilliseconds;

            Calculator.OperationCount = 0;
            sw.Restart();
            var calc10 = new Calculator { AddValue = value };
            var addTask = await calc10.ComputeAddTwoTask();
            sw.Stop();
            var attask = sw.ElapsedMilliseconds;

            Calculator.OperationCount = 0;
            sw.Restart();
            var calc11 = new Calculator { AddValue = value };
            var addThread = await calc11.ComputeAddTwoThread();
            sw.Stop();
            var atthread = sw.ElapsedMilliseconds;

            Calculator.OperationCount = 0;
            sw.Restart();
            var calc12 = new Calculator { AddValue = value };
            var addAsync = await calc12.ComputeAddTwoAsync();
            sw.Stop();
            var atasync = sw.ElapsedMilliseconds;

            lblAddTwo.Text = $"Sync: {ats} мс | Task: {attask} мс | Thread: {atthread} мс | Async: {atasync} мс";

            // ==================== Цикл ====================
            Calculator.OperationCount = 0;
            sw.Restart();
            var calc13 = new Calculator { LoopIterations = loopIters };
            var loopSync = calc13.ExecuteLoopSync();
            sw.Stop();
            var ls = sw.ElapsedMilliseconds;

            Calculator.OperationCount = 0;
            sw.Restart();
            var calc14 = new Calculator { LoopIterations = loopIters };
            var loopTask = await calc14.ExecuteLoopTask();
            sw.Stop();
            var ltask = sw.ElapsedMilliseconds;

            Calculator.OperationCount = 0;
            sw.Restart();
            var calc15 = new Calculator { LoopIterations = loopIters };
            var loopThread = await calc15.ExecuteLoopThread();
            sw.Stop();
            var lthread = sw.ElapsedMilliseconds;

            Calculator.OperationCount = 0;
            sw.Restart();
            var calc16 = new Calculator { LoopIterations = loopIters };
            var loopAsync = await calc16.ExecuteLoopAsyncMethod();
            sw.Stop();
            var lasync = sw.ElapsedMilliseconds;

            lblLoop.Text = $"Sync: {ls} мс | Task: {ltask} мс | Thread: {lthread} мс | Async: {lasync} мс";

            MessageBox.Show("Все замеры выполнены!", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}