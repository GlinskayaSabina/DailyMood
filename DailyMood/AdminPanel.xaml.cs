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
        }

        private async void AddNewLine()
        {
            
            // стягиваешь поля

            // OperationsResponse response = await AccountOperations.CreateAccount(userId, Name, phone, dateTime);
            Accounts.ItemsSource = AccountOperations.GetAllAccounts();
        }

    }
}
