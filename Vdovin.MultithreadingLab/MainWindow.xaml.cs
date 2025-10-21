using System;
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

            calculator.FactorialComplete += OnFactorialComplete;
            calculator.FactorialMinusOneComplete += OnFactorialMinusOneComplete;
            calculator.AddTwoComplete += OnAddTwoComplete;
            calculator.LoopComplete += OnLoopComplete;
        }

        private async void BtnFactorial_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int value))
            {
                calculator.varFact1 = value;
                await Task.Run(() => calculator.StartFactorialAsync());
            }
        }

        private async void BtnFactorialMinus_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int value))
            {
                calculator.varFact2 = value;
                await Task.Run(() => calculator.StartFactorialMinusOneAsync());
            }
        }

        private async void BtnAddTwo_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int value))
            {
                calculator.varAddTwo = value;
                await Task.Run(() => calculator.StartAddTwoAsync());
            }
        }

        private async void BtnLoop_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int value))
            {
                calculator.varLoopValue = value;
                await Task.Run(() => calculator.StartLoopAsync());
            }
        }

        // Обработчики событий — обновление UI через Dispatcher
        private void OnFactorialComplete(BigInteger result, double total, double duration)
        {
            Dispatcher.Invoke(() => UpdateUI(result.ToString(), total, duration, result.ToString().Length));
        }

        private void OnFactorialMinusOneComplete(BigInteger result, double total, double duration)
        {
            Dispatcher.Invoke(() => UpdateUI(result.ToString(), total, duration, result.ToString().Length));
        }

        private void OnAddTwoComplete(int result, double total, double duration)
        {
            Dispatcher.Invoke(() => UpdateUI(result.ToString(), total, duration, null));
        }

        private void OnLoopComplete(double total, int count, double duration)
        {
            Dispatcher.Invoke(() => UpdateUI($"Цикл завершён ({count} итераций)", total, duration, null));
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