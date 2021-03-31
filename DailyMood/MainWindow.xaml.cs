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

namespace DailyMood
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
        private void MainButtonClick(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            Binding binding = new Binding();

            switch (index)
            {
                case 0:
                    GridSingUpPage.Visibility = Visibility.Visible;
                    GridSingInPage.Visibility = Visibility.Collapsed;
                    GridSingPage.Visibility = Visibility.Collapsed;
                    GridHomePage.Visibility = Visibility.Collapsed;


                    break;
                case 1:
                    GridSingUpPage.Visibility = Visibility.Collapsed;
                    GridSingPage.Visibility = Visibility.Collapsed;
                    GridSingInPage.Visibility = Visibility.Visible;
                    GridHomePage.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    GridSingUpPage.Visibility = Visibility.Collapsed;
                    GridSingPage.Visibility = Visibility.Visible;
                    GridSingInPage.Visibility = Visibility.Collapsed;
                    GridHomePage.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    GridSingUpPage.Visibility = Visibility.Collapsed;
                    GridSingPage.Visibility = Visibility.Collapsed;
                    GridSingInPage.Visibility = Visibility.Collapsed;
                    GridHomePage.Visibility = Visibility.Visible;
                    break;
                case 4:
                    GridJournalPage.Visibility = Visibility.Visible;
                    break;
                case 5:
                    GridJournalPage.Visibility = Visibility.Collapsed;
                    break;
                case 6:
                    GridJournalPage.Visibility = Visibility.Collapsed;
                    break;
                case 7:
                    GridJournalPage.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;


            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
    
}
