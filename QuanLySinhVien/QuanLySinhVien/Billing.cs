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
    public partial class Billing : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\PetShopOb1.mdf;Integrated Security=True;Connect Timeout=30");

        public Billing()
        {
            InitializeComponent();
            lblEmploy.Text = "";
            GetCustomers();

        }
        private void GetCustomers()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Id from CustomersTbl", conn);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable(); 
            dt.Columns.Add("Id", typeof(int));
            dt.Load(Rdr);
            CusIdCombo.ValueMember = "Id";
            CusIdCombo.DataSource = dt;
            conn.Close();

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
        private void getCusName()
        {
            conn.Open();
            string query = "select * from CustomersTbl where Id =" + CusIdCombo.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                cusname.Text = dr["CusName"].ToString();
            }
            conn.Close();
        }
        int stock = 10;
        int key = 1;
        private void UpdateStock()
        {
            try
            {
                int newQty = stock -Convert.ToInt32(quantity.Text);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Update ProductTbl set Prquan=" + newQty + " where PrId=" + key + "",conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                displayProducts();


                    
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }
        private void txtname_Click(object sender, EventArgs e)
        {

        }
    }
}
