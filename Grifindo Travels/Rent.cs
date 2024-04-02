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
    public partial class Rent : UserControl
    {
        public Rent()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-2LLKKSH\SQLEXPRESS;Initial Catalog=Grifindo_Travels;Integrated Security=True");

        private void guna2RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void calculationButton_Click(object sender, EventArgs e)
        {
            if (WithDriverRadioBTN.Checked == true)
            {
                Calculation calculation = new Calculation();
                int TotalRent = calculation.Rent_calculation(vehicleREGNOtextbox.Text, RentDateTimePicker.Value, ReturnDateTimePicker.Value, true);
                RentCalculationTextBox.Text = TotalRent.ToString();
            }
            else
            {
                Calculation calculation = new Calculation();
                int TotalRent = calculation.Rent_calculation(vehicleREGNOtextbox.Text, RentDateTimePicker.Value, ReturnDateTimePicker.Value, false);
                RentCalculationTextBox.Text = TotalRent.ToString();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string SalesType; 
             
            if (WithDriverRadioBTN.Checked == true )
            {
                SalesType = "Rent With Driver";
            }
            else
            {
                SalesType = "Rent Without Driver";
            }

            int Amount = int.Parse((RentCalculationTextBox.Text).ToString());

            String date = DateTime.Now.ToShortDateString();

            if (WithDriverRadioBTN.Checked == true)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Sales (Sales_Type,Amount,Date) VALUES (@Sales_Type,@amount,@Date)", con);
                cmd.Parameters.AddWithValue("@Sales_Type", SalesType);
                cmd.Parameters.AddWithValue("@amount", Amount);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert Successfully", "Insert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else if (WithoutDriverRadioBTN.Checked == true)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Sales (Sales_Type,Amount,Date) VALUES (@Sales_Type,@amount,@Date)", con);
                cmd.Parameters.AddWithValue("@Sales_Type", SalesType);
                cmd.Parameters.AddWithValue("@amount", Amount);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert Successfully", "Insert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }   
            

        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
    
}
