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
using SPY4et.Resourse;
using SPY4et.Clss;

namespace SPY4et.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
        }
        int CountTry = 0;

        private void TxbLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            ClsFiltr.TxbGot(TxbLogin, "Инспектор");
        }

        private void TxbLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            ClsFiltr.TxbLost(TxbLogin, "Инспектор");
        }

        private void BtnAuth_Click(object sender, RoutedEventArgs e)
        {
            var user = ClsFrame.EntUser.User.FirstOrDefault(x => x.Password == TxbPass.Password && x.Login == TxbLogin.Text);

            if (TxbLogin.Text == "Инспектор" /*|| TxbPass.Text == "Пароль"*/)
            {
                BdLogin.Visibility = Visibility.Visible;
                BdPass.Visibility = Visibility.Visible;
                CountTry++;
                //if (CountTry == 3)
                //{
                //    ClsFiltr.FuncError("Вы не ввели данные!");
                //    ClsFiltr.TxbClear(TxbLogin, "Инспектор");
                //    //ClsFiltr.TxbClear(TxbPass, "Пароль");
                //    CountTry = 0;
                //}
            }

            else if (user == null)
            {
                BdLogin.Visibility = Visibility.Visible;
                BdPass.Visibility = Visibility.Visible;
                CountTry++;

                //if (CountTry == 3)
                //{
                //    Properties.Settings.Default.SaveData += 1;
                //    WinBlock block = new WinBlock();
                //    block.ShowDialog();
                //    CountTry = 0;
                //}
            }
            else
            {
                ClsFrame.FrmBody.Navigate(new MainPage());
                BdLogin.Visibility = Visibility.Hidden;
                BdPass.Visibility = Visibility.Hidden;
                CountTry = 0;
            }

            ClsFiltr.TxbClear(TxbLogin, "Инспектор");
            TxbPass.Clear();
            //ClsFiltr.TxbClear(TxbPass, "Пароль");
        }
    }
}
