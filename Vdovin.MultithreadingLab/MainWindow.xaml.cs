using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Numerics;

namespace Vdovin.MultithreadingLab
{
    public partial class MainWindow : Window
    {
        private Calculator calculator = new Calculator();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnFactorial_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int value))
            {
                var sw = Stopwatch.StartNew();
                var result = await calculator.ComputeFactorialAsync(value);
                sw.Stop();

                UpdateUI(result.ToString(), calculator.varTotalCalculations, sw.Elapsed.TotalMilliseconds, result.ToString().Length);
            }
        }

        private async void BtnFactorialMinus_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int value))
            {
                var sw = Stopwatch.StartNew();
                var result = await calculator.ComputeFactorialMinusOneAsync(value);
                sw.Stop();

                UpdateUI(result.ToString(), calculator.varTotalCalculations, sw.Elapsed.TotalMilliseconds, result.ToString().Length);
            }
        }

        private async void BtnAddTwo_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int value))
            {
                var sw = Stopwatch.StartNew();
                var result = await calculator.ComputeAddTwoAsync(value);
                sw.Stop();

                UpdateUI(result.ToString(), calculator.varTotalCalculations, sw.Elapsed.TotalMilliseconds, null);
            }
        }

        private async void BtnLoop_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int value))
            {
                var sw = Stopwatch.StartNew();
                var total = await calculator.ComputeLoopAsync(value);
                sw.Stop();

                UpdateUI($"Цикл завершён ({value} итераций)", total, sw.Elapsed.TotalMilliseconds, null);
            }
        }

        private void UpdateUI(string status, double total, double duration, int? digitCount)
        {
            string preview = status.Length > 50 ? status.Substring(0, 50) + "..." : status;
            lblStatus.Text = preview;
            lblTotal.Text = $"Всего вычислений: {total}";
            lblDuration.Text = $"Время выполнения: {duration:F2} мс";
            lblDigits.Text = digitCount.HasValue ? $"Количество цифр: {digitCount}" : "";
        }
    }
}