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

namespace CleverPoppy
{
    public partial class AddNewItem : Form
    {
        public AddNewItem()
        {
            InitializeComponent();
        }
        public void DisplayData()
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\C# projects\C# project\CleverPoppy\Database\LoginDB.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "Select * from Stock";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);

            //viewing data
            dgvRefreshStock.DataSource = dtbl;
            foreach (DataGridViewBand band in dgvRefreshStock.Columns)
            {
                band.ReadOnly = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
            if(txtCode.Text=="" || txtName.Text == "" || txtPrice.Text == "" || txtQty.Text == "")
            {
                lblError.Text = "";
                lblOk.Text = "";
                lblError.Text = "Please fill the empty fields";
                txtCode.Clear();
                txtName.Clear();
                txtPrice.Clear();
                txtQty.Clear();
                txtCode.Focus();
                DisplayData();
            }
            else
            {
                try
                {
                    lblError.Text = "";
                    lblOk.Text = "";
                    SqlConnection sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\C# projects\C# project\CleverPoppy\Database\LoginDB.mdf;Integrated Security=True;Connect Timeout=30");
                    string query = "insert into Stock values('" + txtCode.Text + "','" + txtName.Text + "','" + txtQty.Text + "','" + txtPrice.Text + "')";
                    SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
                    DataTable dtbl = new DataTable();
                    sda.Fill(dtbl);
                    lblOk.Text = "Successfully Added";
                    txtCode.Clear();
                    txtName.Clear();
                    txtPrice.Clear();
                    txtQty.Clear();
                    txtCode.Focus();
                    DisplayData();

                }
                catch (SystemException ex)
                {
                    MessageBox.Show(string.Format("An error occurred: {0}", ex.Message));
                }

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCode.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            txtCode.Focus();
            lblError.Text = "";
            lblOk.Text = "";
            DisplayData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblOk.Text = "";
            SqlConnection sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\C# projects\C# project\CleverPoppy\Database\LoginDB.mdf;Integrated Security=True;Connect Timeout=30");
            string query = "Select code,name from Stock where code='"+txtCode.Text+"' and name='"+txtName.Text.Trim()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);

            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            if (dtbl.Rows.Count == 1)
            {
                try
                {
                    string q = "Delete from Stock where code='" + txtCode.Text + "' and name='" + txtName.Text.Trim() + "'";
                    SqlDataAdapter sdaA = new SqlDataAdapter(q, sqlcon);
                    DataTable dtblA = new DataTable();
                    sdaA.Fill(dtblA);
                    lblOk.Text = "Successfully deleted";
                    txtCode.Clear();
                    txtName.Clear();
                    txtCode.Focus();
                    DisplayData();
                }
                catch (SystemException ex)
                {
                    MessageBox.Show(string.Format("An error occurred: {0}", ex.Message));
                }

            }
            else
            {
                lblError.Text = "No item found";
                txtCode.Clear();
                txtName.Clear();
                txtCode.Focus();
                DisplayData();
            }

        }

        private void AddNewItem_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new Form1()).Show();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new UpdateStock()).Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
