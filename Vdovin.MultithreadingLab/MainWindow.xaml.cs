using System;
using System.Threading;
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

        private void BtnFactorial_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int value))
            {
                calculator.varFact1 = value;
                Thread t = new Thread(calculator.Factorial);
                t.IsBackground = true;
                t.Start();
            }
        }

        private void BtnFactorialMinus_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int value))
            {
                calculator.varFact2 = value;
                Thread t = new Thread(calculator.FactorialMinusOne);
                t.IsBackground = true;
                t.Start();
            }
        }

        private void BtnAddTwo_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int value))
            {
                calculator.varAddTwo = value;
                Thread t = new Thread(calculator.AddTwo);
                t.IsBackground = true;
                t.Start();
            }
        }

        private void BtnLoop_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int value))
            {
                calculator.varLoopValue = value;
                Thread t = new Thread(calculator.RunALoop);
                t.IsBackground = true;
                t.Start();
            }
        }

        private void OnFactorialComplete(BigInteger result, double total, double duration)
        {
            Dispatcher.Invoke(() =>
            {
                // Не выводим всё число — только длину и пример
                string preview = result.ToString();
                if (preview.Length > 50)
                    preview = preview.Substring(0, 50) + "...";

                lblStatus.Text = $"Факториал: {preview}";
                lblDigits.Text = $"Количество цифр: {result.ToString().Length}";
                lblTotal.Text = $"Всего вычислений: {total}";
                lblDuration.Text = $"Время выполнения: {duration:F2} мс";
            });
        }

        private void OnFactorialMinusOneComplete(BigInteger result, double total, double duration)
        {
            Dispatcher.Invoke(() =>
            {
                string preview = result.ToString();
                if (preview.Length > 50)
                    preview = preview.Substring(0, 50) + "...";

                lblStatus.Text = $"Факториал-1: {preview}";
                lblDigits.Text = $"Количество цифр: {result.ToString().Length}";
                lblTotal.Text = $"Всего вычислений: {total}";
                lblDuration.Text = $"Время выполнения: {duration:F2} мс";
            });
        }

        private void OnAddTwoComplete(int result, double total, double duration)
        {
            Dispatcher.Invoke(() =>
            {
                lblStatus.Text = $"Прибавить 2: {result}";
                lblDigits.Text = "";
                lblTotal.Text = $"Всего вычислений: {total}";
                lblDuration.Text = $"Время выполнения: {duration:F2} мс";
            });
        }

        private void OnLoopComplete(double total, int count, double duration)
        {
            Dispatcher.Invoke(() =>
            {
                lblStatus.Text = $"Цикл завершён ({count} итераций)";
                lblDigits.Text = "";
                lblTotal.Text = $"Всего вычислений: {total}";
                lblDuration.Text = $"Время выполнения: {duration:F2} мс";
            });
        }
    }
}