using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SPY4et.Pages;

namespace SPY4et.Clss
{
    internal class ClsFiltr
    {
        public static bool CorrectData;

        public static void TxbGot(TextBox textBox, string Text)
        {
            if (textBox.Text == Text)
            {
                textBox.Text = null;
                textBox.Foreground = Brushes.Black;
            }
        }
        public static void TxbLost(TextBox textBox, string Text)
        {
            if (textBox.Text == "")
            {
                textBox.Text = Text;
                textBox.Foreground = Brushes.LightGray;
            }
        }

        public static void TxbClear(TextBox textBox1, string Text)
        {
            textBox1.Foreground = Brushes.LightGray;
            textBox1.Text = Text;
        }

        public static void TxbBorder(TextBox textBox, Border border, string Text)
        {
            if (textBox.Text == Text)
            {
                border.Visibility = Visibility.Visible;
                CorrectData = false;
            }
            else
                CorrectData = true;
        }
    }
}
