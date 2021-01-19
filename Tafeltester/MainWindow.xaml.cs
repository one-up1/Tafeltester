using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tafeltester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int EQUATION_COUNT = 5;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void bAddEquations_Click(object sender, RoutedEventArgs e)
        {
            spEquations.Children.Clear();
            bCheckResults.IsEnabled = false;
            lScore.Content = "";

            //TODO: Validate max string and values < EQUATION_COUNT
            int maxValue = int.Parse(tbMaxValue.Text);

            List<int> values = new List<int>();
            for (int i = 1; i <= maxValue; i++)
            {
                values.Add(i);
            }

            Random random = new Random();
            for (int i = 1; i <= EQUATION_COUNT; i++)
            {
                int valueIndex = random.Next(0, values.Count);
                int value = values[valueIndex];
                values.RemoveAt(valueIndex);

                StackPanel spEquation = new StackPanel();
                spEquation.Orientation = Orientation.Horizontal;

                Label lEquation = new Label();
                lEquation.Content = i + " x " + value + " =";
                spEquation.Children.Add(lEquation);

                TextBox tbResult = new TextBox();
                tbResult.Width = 100;
                tbResult.Tag = i * value;
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
            for (int i = 0; i < EQUATION_COUNT; i++)
            {
                StackPanel spEquation = (StackPanel)spEquations.Children[i];
                TextBox tbResult = (TextBox)spEquation.Children[1];

                int result;
                try
                {
                    result = int.Parse(tbResult.Text);
                }
                catch
                {
                    lScore.Content = "Vul alle sommen in";
                    lScore.Foreground = Brushes.Red;
                    return;
                }

                Label lResult = (Label)spEquation.Children[2];
                if (result == (int)tbResult.Tag)
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

            lScore.Content = "Je hebt een " + Math.Round(correctResults * 10d / EQUATION_COUNT);
            lScore.Foreground = Brushes.Black;
        }
    }
}
