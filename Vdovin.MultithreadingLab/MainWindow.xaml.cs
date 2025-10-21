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

        private async void BtnRunAll_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtValue.Text, out int n)) return;

            // Сброс результатов
            lblSingle.Text = "Выполняется...";
            lblTask.Text = "Выполняется...";
            lblAsync.Text = "Выполняется...";
            lblTotalTime.Text = "";
            lblStatus.Text = "Запуск всех методов...";
            lblDigits.Text = "";

            var swTotal = Stopwatch.StartNew();

            // === 1. Однопоточный (запускаем первым, но он блокирует UI на время выполнения) ===
            lblStatus.Text = "Выполняется однопоточно... (UI заблокирован)";
            UpdateLayout(); // принудительно обновить интерфейс

            var sw1 = Stopwatch.StartNew();
            var result1 = ComputeFactorial(n);
            sw1.Stop();

            lblSingle.Text = $"Время: {sw1.Elapsed.TotalMilliseconds:F2} мс";
            lblDigits.Text = $"Количество цифр: {result1.ToString().Length}";

            // === 2. Через Task (фон) ===
            lblStatus.Text = "Выполняется через Task...";
            BigInteger result2 = default;
            Stopwatch sw2 = Stopwatch.StartNew();
            var task = Task.Run(() =>
            {
                result2 = ComputeFactorial(n);
                sw2.Stop();
            });

            // === 3. Асинхронно ===
            lblStatus.Text = "Выполняется асинхронно...";
            var sw3 = Stopwatch.StartNew();
            var result3 = await Task.Run(() => ComputeFactorial(n));
            sw3.Stop();

            // Ждём завершения Task
            await task;

            // Обновляем UI
            lblTask.Text = $"Время: {sw2.Elapsed.TotalMilliseconds:F2} мс";
            lblAsync.Text = $"Время: {sw3.Elapsed.TotalMilliseconds:F2} мс";

            swTotal.Stop();
            lblTotalTime.Text = $"Общее время выполнения: {swTotal.Elapsed.TotalMilliseconds:F2} мс";
            lblStatus.Text = "Все методы завершены!";
        }

        // === Общая логика вычисления ===
        private BigInteger ComputeFactorial(int n)
        {
            BigInteger result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}