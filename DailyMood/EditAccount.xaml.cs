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
    /// Interaction logic for EditAccount.xaml
    /// </summary>
    public partial class EditAccount : Window
    {
        private Account _account;
        private ItemsControl _accounts;
        public EditAccount(Account account, ref ItemsControl Accounts)
        {
            InitializeComponent();
            _accounts = Accounts;
            _account = account;
            Name.Text = account.Name;
            Years.Text = account.Years.ToString();
            Telegram.Text = account.Telegram;
        }

        private async void EditAccountEvent(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            int years;
            if(!Int32.TryParse(Years.Text, out years)) years = _account.Years;
            string telegram = Telegram.Text;
            await AccountOperations.EditAccount(_account.UserId, name, years, telegram);
            _accounts.ItemsSource = AccountOperations.GetAllAccounts();
            MessageBox.Show("Изменения сохранены!");
            this.Close();
        }
    }
}
