using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class Intro : Form
    {
        public Intro()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        int start = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            start += 1;
            myprogress.Value = start;
            status.Text = start + "%";
            if (myprogress.Value == 100)
            {
                myprogress.Value = 0;
                LoginForm obj = new LoginForm();
                obj.Show();
                this.Hide();
                timer1.Stop();
            }
        }
    }
}
