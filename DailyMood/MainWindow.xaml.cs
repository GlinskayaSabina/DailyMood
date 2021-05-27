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
using LiveCharts;
using LiveCharts.Wpf;
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
        private string _login = "";
        private Account _account;
        private int testNumber = 1;

        public List<Statistic> StatisticList { get; set; }
        public List<Test> Tests { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Tests = TestOperations.GetAllTests();
            TestValue.Text = Tests[testNumber - 1].Value;
        }

        private void UpdateChart()
        {
            var a = StatisticList.Select(e => e.Emoji);
            SeriesCollection c = new SeriesCollection {
                new LineSeries
                {
                    Values = new ChartValues<int>(a)
                }
            };
            Chart.Series = c;
        }

        public string Emoji
        {
            get
            {
                switch(_emoji)
                {
                    case 1:
                        return "1";
                        break;
                    case 2:
                        return "2";
                        break;
                    case 3:
                        return "3";
                        break;
                    case 4:
                        return "4";
                        break;
                    case 5:
                        return "5";
                        break;
                    default:
                        return "Выберите настроение";
                        break;
                }
            }
            set
            {
                _emoji = Int32.Parse(value);
            }
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
                    GridPersonInfo.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    GridSingUpPage.Visibility = Visibility.Collapsed;
                    GridSingPage.Visibility = Visibility.Collapsed;
                    GridSingInPage.Visibility = Visibility.Visible;
                    GridHomePage.Visibility = Visibility.Collapsed;
                    GridPersonInfo.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    GridSingUpPage.Visibility = Visibility.Collapsed;
                    GridSingPage.Visibility = Visibility.Collapsed;
                    GridSingInPage.Visibility = Visibility.Collapsed;
                    GridHomePage.Visibility = Visibility.Collapsed;
                    GridPersonInfo.Visibility = Visibility.Visible;
                    break;
                case 3:
                    GridSingUpPage.Visibility = Visibility.Collapsed;
                    GridSingPage.Visibility = Visibility.Collapsed;
                    GridSingInPage.Visibility = Visibility.Collapsed;
                    GridPersonInfo.Visibility = Visibility.Collapsed;
                    GridHomePage.Visibility = Visibility.Visible;
                    break;
                case 4:
                    GridJournalPage.Visibility = Visibility.Visible;
                    GridStaticticPage.Visibility = Visibility.Collapsed;
                    GridSettingPage.Visibility = Visibility.Collapsed;
                    GridNotesPage.Visibility = Visibility.Collapsed;
                    break;
                case 5:
                    GridStaticticPage.Visibility = Visibility.Visible;
                    GridJournalPage.Visibility = Visibility.Collapsed;
                    GridSettingPage.Visibility = Visibility.Collapsed;
                    GridNotesPage.Visibility = Visibility.Collapsed;
                    break;
                case 6:
                    GridNotesPage.Visibility = Visibility.Visible;
                    GridJournalPage.Visibility = Visibility.Collapsed;
                    GridStaticticPage.Visibility = Visibility.Collapsed;
                    GridSettingPage.Visibility = Visibility.Collapsed;
                    break;
                case 7:
                    GridJournalPage.Visibility = Visibility.Collapsed;
                    GridStaticticPage.Visibility = Visibility.Collapsed;
                    GridSettingPage.Visibility = Visibility.Visible;
                    GridNotesPage.Visibility = Visibility.Collapsed;
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
                case 10:
                    GridSingUpPage.Visibility = Visibility.Collapsed;
                    GridSingPage.Visibility = Visibility.Visible;
                    GridSingInPage.Visibility = Visibility.Collapsed;
                    GridPersonInfo.Visibility = Visibility.Collapsed;
                    GridHomePage.Visibility = Visibility.Collapsed;
                    break;              
                default:
                    break;
            }
        }

        private async void Authorization(object sender, RoutedEventArgs e)
        {
            string login = LoginFieldAuth.Text;
            _login = login;
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
                        _account = await AccountOperations.GetAccountByUserId(_userid);
                        if (_account == null)
                        {
                            await AccountOperations.CreateAccount(_userid);
                            _account = await AccountOperations.GetAccountByUserId(_userid);
                        }
                        OpenAdminPanelBtn.Visibility = _account.Role == "admin" ? Visibility.Visible : Visibility.Collapsed;
                        List<Statistic> list = await StatisticOperations.GetUserStatistics(_userid);
                        StatisticList = list != null ? list : new List<Statistic>();
                        UpdateChart();
                        PersonNameAccount.Text = _account.Name;
                        PersonTelegramAccount.Text = _account.Telegram;
                        PersonYearsAccount.Text = _account.Years.ToString();
                        Notes.ItemsSource = StatisticList;
                        LoginFieldAuth.Text = "";
                        PasswordFieldAuth.Password = "";
                        break;
                    case OperationsResponse.UserNotExists:
                        MessageBox.Show("Неверный логин или пароль");
                        break;
                    default:
                        MessageBox.Show("Server error: Authorization component.");
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

            if (login.Length > 20 || password.Length > 20)
            {
                MessageBox.Show("Длина логина и пароля не должна превышать 20 символов.");
                LoginFieldReg.Text = "";
                PasswordFieldReg.Text = "";
                ConfirmPasswordFieldReg.Text = "";
                LoginFieldReg.Focus();
                return;
            }

            if (login == "" || password == "" || confirmPassword == "")
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
            _userid = await UserOperations.GetUserId(login);

            Loading(false);

            switch(response)
            {
                case OperationsResponse.Ok:
                    GridSingUpPage.Visibility = Visibility.Collapsed;
                    GridSingPage.Visibility = Visibility.Collapsed;
                    GridSingInPage.Visibility = Visibility.Collapsed;
                    GridHomePage.Visibility = Visibility.Collapsed;
                    GridPersonInfo.Visibility = Visibility.Visible;
                    LoginFieldReg.Text = "";
                    PasswordFieldReg.Text = "";
                    ConfirmPasswordFieldReg.Text = "";
                    break;
                case OperationsResponse.UserExists:
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                    break;
                default:
                    MessageBox.Show("Server error: Authorization component.");
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

        private async void ChangeAccount(object sender, RoutedEventArgs e)
        {
            string name = PersonNameAccountTextBox.Text != "" ? PersonNameAccountTextBox.Text : _account.Name;
            int years;
            if(!Int32.TryParse(PersonYearsAccountTextBox.Text, out years)) years = _account.Years;
            string telegram = PersonTelegramAccountTextBox.Text != "" ? PersonTelegramAccountTextBox.Text : _account.Telegram;
            OperationsResponse response = await AccountOperations.EditAccount(_userid, name, years, telegram);
            if(response == OperationsResponse.Ok)
            {
                _account = await AccountOperations.GetAccountByUserId(_userid);
                PersonNameAccount.Text = _account.Name;
                PersonTelegramAccount.Text = _account.Telegram;
                PersonYearsAccount.Text = _account.Years.ToString();
            }
        }


        private void EmojiSelect(object sender, RoutedEventArgs e)
        {
            Emoji = (sender as Button).Uid;
            MyState.Text = Emoji;
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
                    StatisticList = await StatisticOperations.GetUserStatistics(_userid);
                    Notes.ItemsSource = StatisticList;
                    UpdateChart();
                }
                else if (responce == OperationsResponse.ServerError)
                {
                    MessageBox.Show("Server error");
                }
            }
            else MessageBox.Show("Ошибка. Выберите эмоцию");
        }

        private void OpenAdminPanel(object sender, RoutedEventArgs e)
        {
            AdminPanel ap = new AdminPanel();
            ap.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ap.Show();
        }

        private async void SaveAccount(object sender, RoutedEventArgs e)
        {
            string personname = PersonNameAccount.Text;
            int personyears = Int32.Parse(PersonYearsAccount.Text);
            string persontelegram = PersonTelegramAccount.Text;

            int userId = await UserOperations.GetUserId(_login);

            OperationsResponse responce = await AccountOperations.EditAccount(userId, personname, personyears, persontelegram);

            if (responce == OperationsResponse.ServerError) MessageBox.Show("Server error");

            PersonNameAccount.Text = "";
            PersonYearsAccount.Text = "";
            PersonTelegramAccount.Text = "";

        }
        private async void AddPersonalInformation(object sender, RoutedEventArgs e)
        {
            string name = AccountNameField.Text;
            int years;
            if (Int32.TryParse(AccountYearsField.Text, out int val)) years = val;
            else years = 0;
            string telegram = AccountTelegramField.Text;
            OperationsResponse response = await AccountOperations.CreateAccount(_userid, name, telegram, years);

            if (response == OperationsResponse.Ok)
            {
                GridSingUpPage.Visibility = Visibility.Collapsed;
                GridSingPage.Visibility = Visibility.Collapsed;
                GridSingInPage.Visibility = Visibility.Visible;
                GridPersonInfo.Visibility = Visibility.Collapsed;
                GridHomePage.Visibility = Visibility.Collapsed;
            }
            else MessageBox.Show("Ваш аккаунт не создался, попробуйте еще раз!");
        }

        private void TestAnswerBtn(object sender, RoutedEventArgs e)
        {
            testNumber = testNumber + 1;
            if (testNumber != Tests.Count) TestValue.Text = Tests[testNumber - 1].Value;
            else
            {
                MessageBox.Show("Вы прошли тест! Результаты: Вы довольно беспокойный человек.Чем больше у вас баллов, тем более выражен ваш персональный уровень беспокойства.Вы должны понимать, что нельзя изменить вашу индивидуальную степень подверженности стрессам, но можно в значительной степени защитить себя от психотравмирующих воздействий и раздражителей, если научиться правильно расслабляться и мыслить, или просто подавлять свои страхи.Один из самых простых способов расслаблять свой мозг - это научиться мечтать.Также полезно переключатся из одного вида деятельности в другой, иногда даже совсем противоположный первому.Надо уметь выделять время для отдыха и время, когда вы будете наедине с собой.Овладев этими способами расслабления и стабилизации своего напряжения, вы сможете справляться со многими стрессовыми ситуациями, прибегая каждый раз к помощи скрытых ресурсов своего организма.");
                testNumber = 1;
                TestValue.Text = Tests[testNumber - 1].Value;
            }
        }
    }   
}
