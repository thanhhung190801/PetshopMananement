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
    public partial class Products : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\PetShopOb1.mdf;Integrated Security=True;Connect Timeout=30");

        public Products()
        {
            InitializeComponent();
        }
        private void displayProducts()
        {

            string query = "select * from ProductTbl ";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductDRW.DataSource = ds.Tables[0];

        }
        private void btnreset_Click(object sender, EventArgs e)
        {
            txtname1.Text = "";
            txtquan.Text = "";
            txtprice.Text = "";
            ComCato.SelectedIndex = 0;
            
        }
        int key = 1;

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (txtname1.Text == "" || txtprice.Text == "" || txtquan.Text == "" || ComCato.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa Nhập dữ Liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    SqlCommand saveEmpcommand = conn.CreateCommand();
                    saveEmpcommand.CommandType = CommandType.Text;
                    saveEmpcommand.CommandText = "insert into ProductTbl (PrName,PrCato,Prquan,PrPrice) " +
                        "values(N'" + txtname1.Text + "',N'" + ComCato.SelectedItem.ToString() + "'," + int.Parse(txtquan.Text) + "," + int.Parse(txtprice.Text) + ") ";
                    saveEmpcommand.ExecuteNonQuery();
                    MessageBox.Show("Bạn lưu dữ liệu Thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    displayProducts();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (txtname1.Text == "" || txtprice.Text == "" || txtquan.Text == "" || ComCato.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa Nhập dữ Liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {

                    SqlCommand saveEmpcommand = conn.CreateCommand();
                    saveEmpcommand.CommandType = CommandType.Text;
                    saveEmpcommand.CommandText = "update ProductTbl " +
                        "set PrName=N'" + txtname1.Text + "',Prquan=" + int.Parse(txtquan.Text) + ",PrCato=N'" + ComCato.SelectedItem.ToString() + "',PrPrice=" + int.Parse(txtprice.Text) + " where PrId='" + key + "' ";
                    saveEmpcommand.ExecuteNonQuery();
                    MessageBox.Show("Bạn update dữ liệu Thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    displayProducts();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("vui long Nhap dong du lieu can xoa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {

                    SqlCommand saveEmpcommand = conn.CreateCommand();
                    saveEmpcommand.CommandType = CommandType.Text;
                    saveEmpcommand.CommandText = "delete from ProductTbl where PrId='" + key + "' ";
                    saveEmpcommand.ExecuteNonQuery();
                    MessageBox.Show("Bạn delete dữ liệu Thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    displayProducts();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Products_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }

        private void ProductDRW_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string gender = "";

            txtname1.Text = ProductDRW.SelectedRows[0].Cells[1].Value.ToString();
            ComCato.Text = ProductDRW.SelectedRows[0].Cells[2].Value.ToString();
            txtquan.Text = ProductDRW.SelectedRows[0].Cells[3].Value.ToString();
            txtprice.Text = ProductDRW.SelectedRows[0].Cells[4].Value.ToString();
            
            if (txtname1.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ProductDRW.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
