using Microsoft.Office.Interop.Excel;
using SPY4et.Pages;
using SPY4et.Resourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
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
                        for (int j = 0; j < dataGrid.Items.Count && j < 15; j++)
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

                    var array = new List<int>();
                    for (int i = 0; i < dataGrid.Columns.Count; i++)
                    {
                        for (int k = 0; k <= dataGrid.Items.Count && k < 15; k++)
                        {
                            for (int j = 0; j < dataGrid.SelectedItems.Count && j < 15; j++)
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

        public static void importFromExcel(string filePath, DataGrid dataGrid)
        {
            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = excelApplication.Workbooks.Open(filePath);
            Worksheet worksheet = (Worksheet)workbook.Worksheets[1];
            Range range = worksheet.UsedRange;
            Array values = (Array)range.Cells.Value2;
            int rowsCount = values.GetLength(0);
            int columnsCount = values.GetLength(1);
            var newItemSource = dataGrid.ItemsSource.Cast<MainTable>();
            var brokenRecords = new List<string[]>();
            try
            {
                for (int row = 2; row <= rowsCount; row++)
                {
                    string[] record = new string[columnsCount];

                    for (int column = 1; column <= columnsCount; column++)
                    {
                        if (column > 1 && column < 11)
                        {
                            
                            if (values.GetValue(row, column) != null)
                            {
                                string cost = values.GetValue(row, column).ToString().Replace(" ", "").Replace("￥", "");
                                record[column - 1] = cost;
                            }
                            else
                            {
                                brokenRecords.Add(record);
                            }
                        }
                        else
                            record[column - 1] = values.GetValue(row, column).ToString();
                    }

                    var newRow = MainTable.CreateFromData(record);

                    if (newRow != null)
                    {
                        newItemSource = newItemSource.Append(newRow);
                    }
                    else
                    {
                        brokenRecords.Add(record);
                    }
                }

                dataGrid.ItemsSource = newItemSource;

                workbook.Close(true, null, null);
                excelApplication.Quit();

                if (brokenRecords.Count > 0)
                {
                    var bld = new StringBuilder().AppendLine($"{brokenRecords.Count} записей было проигнорировано (возможно, отсутствуют данные):");

                    foreach (var record in brokenRecords)
                    {
                        bld.AppendLine(string.Join("; ", record));
                    }

                    MessageBox.Show(bld.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Что-то тут не так");
            }
        }

    }
}
