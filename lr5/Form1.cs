using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR_5_Vados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            double x1min = double.Parse(tbx1min.Text);
            double x1max = double.Parse(tbx2max.Text);
            double x2min = double.Parse(tbx1min.Text);
            double x2max = double.Parse(tbx2max.Text);
            double dx1 = double.Parse(tbdx1.Text);
            double dx2 = double.Parse(tbdx2.Text);

            gv.ColumnCount = (int)Math.Truncate((x2max - x2min) / dx2) + 2;
            gv.RowCount = (int)Math.Truncate((x1max - x1min) / dx1) + 2;

            for (int i = 0; i < gv.RowCount - 1; i++)
                gv.Rows[i].HeaderCell.Value = (x1min + i * dx1).ToString("0.000");
            gv.RowHeadersWidth = 80;
            for (int i = 0; i < gv.ColumnCount - 1; i++)
            {
                gv.Columns[i].HeaderCell.Value = (x2min + i *
                dx2).ToString("0.000");
                gv.Columns[i].Width = 60;
            }

            int cl, rw;
            double x1, x2, y;

            rw = 0;
            x1 = x1min;
            double id_zav = 0;
            while (x1 <= x1max)
            {
                x2 = x2min;
                cl = 0;
                while (x2 <= x2max)
                {
                    y = Math.Sin(x1) - Math.Pow(x2, 3) + Math.Sqrt(x1) - 1.3 * Math.Pow(x1, 3);
                    if (y < 0)
                        id_zav += Math.Pow(y, 2);
                    gv.Rows[rw].Cells[cl].Value = y.ToString("0.000");
                    x2 += dx2;
                    cl++;
                }
                x1 += dx1;
                rw++;
            }
            //підрахунок суми відповідних рядків та стовпців
            gv.Rows[gv.RowCount - 1].HeaderCell.Value = "Сумма";
            gv.Columns[gv.ColumnCount - 1].HeaderCell.Value = "Сумма";
            double sum = 0;
            for (int j = 0; j < gv.RowCount - 1; j++)
            {
                for (int i = 0; i < gv.ColumnCount - 1; i++)
                    sum += Convert.ToDouble(gv.Rows[j].Cells[i].Value);
                gv.Rows[j].Cells[gv.ColumnCount - 1].Value = sum.ToString("0.000");
                sum = 0;
            }
            for (int i = 0; i < gv.ColumnCount; i++)
            {
                for (int j = 0; j < gv.RowCount - 1; j++)
                    sum += Convert.ToDouble(gv.Rows[j].Cells[i].Value);
                gv.Rows[gv.RowCount - 1].Cells[i].Value = sum.ToString("0.000");
                sum = 0;
            }
            lab_id_zav.Text = "Сума квадратів всіх від'ємних значень = " + Convert.ToString(id_zav);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbx1min.Text = "";
            label3.Text = "";
            tbx2min.Text = "";
            label4.Text = "";
            tbdx1.Text = "";
            tbdx2.Text = "";
            gv.Rows.Clear();
            for (int Cl = 0; Cl < gv.ColumnCount; Cl++)
                gv.Columns[Cl].HeaderCell.Value = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрити програму?", "Вихід з програми", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                Application.Exit();
        }
    }
}
