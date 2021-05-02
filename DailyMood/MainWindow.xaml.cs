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
using DailyMood.Models;
using WaterBalance.Models.DataOperations;

namespace DailyMood
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int _emoji = 0;
        private int _userid;

        public MainWindow()
        {
            InitializeComponent();
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
                    GridStaticticPage.Visibility = Visibility.Collapsed;
                    GridSettingPage.Visibility = Visibility.Collapsed;
                    break;
                case 5:
                    GridStaticticPage.Visibility = Visibility.Visible;
                    GridJournalPage.Visibility = Visibility.Collapsed;
                    GridSettingPage.Visibility = Visibility.Collapsed;
                    break;
                case 6:
                    GridJournalPage.Visibility = Visibility.Collapsed;
                    GridStaticticPage.Visibility = Visibility.Collapsed;
                    GridSettingPage.Visibility = Visibility.Collapsed;
                    break;
                case 7:
                    GridJournalPage.Visibility = Visibility.Collapsed;
                    GridStaticticPage.Visibility = Visibility.Collapsed;
                    GridSettingPage.Visibility = Visibility.Visible;
                    break;
                case 8:
                    GridSingUpPage.Visibility = Visibility.Collapsed;
                    GridSingInPage.Visibility = Visibility.Collapsed;
                    GridSingPage.Visibility = Visibility.Visible;
                    GridHomePage.Visibility = Visibility.Collapsed;
                    break;
                case 9:
                    GridSingUpPage.Visibility = Visibility.Collapsed;
                    GridSingInPage.Visibility = Visibility.Collapsed;
                    GridSingPage.Visibility = Visibility.Visible;
                    GridAdminPage.Visibility = Visibility.Collapsed;
                    break;

                default:
                    break;


            }

        }

        private async void Authorization(object sender, RoutedEventArgs e)
        {
            string login = LoginFieldAuth.Text;
            string password = PasswordFieldAuth.Password;
            if (login != "" && password != "")
            {
                Loading(true);
                OperationsResponse response = await UserOperations.CheckAuthorization(login, password);
                Loading(false);

                switch(response)
                {
                    case OperationsResponse.Ok:
                        GridSingUpPage.Visibility = Visibility.Collapsed;
                        GridSingPage.Visibility = Visibility.Collapsed;
                        GridSingInPage.Visibility = Visibility.Collapsed;
                        GridHomePage.Visibility = Visibility.Visible;
                        _userid = await UserOperations.GetUserId(login);
                        LoginFieldAuth.Text = "";
                        PasswordFieldAuth.Password = "";
                        break;
                    case OperationsResponse.UserNotExists:
                        MessageBox.Show("Неверный логин или пароль");
                        break;
                    default:
                        MessageBox.Show("Необработанное исключение в функции Authorization");
                        break;
                }
            }
            else MessageBox.Show("Заполнте поля логин и пароль");
        }

        private async void Register(object sender, RoutedEventArgs e)
        {
            string login = LoginFieldReg.Text;
            string password = PasswordFieldReg.Text;
            string confirmPassword = ConfirmPasswordFieldReg.Text;

            if(login == "" || password == "" || confirmPassword == "")
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if(password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, 10);

            Loading(true);
            OperationsResponse response = await UserOperations.CreateUser(login, hashedPassword);
            Loading(false);

            switch(response)
            {
                case OperationsResponse.Ok:
                    GridSingUpPage.Visibility = Visibility.Collapsed;
                    GridSingPage.Visibility = Visibility.Collapsed;
                    GridSingInPage.Visibility = Visibility.Collapsed;
                    GridHomePage.Visibility = Visibility.Visible;
                    LoginFieldReg.Text = "";
                    PasswordFieldReg.Text = "";
                    ConfirmPasswordFieldReg.Text = "";
                    break;
                case OperationsResponse.UserExists:
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                    break;
                default:
                    MessageBox.Show("Необработанное исключение в функции Authorization");
                    break;
            }
        }

        private void Loading(bool isLoading)
        {
            if (isLoading)
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait; // set the cursor to loading spinner

                LoginFieldAuth.IsEnabled = false;
                PasswordFieldAuth.IsEnabled = false;
                LoginFieldReg.IsEnabled = false;
                PasswordFieldReg.IsEnabled = false;
                ConfirmPasswordFieldReg.IsEnabled = false;
                LogIn.IsEnabled = false;
                registration.IsEnabled = false;
            }
            else
            {
                Mouse.OverrideCursor = null; // set the cursor back to arrow

                LoginFieldAuth.IsEnabled = true;
                PasswordFieldAuth.IsEnabled = true;
                LoginFieldReg.IsEnabled = true;
                PasswordFieldReg.IsEnabled = true;
                ConfirmPasswordFieldReg.IsEnabled = true;
                LogIn.IsEnabled = true;
                registration.IsEnabled = true;
            }
        }


        private void EmojiSelect(object sender, RoutedEventArgs e)
        {
            _emoji = Int32.Parse((sender as Button).Uid);
        }

        private async void SaveNote(object sender, RoutedEventArgs e)
        {
            string history = AddNoteTexBox.Text;

            if (_emoji != 0)
            {
                OperationsResponse responce = await StatisticOperations.AddNote(history, _emoji, _userid);
                if (responce == OperationsResponse.Ok)
                {
                    MessageBox.Show("Заметка была добавлена");
                }
                else if (responce == OperationsResponse.ServerError)
                {
                    MessageBox.Show("Server error");
                }
            }
            else MessageBox.Show("Ошибка. Выберите эмоцию");
        }
    }
    
}
