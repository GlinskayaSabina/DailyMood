using DailyMood.Models;
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
using System.Windows.Shapes;
using WaterBalance.Models.DataOperations;

namespace DailyMood
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public List<Account> Accounts_;
        public AdminPanel()
        {
            
            InitializeComponent();
            Accounts.ItemsSource = AccountOperations.GetAllAccounts();
            Tests.ItemsSource = TestOperations.GetAllTests();
        }

        private async void EditAccount(object sender, RoutedEventArgs e)
        {
            int userId = Int32.Parse((sender as Button).Uid);
            Account account = await AccountOperations.GetAccountByUserId(userId);
            EditAccount window = new EditAccount(account, ref Accounts);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }


        private async void DeleteTest(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse((sender as Button).Uid);
            await TestOperations.DeleteTest(id);
            Tests.ItemsSource = TestOperations.GetAllTests();
            
        }

        private async void AddQuestion(object sender, RoutedEventArgs e)
        {
            string question = TestInputField.Text;
            if (question == "")
            {
                MessageBox.Show("Введите вопрос!");
                return;
            }

            OperationsResponse response = await TestOperations.CreateTest(question);
            if (response != OperationsResponse.Ok) MessageBox.Show("Серверная ошибка. Не получилось добавить вопрос!");
            Tests.ItemsSource = TestOperations.GetAllTests();
            TestInputField.Text = "";
        }

        private void OpenChangeAccountsWindow(object sender, RoutedEventArgs e)
        {
            ChangeAccountsWindow.Visibility = Visibility.Visible;
            ChangeTestsWindow.Visibility = Visibility.Collapsed;
        }

        private void OpenChangeTestsWindow(object sender, RoutedEventArgs e)
        {
            ChangeAccountsWindow.Visibility = Visibility.Collapsed;
            ChangeTestsWindow.Visibility = Visibility.Visible;
        }
    }
}
