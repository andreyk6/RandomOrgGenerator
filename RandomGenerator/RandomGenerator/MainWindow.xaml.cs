using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace RandomGeneratorProblem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RandomGenerator random;

        public MainWindow()
        {
            InitializeComponent();
            //Create RandomOrg object
            random = new RandomGenerator(10, 100, 1000);
        }

        private async void MainButton_Click(object sender, RoutedEventArgs e)
        {
            //Load numbers and add to ConsoleTextBlock
            await LoadNumbers(20000);
        }
        private Task LoadNumbers(int count)
        {
            return Task.Run(() =>
            {
                //Format result string
                string resultString = "";

                this.Dispatcher.Invoke((Action)(() =>
                {
                    OutTextBlock.Text = "Loading numbers...\n";
                }));

                foreach (int n in random.GetNumbers(count))
                {
                    resultString += n + "; ";
                }

                this.Dispatcher.Invoke((Action)(() =>
                {
                    //Remove @"Loading numbers...\n" 
                    OutTextBlock.Text = OutTextBlock.Text.Substring(0, OutTextBlock.Text.Length - "Loading numbers...\n".Length);
                    //Add random numbers
                    OutTextBlock.Text += "Your numbers: \n" + resultString + "\n";
                }));
            }
            );
        }
    }
}

