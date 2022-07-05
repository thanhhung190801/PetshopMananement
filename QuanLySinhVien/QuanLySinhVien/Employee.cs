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
    public partial class Employee : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\PetShopOb1.mdf;Integrated Security=True;Connect Timeout=30");
        public Employee()
        {
            InitializeComponent();


            displayEmp();
        }


        private void Employee_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }
        int key = 1;
        private void btnsave_Click(object sender, EventArgs e)
        {
            if (txtname.Text == "" || txtphone.Text == "" || txtadd.Text == "")
            {
                MessageBox.Show("Bạn chưa Nhập dữ Liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {

                    string gender = "";
                    if (rdbfemale.Checked == true) gender = "NU";
                    else if (rdmale.Checked == true) gender = "NAM";
                    SqlCommand saveEmpcommand = conn.CreateCommand();
                    saveEmpcommand.CommandType = CommandType.Text;
                    saveEmpcommand.CommandText = "insert into EmployeeTbl(EmpName,EmpAdd,EmpDOB,EmpGender,Empphone) " +
                        "values(N'" + txtname1.Text + "',N'" + txtadd.Text + "',N'" + txtdate.Value.Date + "',N'" + gender + "',N'" + txtphone.Text + "') ";
                    saveEmpcommand.ExecuteNonQuery();
                    MessageBox.Show("Bạn lưu dữ liệu Thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    displayEmp();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void displayEmp()
        {

            string query = "select * from EmployeeTbl ";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDRW.DataSource = ds.Tables[0];

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {

            if (txtname.Text == "" || txtphone.Text == "" || txtadd.Text == "")
            {
                MessageBox.Show("Bạn chưa Nhập dữ Liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {

                    string gender = "";
                    if (rdbfemale.Checked == true) gender = "NU";
                    else if (rdmale.Checked == true) gender = "NAM";
                    SqlCommand saveEmpcommand = conn.CreateCommand();
                    saveEmpcommand.CommandType = CommandType.Text;
                    saveEmpcommand.CommandText = "update EmployeeTbl " +
                        "set EmpName=N'" + txtname1.Text + "',EmpAdd=N'" + txtadd.Text + "',EmpDOB=N'" + txtdate.Value.Date + "',EmpGender=N'" + gender + "',Empphone=N'" + txtphone.Text + "'where EmpId='" + key + "' ";
                    saveEmpcommand.ExecuteNonQuery();
                    MessageBox.Show("Bạn update dữ liệu Thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    displayEmp();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EmployeeDRW_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string gender = "";

            txtname1.Text = EmployeeDRW.SelectedRows[0].Cells[1].Value.ToString();
            txtadd.Text = EmployeeDRW.SelectedRows[0].Cells[2].Value.ToString();
            txtdate.Text = EmployeeDRW.SelectedRows[0].Cells[3].Value.ToString();
            gender = EmployeeDRW.SelectedRows[0].Cells[4].Value.ToString();
            if (gender == "NU") rdbfemale.Checked = true;
            else
                rdmale.Checked = true;
            txtphone.Text = EmployeeDRW.SelectedRows[0].Cells[5].Value.ToString();
            if (txtname1.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(EmployeeDRW.SelectedRows[0].Cells[0].Value.ToString());
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

                    string gender = "";
                    if (rdbfemale.Checked == true) gender = "NU";
                    else if (rdmale.Checked == true) gender = "NAM";
                    SqlCommand saveEmpcommand = conn.CreateCommand();
                    saveEmpcommand.CommandType = CommandType.Text;
                    saveEmpcommand.CommandText = "delete from EmployeeTbl where EmpId='" + key + "' ";
                    saveEmpcommand.ExecuteNonQuery();
                    MessageBox.Show("Bạn delete dữ liệu Thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    displayEmp();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtname1.Text = "";
            txtphone.Text = "";
            txtadd.Text = "";
            rdbfemale.Checked = false;
            rdmale.Checked = false;
            txtdate.Value = DateTime.Now;
        }
    }
}
