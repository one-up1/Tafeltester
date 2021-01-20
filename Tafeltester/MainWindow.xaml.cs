using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tafeltester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bAddEquations_Click(object sender, RoutedEventArgs e)
        {
            spEquations.Children.Clear();
            bCheckResults.IsEnabled = false;
            lScore.Content = "";

            int maxValue;
            try
            {
                maxValue = int.Parse(tbMaxValue.Text);
            }
            catch
            {
                setError("Vul het hoogste getal in", tbMaxValue);
                return;
            }

            int equationCount;
            try
            {
                equationCount = int.Parse(tbEquationCount.Text);
            }
            catch
            {
                setError("Vul het aantal sommen in", tbMaxValue);
                return;
            }

            if (equationCount <= 0)
            {
                return;
            }
            if (equationCount > maxValue)
            {
                setError("Het aantal sommen kan niet meer zijn dan het hoogste getal", tbMaxValue);
                return;
            }

            List<int> values = new List<int>();
            for (int i = 1; i <= maxValue; i++)
            {
                values.Add(i);
            }

            Random random = new Random();
            for (int i = 1; i <= equationCount; i++)
            {
                int valueIndex = random.Next(0, values.Count);
                int value = values[valueIndex];
                values.RemoveAt(valueIndex);

                StackPanel spEquation = new StackPanel();
                spEquation.Orientation = Orientation.Horizontal;
                spEquation.Tag = i * value; // The correct answer.

                Label lEquation = new Label();
                lEquation.Content = i + " x " + value + " =";
                spEquation.Children.Add(lEquation);

                TextBox tbResult = new TextBox();
                tbResult.Width = 100;
                spEquation.Children.Add(tbResult);

                Label lResult = new Label();
                lResult.FontWeight = FontWeights.Bold;
                spEquation.Children.Add(lResult);

                spEquations.Children.Add(spEquation);
            }

            bCheckResults.IsEnabled = true;
        }

        private void bCheckResults_Click(object sender, RoutedEventArgs e)
        {
            int correctResults = 0;
            foreach (StackPanel spEquation in spEquations.Children)
            {
                TextBox tbResult = (TextBox)spEquation.Children[1];
                int result;
                try
                {
                    result = int.Parse(tbResult.Text);
                }
                catch
                {
                    setError("Vul alle sommen in", tbResult);
                    return;
                }

                Label lResult = (Label)spEquation.Children[2];
                if (result == (int)spEquation.Tag)
                {
                    correctResults++;
                    lResult.Content = "Goed";
                    lResult.Foreground = Brushes.Green;
                }
                else
                {
                    lResult.Content = "Fout";
                    lResult.Foreground = Brushes.Red;
                }
            }

            lScore.Content = "Je hebt een " + Math.Round(
                correctResults * 10d / spEquations.Children.Count);
            lScore.Foreground = Brushes.Black;
        }

        private void setError(string content, UIElement control)
        {
            lScore.Content = content;
            lScore.Foreground = Brushes.Red;
            control.Focus();
        }
    }
}
