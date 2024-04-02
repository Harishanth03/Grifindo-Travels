using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Grifindo_Travels
{
    public partial class Sales : UserControl
    {
        public Sales()
        {
            InitializeComponent();
            
        }
        private void DisplaySales()
        {

           
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Sales_Load(object sender, EventArgs e)
        {

        }

        private void calculationButton_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox2.Text == "" || StartTimePicker.Text == "" || guna2DateTimePicker1.Text == "" )
            {
                MessageBox.Show("information is missing!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string type_of_sales = guna2ComboBox2.Text;
                string startDate = StartTimePicker.Value.ToShortDateString();
                string endDate = guna2DateTimePicker1.Value.ToShortDateString();
                RentCalculationTextBox.Text = Calculation.Display_Total_Sales(type_of_sales, startDate, endDate).ToString();
                Calculation obj = new Calculation();
                SqlConnection con = new SqlConnection(obj.getConnection());


                con.Open();
                string query = "SELECT * FROM Sales where  Sales_Type = '" + type_of_sales + "' and Date BETWEEN '" + startDate + "' and '" + endDate + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                bunifuDataGridView1.DataSource = ds.Tables[0];
                con.Close();
            }
            
        }
    }
}
