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
            gEquations.RowDefinitions.Clear();
            gEquations.Children.Clear();
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
            for (int i = 0; i < equationCount; i++)
            {
                int number = i + 1;
                int valueIndex = random.Next(0, values.Count);
                int value = values[valueIndex];
                values.RemoveAt(valueIndex);

                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                gEquations.RowDefinitions.Add(row);
                
                Label lEquation = new Label();
                lEquation.SetValue(Grid.RowProperty, i);
                lEquation.SetValue(Grid.ColumnProperty, 0);
                lEquation.Content = number + " x " + value + " =";
                gEquations.Children.Add(lEquation);

                TextBox tbResult = new TextBox();
                tbResult.SetValue(Grid.RowProperty, i);
                tbResult.SetValue(Grid.ColumnProperty, 1);
                tbResult.Margin = new Thickness(2);
                gEquations.Children.Add(tbResult);

                Label lResult = new Label();
                lResult.SetValue(Grid.RowProperty, i);
                lResult.SetValue(Grid.ColumnProperty, 2);
                lResult.FontWeight = FontWeights.Bold;
                lResult.Tag = number * value; // The correct answer.
                gEquations.Children.Add(lResult);
            }

            bCheckResults.IsEnabled = true;
        }

        private void bCheckResults_Click(object sender, RoutedEventArgs e)
        {
            int correctResults = 0;
            for (int i = 0; i < gEquations.RowDefinitions.Count; i++)
            {
                int index = i * 3;

                TextBox tbResult = (TextBox)gEquations.Children[index + 1];
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

                Label lResult = (Label)gEquations.Children[index + 2];
                if (result == (int)lResult.Tag)
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

            int score = (int)Math.Round(correctResults * 10d / gEquations.RowDefinitions.Count);
            if (score == 0)
            {
                // Je kunt geen 0 halen.
                score = 1;
            }

            lScore.Content = "Je hebt een " + score;
            lScore.Foreground = Brushes.Black;
        }

        private void setError(string content, TextBox textBox)
        {
            lScore.Content = content;
            lScore.Foreground = Brushes.Red;

            textBox.SelectAll();
            textBox.Focus();
        }
    }
}
