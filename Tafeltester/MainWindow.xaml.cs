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
                int index = random.Next(0, values.Count);
                int value = values[index];
                values.RemoveAt(index);

                Label lEquation = new Label();
                lEquation.Content = i + " x " + value + " =";
                spEquations.Children.Add(lEquation);
            }
        }
    }
}
