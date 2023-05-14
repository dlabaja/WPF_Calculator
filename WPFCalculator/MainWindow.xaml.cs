using System;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using PostfixCalculator = WPFCalculator.PostfixCalculator;

namespace WPFCalculator
{
    public partial class MainWindow
    {
        private string _ans = "0";
        private const string operators = "+-*/%";
        private string[] constants = {"π", "e", "Ans"};

        private void WriteToInput(string val)
        {
            Input.Text += val;
        }

        private void WriteFuncConst(object sender, RoutedEventArgs e)
        {
            if (operators.Contains(GetLastChar()) || GetLastChar() == default || GetLastChar() == '(')
                WriteToInput(((Button) sender).Tag.ToString());
        }

        private void WriteAnything(object sender, RoutedEventArgs e)
        {
            WriteToInput(((Button) sender).Tag.ToString());
        }

        private void WriteDecimalPoint(object sender, RoutedEventArgs e)
        {
            if (char.IsDigit(GetLastChar()))
                WriteToInput(".");
        }

        private void WriteOperator(object sender, RoutedEventArgs e)
        {
            if (!operators.Contains(GetLastChar()))
                WriteToInput(((Button) sender).Tag.ToString());
        }

        private void SendToServer(object sender, RoutedEventArgs e)
        {
            Input.Text = Input.Text.Replace("Ans", _ans);
            Output.Text = new PostfixCalculator().CalculatePostfix(
                new PostfixCalculator().InfixToPostfix(new StringBuilder(Input.Text))).ToString();
            _ans = Output.Text;
        }

        private void DeleteChar(object sender, RoutedEventArgs e)
        {
            if (Input.Text.Length == 0) return;
            Input.Text = Input.Text.Substring(0, Input.Text.Length - 1);
        }

        private void ClearInput(object sender, RoutedEventArgs e)
        {
            Input.Text = "";
        }

        private char GetLastChar()
        {
            return Input.Text.Length == 0 ? default : Input.Text[Input.Text.Length - 1];
        }
    }
}