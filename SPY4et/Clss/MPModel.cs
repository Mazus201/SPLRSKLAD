using Microsoft.Office.Interop.Excel;
using SPY4et.Pages;
using SPY4et.Resourse;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SPY4et.Clss
{
    internal class MPModel
    {
        public static void DeleteData(DataGrid dataGrid)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dataGrid.SelectedItems.Count; i++)
                {
                    MainTable mainTable = dataGrid.SelectedItems[i] as MainTable;
                    ClsFrame.Ent.MainTable.Remove(mainTable);
                }
                ClsFrame.Ent.SaveChanges();

            }
        }

        public static void UpdateDate(DataGrid dataGrid, TabItem tab, DataGrid dataGrid1)
        {
            if (tab.IsSelected == true) //в товарах на складе 
            {
                dataGrid.ItemsSource = null;
                //MainPage.checkNullStock();
                dataGrid1.SelectedItems.Clear();
            }
        }

        public static void exportToExcel(TabItem tabItem, DataGrid dataGrid)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
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
                        for (int j = 0; j < dataGrid.Items.Count && j < 17; j++)
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

    }
}
