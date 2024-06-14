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
    public partial class LoadingScreen : Form
    {
        public LoadingScreen()
        {
            InitializeComponent();
        }

        // animation for loading screen bar
        private void timer1_Tick(object sender, EventArgs e)
        {
            loadingBar.Width += 3;

            if (loadingBar.Width >= 800)
            {
                timer1.Stop();
                
                new main_POS().Show();

                this.Hide();
            }
        }

       
    }
}
