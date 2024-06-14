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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        // Proceed if the user input is correct, else a message box will show
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtBoxUser.Text == "Admin" && txtBoxPass.Text == "Admin1234")
            {
                new LoadingScreen().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong User or Password!");
                txtBoxUser.Clear();
                txtBoxPass.Clear();
                txtBoxUser.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
