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
    public partial class LongTour : UserControl
    {
        public LongTour()
        {
            InitializeComponent();
        }
        //
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-2LLKKSH\SQLEXPRESS;Initial Catalog=Grifindo_Travels;Integrated Security=True");

        private void calculationButton_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox2.Text == "" || guna2ComboBox1.Text == "" || StartTimePicker.Text == "" || guna2DateTimePicker1.Text == "" || StartKmTextBox.Text == "" || EndKmTextBox.Text == "")
            {
                MessageBox.Show("Missing information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string Vehicle_No = guna2ComboBox2.Text;
                string Package_Type = guna2ComboBox1.Text;
                DateTime start_Date = StartTimePicker.Value;
                DateTime End_Date = guna2DateTimePicker1.Value;
                int start_Km = int.Parse(StartKmTextBox.Text);
                int End_Km = int.Parse(EndKmTextBox.Text);

                int Base_Hire_Charge = 0;
                int overnight_stay_charge = 0;
                int extra_km_charge = 0;

                Calculation.LongTour_Hire_calculation(Vehicle_No, Package_Type, start_Date, End_Date, start_Km, End_Km, ref Base_Hire_Charge, ref overnight_stay_charge, ref extra_km_charge);

                int total_hire_value = Base_Hire_Charge + overnight_stay_charge + extra_km_charge;
                BaseHireChargeTextBox.Text = Base_Hire_Charge.ToString();
                WaitingChargeTextBox.Text = overnight_stay_charge.ToString();
                ExtraKMChargeTextBox.Text = extra_km_charge.ToString();
                TotalChargeTextBox.Text = total_hire_value.ToString();
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (TotalChargeTextBox.Text == "")
            {
                MessageBox.Show("Missing information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string Sales_Type = "Long Tour";
                int Amount = int.Parse((TotalChargeTextBox.Text).ToString());
                string date = DateTime.Now.ToShortDateString();

                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Sales (Sales_Type,Amount,Date) VALUES (@Sales_Type,@amount,@Date)", con);
                cmd.Parameters.AddWithValue("@Sales_Type", Sales_Type);
                cmd.Parameters.AddWithValue("@amount", Amount);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert Successfully", "Insert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            
        }

    }
}
