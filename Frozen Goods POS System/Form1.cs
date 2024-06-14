using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frozen_Goods_POS_System
{
    public partial class main_POS : Form
    {
        public main_POS()
        {
            InitializeComponent();
        }
        
        public double Cost_Items()
        {
            Double sum = 0;
            int i = 0;

            for (i = 0; i < (dataGridView1.Rows.Count); 
                i++)
            {
                sum = sum + Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
            }
            return sum;
        }

        // this function is the formula for the cost
        private void AddCost()
        {
            Double tax, q;
            tax = 1.0;

            if (dataGridView1.Rows.Count > 0)
            {
                lblTax.Text = String.Format("\u20b1 " + "{0:0,0.00}", (((Cost_Items() * tax) / 100)));
                lblSubTotal.Text = String.Format("\u20b1 " + "{0:0,0.00}", (Cost_Items()));
                q = ((Cost_Items() * tax) / 100);

                lblTotal.Text = String.Format("\u20b1 " + "{0:0,0.00}", (Cost_Items() + q));
                
            }
        }

        // this function is the formula for the change
        private void Change()
        {
            Double tax, q, c;
            tax = 1.0;

            if (dataGridView1.Rows.Count > 0)
            {
               
                q = ((Cost_Items() * tax) / 100) + Cost_Items();
                c = Convert.ToInt32(lblCash.Text);

                lblChange.Text = String.Format("\u20b1 " + "{0:0,0.00}", c - q);
            }
        }

        // this function is used for printing the receipt
        Bitmap bitmap;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int height = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bitmap, new Rectangle(30, 165, dataGridView1.Width, dataGridView1.Height));
                printPreviewDialog1.Size = new System.Drawing.Size(425, 1000);
                printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                printPreviewDialog1.ShowDialog();
                dataGridView1.Height = Height;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawString("Frozen Goods Receipt", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(75, 10));
                e.Graphics.DrawString("Brgy. West Awang, Bugallon St. Calbayog City", new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new Point(35, 36));
                e.Graphics.DrawString("--------------------------------------------", new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new Point(9, 48));
                e.Graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(10, 75));
                e.Graphics.DrawString("Time: " + DateTime.Now.ToLongTimeString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(250, 75));
                e.Graphics.DrawString("Cashier: Joshua Advincula ", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(10, 95));
                e.Graphics.DrawString("Description: ", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(10, 125));
                e.Graphics.DrawString("This serves us Official Receipt.", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(111, 125));

                e.Graphics.DrawImage(bitmap,0, 0);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            cboPayment.Items.Add("Cash");
            cboPayment.Items.Add("Gcash");
            cboPayment.Items.Add("Paymaya");
        }

        // this function is used for Cost buttons
        private void Numbers(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (lblCash.Text == "0")
            {
                lblCash.Text = "";
                lblCash.Text = b.Text;
            }
            else if (b.Text == ".")
            {
                if (!lblCash.Text.Contains("."))
                {
                    lblCash.Text = lblCash.Text + b.Text;
                }
                
            }
            else
                lblCash.Text = lblCash.Text + b.Text;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            lblCash.Text = "0";
        }

        // this function is used to reset or when another transaction is execute
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {

                lblCash.Text = "0";
                lblChange.Text = "";
                lblTotal.Text = "";
                lblSubTotal.Text = "";
                lblTax.Text = "";
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                cboPayment.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // this function is used for removing selected items
        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
            AddCost();

            if (cboPayment.Text == "Cash")
            {
                Change();
            }
            else
            {
                lblChange.Text = "";
                lblCash.Text = "0";
            }
        }

        // this function is used if the customer will pay
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (cboPayment.Text == "Cash")
            {
                Change();
            }
            else
            {
                lblChange.Text = "";
                lblCash.Text = "0";
            }
            // shows the tax, total cost and change 
            dataGridView1.Rows.Add(" ", " ", " ");
            dataGridView1.Rows.Add(" ", "Tax: ", " " + lblTax.Text);
            dataGridView1.Rows.Add(" ", "Total: ", " " + lblTotal.Text);
            dataGridView1.Rows.Add(" ", "Change: ", " " + lblChange.Text);
            dataGridView1.Rows.Add(" " , "Pay Mode: ", cboPayment.Text + " ");

        
        }

        // Buttons for the products
        private void btnChicken_Click(object sender, EventArgs e)
        {
            Double Cost = 230;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Magnolia Chicken"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * Cost;
                    
                }
            }
            dataGridView1.Rows.Add("Magnolia Chicken", "1", string.Format("{0:0.00}", Cost));
            AddCost();
        }

        private void btnFootlong_Click(object sender, EventArgs e)
        {
            Double Cost = 165;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Star Footlong"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * Cost;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Star Footlong", "1", string.Format("{0:0.00}", Cost));
            AddCost();
        }

        private void btnFishball_Click(object sender, EventArgs e)
        {
            Double Cost = 120;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Fish Ball"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * Cost;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Fish Ball", "1", string.Format("{0:0.00}", Cost));
            AddCost();
        }

        private void btnHotdog_Click(object sender, EventArgs e)
        {
            Double Cost = 260;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Virginia Hotdog"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * Cost;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Virginia Hotdog", "1", string.Format("{0:0.00}", Cost));
            AddCost();
        }

        private void btnHam_Click(object sender, EventArgs e)
        {
            Double Cost = 230;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "PureFoods Ham"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * Cost;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("PureFoods Ham", "1", string.Format("{0:0.00}", Cost));
            AddCost();
        }

        private void btnLongga_Click(object sender, EventArgs e)
        {
            Double Cost = 270;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Skinless Longganisa"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * Cost;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Skinless Longganisa", "1", string.Format("{0:0.00}", Cost));
            AddCost();
        }

        private void btnLumpia_Click(object sender, EventArgs e)
        {
            Double Cost = 65;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Lumpia Shanghai"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * Cost;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Lumpia Shanghai", "1", string.Format("{0,7:0.00}", Cost));
            AddCost();
        }

        private void btnPatties_Click(object sender, EventArgs e)
        {
            Double Cost = 245;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "CDO Patties"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * Cost;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("CDO Patties", "1", string.Format("{0:0.00}", Cost));
            AddCost();
        }

        private void btnBelly_Click(object sender, EventArgs e)
        {
            Double Cost = 320;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Sliced Pork Belly"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * Cost;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Sliced Pork Belly", "1", string.Format("{0:0.00}", Cost));
            AddCost();
        }

        private void btnTocino_Click(object sender, EventArgs e)
        {
            Double Cost = 205;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Pork Tocino"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * Cost;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Pork Tocino", "1", string.Format("{0:0.00}", Cost));
            AddCost();
        }

        private void btnSisig_Click(object sender, EventArgs e)
        {
            Double Cost = 135;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Monterey Sisig"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * Cost;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Monterey Sisig", "1", string.Format("{0:0.00}", Cost));
            AddCost();
        }

        private void btnSiomai_Click(object sender, EventArgs e)
        {
            Double Cost = 95;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Holiday Siomai"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * Cost;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Holiday Siomai", "1", string.Format("{0,7:0.00}", Cost));
            AddCost();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult ask = MessageBox.Show("Are you sure you want to Exit?", "Warning!", MessageBoxButtons.YesNo);
            if (ask == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}



    