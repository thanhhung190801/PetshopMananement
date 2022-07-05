using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLySinhVien
{
    public partial class LoginForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\PetShopOb1.mdf;Integrated Security=True;Connect Timeout=30");

        public LoginForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtusername.Text == "" || txtpassword.Text == "" )
            {
                MessageBox.Show("Bạn chưa Nhập dữ Liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {

                   
                    SqlCommand logincommand = conn.CreateCommand();
                    logincommand.CommandType = CommandType.Text;
                    logincommand.CommandText = "select *  from AccountTbl where Username =N'"+txtusername.Text+"' and Password =N'"+txtpassword.Text+"'";
                    var dong =logincommand.ExecuteReader();
                    if (dong.HasRows)
                    {
                   
                    MessageBox.Show("Bạn Đăng Nhập Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Home obj = new Home();
                        obj.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Bạn Đăng Nhập KHÔNG Thành Công. Vui Lòng Xem lại Password or Username", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
