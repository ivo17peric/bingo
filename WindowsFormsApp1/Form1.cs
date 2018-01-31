using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        private Bingo kombi = new Bingo();
        private int step = 0;

        public Form1()
        {
            InitializeComponent();
            for(int i = 0; i < kombi.kombi.Length; i++)
            {
                kombi.kombi[i] = new BingoKombi();
            }
            debug();
            ispisi();

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int number = Int32.Parse(textBox1.Text);
                    if (number > 0 && number < 91)
                    {
                        numberAdd(number);
                    }
                }
                catch (FormatException ex) { }
            }
        }

        private void numberAdd(int value)
        {
            if(step % 15 < 5)
            {
                kombi.kombi[step / 15].firstRow[step % 15] = value;
            }
            else if(step % 15 >= 5 && step % 15 < 10)
            {
                kombi.kombi[step / 15].secondRow[step % 15 - 5] = value;
            }
            else if (step % 15 >= 10 && step % 15 < 15)
            {
                kombi.kombi[step / 15].thirdRow[step % 15 - 10] = value;
            }
            step++;

            ispisi();

            textBox1.ResetText();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ispisi ()
        {
            StringBuilder labelString = new StringBuilder();

            foreach (BingoKombi itemKombi in kombi.kombi)
            {
                labelString.Append("==========================\n");
                labelString.Append("||");
                labelString.Append(formatString(itemKombi.firstRow));
                labelString.Append("||");
                labelString.Append("\n");
                labelString.Append("||");
                labelString.Append(formatString(itemKombi.secondRow));
                labelString.Append("||");
                labelString.Append("\n");
                labelString.Append("||");
                labelString.Append(formatString(itemKombi.thirdRow));
                labelString.Append("||");
                labelString.Append("\n");
                labelString.Append("\n======================\n");
                labelString.Append("\n");
            }

            label1.Text = labelString.ToString();

            int row = 0;
            int width = 30, height = 20;
            
            foreach (BingoKombi itemKombi in kombi.kombi)
            {
                int[] data = null;
                for (int j = 0; j < 3; j++)
                {
                    if(j == 0)
                    {
                        data = itemKombi.firstRow;
                    }
                    else if (j == 1)
                    {
                        data = itemKombi.secondRow;
                    }
                    else if (j == 2)
                    {
                        data = itemKombi.thirdRow;
                    }

                    foreach (int i in data)
                    {
                        Label label = new Label();
                        label.Text = i + "";
                        label.Size = new Size(width, height);
                        label.TextAlign = ContentAlignment.MiddleCenter;
                        int column = i < 10 ? 0 : i < 20 ? 1 : i < 30 ? 2 : i < 40 ? 3 : i < 50 ? 4 : i < 60 ? 5 : i < 70 ? 6 : i < 80 ? 7 : 8;
                        tableLayoutPanel1.Controls.Add(label, column, row);
                    }
                    if(j == 2)
                    {
                        row = row + 2;
                    }
                    else
                    {
                        row++;
                    }
                }
        
            }

        }

        private String formatString(int[] data)
        {
            String format1 = "  A BB CC DD EE FF GG HH II ";
            foreach (int item in data)
            {
                if (item == 0)
                {

                }
                else if (item < 10)
                {
                    format1 = format1.Replace("A", item.ToString());
                }
                else if (item < 20)
                {
                    format1 = format1.Replace("BB", item.ToString());
                }
                else if (item < 30)
                {
                    format1 = format1.Replace("CC", item.ToString());
                }
                else if (item < 40)
                {
                    format1 = format1.Replace("DD", item.ToString());
                }
                else if (item < 50)
                {
                    format1 = format1.Replace("EE", item.ToString());
                }
                else if (item < 60)
                {
                    format1 = format1.Replace("FF", item.ToString());
                }
                else if (item < 70)
                {
                    format1 = format1.Replace("GG", item.ToString());
                }
                else if (item < 80)
                {
                    format1 = format1.Replace("HH", item.ToString());
                }
                else if (item <= 90)
                {
                    format1 = format1.Replace("II", item.ToString());
                }
            }
            format1 = format1.Replace("A", " ").Replace("B", " ").Replace("C", " ").Replace("D", " ")
                    .Replace("E", " ").Replace("F", " ").Replace("G", " ").Replace("H", " ").Replace("I", " ");

            return format1;
        }

        private void debug()
        {
            kombi.kombi[0].firstRow = new int[] { 23, 48, 51, 64, 84 };
            kombi.kombi[0].secondRow = new int[] { 7, 25, 46, 60, 70 };
            kombi.kombi[0].thirdRow = new int[] { 2, 11, 30, 77, 90 };

            kombi.kombi[1].firstRow = new int[] { 15, 27, 43, 54, 62 };
            kombi.kombi[1].secondRow = new int[] { 5, 10, 44, 56, 75 };
            kombi.kombi[1].thirdRow = new int[] { 38, 53, 63, 79, 87 };

            kombi.kombi[2].firstRow = new int[] { 14, 26, 40, 55, 88 };
            kombi.kombi[2].secondRow = new int[] { 29, 39, 45, 61, 86 };
            kombi.kombi[2].thirdRow = new int[] { 8, 22, 49, 76, 85 };

            kombi.kombi[3].firstRow = new int[] { 13, 20, 36, 69, 80 };
            kombi.kombi[3].secondRow = new int[] { 3, 37, 47, 57, 67 };
            kombi.kombi[3].thirdRow = new int[] { 9, 21, 34, 71, 83 };

            kombi.kombi[4].firstRow = new int[] { 4, 18, 24, 32, 59 };
            kombi.kombi[4].secondRow = new int[] { 19, 33, 66, 72, 89 };
            kombi.kombi[4].thirdRow = new int[] { 12, 31, 42, 58, 65 };

            kombi.kombi[5].firstRow = new int[] { 35, 41, 52, 68, 73 };
            kombi.kombi[5].secondRow = new int[] { 1, 16, 50, 78, 81 };
            kombi.kombi[5].thirdRow = new int[] { 6, 17, 28, 74, 82 };
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if(e.Row % 4 == 3)
            {
                using (SolidBrush brush = new SolidBrush(Color.Black))
                    e.Graphics.FillRectangle(brush, e.CellBounds);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<html><body>");

            htmlBuilder.Append("<table border='1'>");

            foreach(BingoKombi item in kombi.kombi)
            {
                htmlBuilder.Append("<tr>");
                List<int> lista = new List<int>();
                foreach(int i in item.firstRow)
                {
                    lista.Add(i);
                }
                for(int i = 0; i < 9; i++)
                {
                    String dataS = "-";
                    if(lista.First() < 0 * 10 + 10)
                    {
                        dataS = lista.First().ToString();
                        lista.RemoveAt(0);
                    }else if(lista.First() == 90 && i == 8)
                    {
                        dataS = "90";
                    }
                    htmlBuilder.Append("<td>" + dataS + "</td>");
                }
                htmlBuilder.Append("</tr>");
                foreach (int i in item.secondRow)
                {
                    htmlBuilder.Append("<td>" + i + "</td>");
                }
                htmlBuilder.Append("</tr>");
                foreach (int i in item.thirdRow)
                {
                    htmlBuilder.Append("<td>" + i + "</td>");
                }
                htmlBuilder.Append("</tr>");
            }

            htmlBuilder.Append("</table>");

            htmlBuilder.Append("</body></html>");

            System.IO.File.WriteAllText(@"C:\Users\ana_travica\Desktop\temp\tem.html", htmlBuilder.ToString());

        }
    }

}
