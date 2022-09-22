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
using SPY4et.Clss;
using SPY4et.Pages;

namespace SPY4et
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool SizeWindow { get; private set; }
        public MainWindow()
        {
            InitializeComponent();

            ClsFrame.FrmBody = FrmCenter;
            FrmCenter.Navigate(new MainPage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrmCenter.GoBack();
            }
            catch
            {
                this.Close();
            }
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (SizeWindow == false) //проверяем открыто ли окно на весь экран
            {
                this.WindowState = WindowState.Maximized; //если нет, то увеличиваем
                SizeWindow = true;
            }
            else //и наоборот
            {
                this.WindowState = WindowState.Normal;
                SizeWindow = false;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch
            {

            }
        }
    }
}
