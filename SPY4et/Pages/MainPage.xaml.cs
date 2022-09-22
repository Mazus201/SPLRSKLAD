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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        bool stockEmpty = false;
        public MainPage()
        {
            InitializeComponent();
            checkNullStock();

            ClsFiltr.TxbClear(TxtFind, "Поиск");
        }

        //DtGrAll.ItemsSource = ClsFrame.Ent.MainTable.ToList();
        //        DtGrInWay.ItemsSource = ClsFrame.Ent.MainTable.Where(x => x.Status == "в пути").ToList();
        //DtGrMust.ItemsSource = ClsFrame.Ent.MainTable.Where(x => x.Status == "не заказано").ToList();
        //DtGrProcessBegin.ItemsSource = ClsFrame.Ent.MainTable.Where(x => x.Status == "оценено").ToList();
        //DtGrAdmTotal.ItemsSource = ClsFrame.Ent.MainTable.Where(x => x.Status == "достаточно").ToList();

        private void TxtFind_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (TabMain.IsSelected == true && TxtFind.Text != "Поиск") //в товарах на складе 
                DtGrAll.ItemsSource = ClsFrame.Ent.MainTable.Where(x => x.Name.Contains(TxtFind.Text)).ToList();

            if (TabTotal.IsSelected == true && TxtFind.Text != "Поиск") //в проданых товарах
                DtGrAdmTotal.ItemsSource = ClsFrame.Ent.MainTable.Where(x => x.Name.Contains(TxtFind.Text) && x.Status == "достаточно").ToList();
            
            if (TabInWay.IsSelected == true && TxtFind.Text != "Поиск") //в товарах на складе 
                DtGrInWay.ItemsSource = ClsFrame.Ent.MainTable.Where(x => x.Name.Contains(TxtFind.Text) && x.Status == "в пути").ToList();

            if (TabProcessBegin.IsSelected == true && TxtFind.Text != "Поиск") //в проданых товарах
                DtGrProcessBegin.ItemsSource = ClsFrame.Ent.MainTable.Where(x => x.Name.Contains(TxtFind.Text) && x.Status == "оценено").ToList();

            if (TabMust.IsSelected == true && TxtFind.Text != "Поиск") //в товарах на складе 
                DtGrMust.ItemsSource = ClsFrame.Ent.MainTable.Where(x => x.Name.Contains(TxtFind.Text) && x.Status == "не заказано").ToList();                       

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            checkNullStock();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DtGrAll.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < DtGrAll.SelectedItems.Count; i++)
                    {
                        MainTable mainTable = DtGrAll.SelectedItems[i] as MainTable;
                        ClsFrame.Ent.MainTable.Remove(mainTable);
                    }
                    ClsFrame.Ent.SaveChanges();
                }
            }
            catch
            {

            }
            ClsFiltr.TxbClear(TxtFind, "Поиск");
        }

        private void TxtFind_GotFocus(object sender, RoutedEventArgs e)
        {
            ClsFiltr.TxbGot(TxtFind, "Поиск");
        }

        private void TxtFind_LostFocus(object sender, RoutedEventArgs e)
        {
            ClsFiltr.TxbLost(TxtFind, "Поиск");
        }

        private void chkEpty_Checked(object sender, RoutedEventArgs e)
        {
            if (chkEmpty.IsChecked == true)
                stockEmpty = true;
        }
        private void chkEmpty_Unchecked(object sender, RoutedEventArgs e)
        {
            if (chkEmpty.IsChecked == false)
                stockEmpty = false;
        }

        public void checkNullStock()
        {
            if (stockEmpty == true)
                DtGrAll.ItemsSource = ClsFrame.Ent.MainTable.Where(x => x.Count < 1).ToList();

            else if (stockEmpty == false)
            {
                DtGrAll.ItemsSource = ClsFrame.Ent.MainTable.ToList();
                DtGrInWay.ItemsSource = ClsFrame.Ent.MainTable.Where(x => x.Status == "в пути").ToList();
                DtGrMust.ItemsSource = ClsFrame.Ent.MainTable.Where(x => x.Status == "не заказано").ToList();
                DtGrProcessBegin.ItemsSource = ClsFrame.Ent.MainTable.Where(x => x.Status == "оценено").ToList();
                DtGrAdmTotal.ItemsSource = ClsFrame.Ent.MainTable.Where(x => x.Status == "достаточно").ToList();
            }

        }

    }
}
