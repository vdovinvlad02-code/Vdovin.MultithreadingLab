using System;
using System.Threading;
using System.Windows;

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

        private void OnFactorialComplete(double result, double total)
        {
            Dispatcher.Invoke(() =>
            {
                lblStatus.Text = $"Факториал: {result}";
                lblTotal.Text = $"Всего вычислений: {total}";
            });
        }

        private void OnFactorialMinusOneComplete(double result, double total)
        {
            Dispatcher.Invoke(() =>
            {
                lblStatus.Text = $"Факториал-1: {result}";
                lblTotal.Text = $"Всего вычислений: {total}";
            });
        }

        private void OnAddTwoComplete(int result, double total)
        {
            Dispatcher.Invoke(() =>
            {
                lblStatus.Text = $"Прибавить 2: {result}";
                lblTotal.Text = $"Всего вычислений: {total}";
            });
        }

        private void OnLoopComplete(double total, int count)
        {
            Dispatcher.Invoke(() =>
            {
                lblStatus.Text = $"Цикл завершён ({count} итераций)";
                lblTotal.Text = $"Всего вычислений: {total}";
            });
        }
    }
}