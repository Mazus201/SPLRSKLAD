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
                exportToExcel(TabMain, DtGrAll);

            if (TabTotal.IsSelected == true)
                exportToExcel(TabTotal, DtGrAdmTotal);

            if (TabInWay.IsSelected == true)
                exportToExcel(TabInWay, DtGrInWay);

            if (TabProcessBegin.IsSelected == true)
                exportToExcel(TabProcessBegin, DtGrProcessBegin);

            if (TabMust.IsSelected == true)
                exportToExcel(TabMust, DtGrMust);

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
            DtGrAll.SelectedItems.Clear();

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
                if (DtGrAll.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < DtGrAll.SelectedItems.Count; i++)
                    {
                        MainTable mainTable = DtGrAll.SelectedItems[i] as MainTable;
                        ClsFrame.Ent.MainTable.Remove(mainTable);
                    }
                    ClsFrame.Ent.SaveChanges();

                }
                if (DtGrAdmTotal.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < DtGrAdmTotal.SelectedItems.Count; i++)
                    {
                        MainTable mainTable = DtGrAdmTotal.SelectedItems[i] as MainTable;
                        ClsFrame.Ent.MainTable.Remove(mainTable);
                    }
                    ClsFrame.Ent.SaveChanges();

                }
                if (DtGrInWay.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < DtGrInWay.SelectedItems.Count; i++)
                    {
                        MainTable mainTable = DtGrInWay.SelectedItems[i] as MainTable;
                        ClsFrame.Ent.MainTable.Remove(mainTable);
                    }
                    ClsFrame.Ent.SaveChanges();

                }
                if (DtGrMust.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < DtGrMust.SelectedItems.Count; i++)
                    {
                        MainTable mainTable = DtGrMust.SelectedItems[i] as MainTable;
                        ClsFrame.Ent.MainTable.Remove(mainTable);
                    }
                    ClsFrame.Ent.SaveChanges();

                }
                if (DtGrProcessBegin.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < DtGrProcessBegin.SelectedItems.Count; i++)
                    {
                        MainTable mainTable = DtGrProcessBegin.SelectedItems[i] as MainTable;
                        ClsFrame.Ent.MainTable.Remove(mainTable);
                    }
                    ClsFrame.Ent.SaveChanges();

                }
                DtGrAll.SelectedItems.Clear();
            }
            catch
            {

            }
            ClsFiltr.TxbClear(TxtFind, "Поиск");

            if (TabMain.IsSelected == true) //в товарах на складе 
            {
                DtGrAll.ItemsSource = null;
                checkNullStock();
                DtGrAll.SelectedItems.Clear();
            }

            if (TabTotal.IsSelected == true) //в проданых товарах
            {
                DtGrAdmTotal.ItemsSource = null;
                checkNullStock();
                DtGrAll.SelectedItems.Clear();
            }

            if (TabInWay.IsSelected == true) //в товарах на складе 
            {
                DtGrInWay.ItemsSource = null;
                checkNullStock();
                DtGrAll.SelectedItems.Clear();
            }

            if (TabProcessBegin.IsSelected == true) //в проданых товарах
            {
                DtGrProcessBegin.ItemsSource = null;
                checkNullStock();
                DtGrAll.SelectedItems.Clear();
            }

            if (TabMust.IsSelected == true) //в товарах на складе 
            {
                DtGrMust.ItemsSource = null;
                checkNullStock();
                DtGrAll.SelectedItems.Clear();
            }

        }

        public void exportToExcel(TabItem tabItem, DataGrid dataGrid)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            if (tabItem.IsSelected == true)
            {
                if (dataGrid.SelectedItems.Count < 1)
                {

                    for (int j = 0; j < dataGrid.Columns.Count; j++)
                    {
                        Range myRange = (Range)sheet1.Cells[1, j + 1];
                        sheet1.Cells[1, j + 1].Font.Bold = true;
                        sheet1.Columns[j + 1].ColumnWidth = 15;
                        myRange.Value2 = dataGrid.Columns[j].Header;
                    }
                    for (int i = 0; i < dataGrid.Columns.Count; i++)
                    {
                        for (int j = 0;j < dataGrid.Items.Count && j < 17; j++)
                        {
                            TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                            myRange.Value2 = b.Text;
                        }
                    }

                }
                else if (dataGrid.SelectedItems.Count > 0)
                {
                    for (int j = 0; j < dataGrid.Columns.Count; j++)
                    {
                        Range myRange = (Range)sheet1.Cells[1, j + 1];
                        sheet1.Cells[1, j + 1].Font.Bold = true;
                        sheet1.Columns[j + 1].ColumnWidth = 15;
                        myRange.Value2 = dataGrid.Columns[j].Header;
                    }
                    for (int i = 0; i < dataGrid.Columns.Count; i++)
                    {
                        for (int j = 0; j < dataGrid.SelectedItems.Count && j < 17; j++)
                        {
                            TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                            myRange.Value2 = b.Text;
                        }
                    }
                }
            }
            
        }

        public HeaderFooter LeftHeader => throw new NotImplementedException();

        public HeaderFooter CenterHeader => throw new NotImplementedException();

        public HeaderFooter RightHeader => throw new NotImplementedException();

        public HeaderFooter LeftFooter => throw new NotImplementedException();

        public HeaderFooter CenterFooter => throw new NotImplementedException();

        public HeaderFooter RightFooter => throw new NotImplementedException();

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClsFrame.FrmBody.Navigate(new Auth());
        }
    }
}
