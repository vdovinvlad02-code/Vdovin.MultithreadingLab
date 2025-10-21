using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Vdovin.MultithreadingLab
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // === 1. Однопоточный (блокирует UI) ===
        private void BtnSingleThread_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtValue.Text, out int n)) return;

            lblStatus.Text = "Выполняется однопоточно... (UI заблокирован)";
            UpdateLayout(); // принудительно обновить интерфейс

            var sw = Stopwatch.StartNew();
            var result = ComputeFactorial(n);
            sw.Stop();

            lblSingle.Text = $"Результат: {Preview(result)}\nВремя: {sw.Elapsed.TotalMilliseconds:F2} мс";
            lblStatus.Text = "Готово (однопоточно)";
        }

        // === 2. Через Task (фон, без async/await) ===
        private void BtnTask_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtValue.Text, out int n)) return;

            lblStatus.Text = "Выполняется через Task...";
            Task.Run(() =>
            {
                var sw = Stopwatch.StartNew();
                var result = ComputeFactorial(n);
                sw.Stop();

                Dispatcher.Invoke(() =>
                {
                    lblTask.Text = $"Результат: {Preview(result)}\nВремя: {sw.Elapsed.TotalMilliseconds:F2} мс";
                    lblStatus.Text = "Готово (через Task)";
                });
            });
        }

        // === 3. Асинхронно (async/await) ===
        private async void BtnAsync_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtValue.Text, out int n)) return;

            lblStatus.Text = "Выполняется асинхронно...";
            var sw = Stopwatch.StartNew();
            var result = await Task.Run(() => ComputeFactorial(n));
            sw.Stop();

            lblAsync.Text = $"Результат: {Preview(result)}\nВремя: {sw.Elapsed.TotalMilliseconds:F2} мс";
            lblStatus.Text = "Готово (асинхронно)";
        }

        // === Общая логика вычисления ===
        private BigInteger ComputeFactorial(int n)
        {
            BigInteger result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
                // Имитация нагрузки (если нужно)
                // Thread.Sleep(0); // не обязательно
            }
            return result;
        }

        // === Вспомогательный метод для сокращения вывода ===
        private string Preview(BigInteger value)
        {
            string s = value.ToString();
            return s.Length > 50 ? s.Substring(0, 50) + "..." : s;
        }
    }
}