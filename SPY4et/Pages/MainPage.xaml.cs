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
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace SPY4et.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Excel.Page
    {
        bool stockEmpty = false;
        public MainPage()
        {
            InitializeComponent();
            checkNullStock();

            ClsFiltr.TxbClear(TxtFind, "Поиск");
        }

        private void TxtFind_SelectionChanged(object sender, RoutedEventArgs e)
        {
            loadData();
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
            if (TabMain.IsSelected == true)
                MPModel.exportToExcel(TabMain, DtGrAll);

            if (TabTotal.IsSelected == true)
                MPModel.exportToExcel(TabTotal, DtGrAdmTotal);

            if (TabInWay.IsSelected == true)
                MPModel.exportToExcel(TabInWay, DtGrInWay);

            if (TabProcessBegin.IsSelected == true)
                MPModel.exportToExcel(TabProcessBegin, DtGrProcessBegin);

            if (TabMust.IsSelected == true)
                MPModel.exportToExcel(TabMust, DtGrMust);

            DtGrAll.SelectedItems.Clear();
            //3210 84955331010
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            deleteData();
        }

        private void TxtFind_GotFocus(object sender, RoutedEventArgs e)
        {
            ClsFiltr.TxbGot(TxtFind, "Поиск");
        }

        private void TxtFind_LostFocus(object sender, RoutedEventArgs e)
        {
            ClsFiltr.TxbLost(TxtFind, "Поиск");
        }

        

        public void loadData()
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
            DtGrAll.SelectedItems.Clear();
        }

        public void deleteData()
        {
            try
            {
                MPModel.DeleteData(DtGrAll);
                MPModel.DeleteData(DtGrAdmTotal);
                MPModel.DeleteData(DtGrInWay);
                MPModel.DeleteData(DtGrMust);
                MPModel.DeleteData(DtGrProcessBegin);
                
                DtGrAll.SelectedItems.Clear();
            }
            catch
            {

            }
            ClsFiltr.TxbClear(TxtFind, "Поиск");

            MPModel.UpdateDate(DtGrAll, TabMain, DtGrAll);
            checkNullStock();
            MPModel.UpdateDate(DtGrAdmTotal, TabTotal, DtGrAll);
            checkNullStock();
            MPModel.UpdateDate(DtGrInWay, TabInWay, DtGrAll);
            checkNullStock();
            MPModel.UpdateDate(DtGrProcessBegin, TabProcessBegin, DtGrAll);
            checkNullStock();
            MPModel.UpdateDate(DtGrMust, TabMust, DtGrAll);
            checkNullStock();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClsFrame.FrmBody.Navigate(new Auth());
        }

        private void DtGrAll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ClsFrame.Ent.MainTable.FirstOrDefault().Name = Convert.ToString(DtGrAdmTotal.Columns[Convert.ToInt32(DtGrAdmTotal.SelectedCells.FirstOrDefault())].GetCellContent(DtGrAdmTotal.Items[1]));
            }
            catch (Exception ex)
            {

            }
        }
        public void checkNullStock()
        {
            bool stockEmpty = false;
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
            DtGrAll.SelectedItems.Clear();

        }

        public HeaderFooter LeftHeader => throw new NotImplementedException();

        public HeaderFooter CenterHeader => throw new NotImplementedException();

        public HeaderFooter RightHeader => throw new NotImplementedException();

        public HeaderFooter LeftFooter => throw new NotImplementedException();

        public HeaderFooter CenterFooter => throw new NotImplementedException();

        public HeaderFooter RightFooter => throw new NotImplementedException();
    }
}
