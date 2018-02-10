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
        private int clickCounter = 0;

        private List<int> numbers = new List<int>();

        public Form1()
        {
            InitializeComponent();
            for(int i = 0; i < kombi.kombi.Length; i++)
            {
                kombi.kombi[i] = new BingoKombi();
            }
            debug();
            ispisi();
            ispisiCounter();


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

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int number = Int32.Parse(textBox2.Text);
                    if (number > 0 && number < 91)
                    {
                        izvuciBroj(number);
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

        private void izvuciBroj(int value)
        {   
            for(int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                for(int j = 0; j < tableLayoutPanel1.ColumnCount; j++)
                {
                    Control c = tableLayoutPanel1.GetControlFromPosition(j, i);
                    if(c != null)
                    {
                        try
                        {
                            int number = Int32.Parse(c.Text);
                            if (number > 0 && number < 91)
                            {
                                if(number == value)
                                {
                                    labelClick(c, null);
                                    textBox2.Text = "";
                                }
                            }
                        }
                        catch (FormatException ex) { }
                    }
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ispisi ()
        {
            StringBuilder labelString = new StringBuilder();

            foreach (BingoKombi itemKombi in kombi.kombi)
            {
                labelString.Append("==================\n");
                labelString.Append("||");
                labelString.Append(formatString(itemKombi.firstRow));
                labelString.Append(" ||");
                labelString.Append("\n");
                labelString.Append("||");
                labelString.Append(formatString(itemKombi.secondRow));
                labelString.Append(" ||");
                labelString.Append("\n");
                labelString.Append("||");
                labelString.Append(formatString(itemKombi.thirdRow));
                labelString.Append(" ||");
                labelString.Append("\n");
            }
            labelString.Append("==================\n");

            label1.Text = labelString.ToString();

        }

        void stvariTablicu()
        {
            int row = 0;
            int width = 30, height = 20;

            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    Control c = tableLayoutPanel1.GetControlFromPosition(j, i);

                    Label label = new Label();
                    label.Text = i + ":" + j;
                    label.Size = new Size(width, height);
                    label.TextAlign = ContentAlignment.MiddleCenter;

                    //label.Click += new EventHandler(labelClick);
                    //label.MouseDoubleClick += new MouseEventHandler(labelDoubleClick);
                    tableLayoutPanel1.Controls.Add(label, j, i);
                }
            }

            return;
            foreach (BingoKombi itemKombi in kombi.kombi)
            {
                int[] data = null;
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
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
                        int column = i < 10 ? 0 : i < 20 ? 1 : i < 30 ? 2 : i < 40 ? 3 : i < 50 ? 4 : i < 60 ? 5 : i < 70 ? 6 : i < 80 ? 7 : 8;

                        Control c = tableLayoutPanel1.GetControlFromPosition(column, row);
                        if (c != null)
                        {
                            Label label = (Label)c;
                            label.Text = i + "";
                        }
                        else
                        {
                            Label label = new Label();
                            label.Text = i + "";
                            label.Size = new Size(width, height);
                            label.TextAlign = ContentAlignment.MiddleCenter;

                            label.Click += new EventHandler(labelClick);
                            label.MouseDoubleClick += new MouseEventHandler(labelDoubleClick);
                            tableLayoutPanel1.Controls.Add(label, column, row);
                        }

                    }
                    if (j == 2)
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

        void labelClick(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if(label.Tag != null)
            {
                int tag = (int)label.Tag;
                if (tag == 99) return;
            }
            label.Tag = 99;
            Color color = Color.Green;
            if(clickCounter >= 35)
            {
                color = Color.Red;
            }
            label.BackColor = color;
            clickCounter++;
            ispisiCounter();
            try
            {
                int value = Int32.Parse(label.Text);
                numbers.Add(value);
                ispisiSveBrojeve();
            }
            catch (Exception ex) { }
        }

        void labelDoubleClick(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            int tag = (int)label.Tag;
            if (tag == 1) return;
            label.Tag = 1;
            label.BackColor = Color.Transparent;
            clickCounter--;
            ispisiCounter();
            try
            {
                int value = Int32.Parse(label.Text);
                numbers.Remove(value);
                ispisiSveBrojeve();
            }
            catch (Exception ex) { }
        }

        private void ispisiCounter()
        {
            label2.Text = "Izvuceno brojeva: " + clickCounter;
        }

        private void ispisiSveBrojeve()
        {
            label3.Text = "";
            for(int i = 0; i < numbers.Count; i++)
            {
                if(i != 0 && i % 10 == 0)
                {
                    label3.Text = label3.Text + "\n";
                }
                int value = numbers.ElementAt(i);
                if(value < 10)
                {
                    label3.Text = label3.Text + "   " + numbers.ElementAt(i);           
                }
                else
                {
                    label3.Text = label3.Text + "  " + numbers.ElementAt(i);
                }
            }
        }

        private String formatString(int[] data)
        {
            String format1 = "";
            foreach(int i in data)
            {
                if(i < 10)
                {
                    format1 = format1 + " ";
                }
                format1 = format1 + " " + i;
            }

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

            htmlBuilder.Append("<head><style>td{text-align:center;width:50px;height:30px;border: 1px solid black;}table{border-collapse: collapse;}</style></head>");

            htmlBuilder.Append("<table>");

            int itemCount = 0;
            foreach(BingoKombi item in kombi.kombi)
            {
                for(int d = 0; d < 3; d++)
                {
                    htmlBuilder.Append("<tr>");
                    List<int> lista = new List<int>();
                    if(d == 0)
                    {
                        foreach (int i in item.firstRow)
                        {
                            lista.Add(i);
                        }
                    }
                    else if(d == 1)
                    {
                        foreach (int i in item.secondRow)
                        {
                            lista.Add(i);
                        }
                    }
                    else
                    {
                        foreach (int i in item.thirdRow)
                        {
                            lista.Add(i);
                        }
                    }
                    for (int i = 0; i < 9; i++)
                    {
                        String dataS = " ";
                        if (lista.Count() > 0)
                        {
                            int value = lista.First();
                            if (value < ((i * 10) + 10))
                            {
                                dataS = lista.First().ToString();
                                lista.RemoveAt(0);
                            }
                            else if (lista.First() == 90 && i == 8)
                            {
                                dataS = "90";
                            }
                        }
                        htmlBuilder.Append("<td>" + dataS + "</td>");
                    }
                    htmlBuilder.Append("</tr>");
                }
                if(itemCount < 5) htmlBuilder.Append("<tr><td colspan='9' style='border:0px'/></tr>");
                itemCount++;
            }

            htmlBuilder.Append("</table>");

            htmlBuilder.Append("</body></html>");
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path = path + "/temp/temp.html";
            System.IO.File.WriteAllText(path, htmlBuilder.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            stvariTablicu();
        }
    }

}
