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
    public partial class Day_tour_Hire : UserControl
    {
        public Day_tour_Hire()
        {
            InitializeComponent();
        }
        
        

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void StartKmTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (!char.IsDigit(c) && c != 8)
            {
                e.Handled = true;
            }
        }

        private void calculationButton_Click(object sender, EventArgs e)
        {
            if (VehicleTypeBox.Text == "" || pacjageTypetextbox.Text == "" || dateTimePicker1.Text == "" || dateTimePicker2.Text == "" || bunifuTextBox1.Text == "" || EndKmTextBox.Text == "")
            {
                MessageBox.Show("Missing information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string VehicleNo = VehicleTypeBox.Text;
                string packageType = pacjageTypetextbox.Text;
                DateTime starttime = dateTimePicker1.Value;
                DateTime endtime = dateTimePicker2.Value;
                int startKm = int.Parse(bunifuTextBox1.Text);
                int endkm = int.Parse(EndKmTextBox.Text);

                int waiting_charge = 0;
                int base_hire_charge = 0;
                int ExtraKM_Charge = 0;

                Calculation.Day_Tour_calculation(VehicleNo, packageType, starttime, endtime, startKm, endkm,
                    ref waiting_charge, ref base_hire_charge, ref ExtraKM_Charge);

                int total_Charge = waiting_charge + base_hire_charge + ExtraKM_Charge;
                TotalChargeTextBox.Text = total_Charge.ToString();
                WaitingChargeTextBox.Text = waiting_charge.ToString();
                BaseHireChargeTextBox.Text = base_hire_charge.ToString();
                ExtraKMChargeTextBox.Text = ExtraKM_Charge.ToString();
            }
            
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (TotalChargeTextBox.Text == "")
            {
                MessageBox.Show("Missing information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string salesType = "Day Tour";
                int amount = int.Parse((TotalChargeTextBox.Text).ToString());
                string Date = DateTime.Now.ToShortDateString();

                Calculation obj = new Calculation();

                SqlConnection con = new SqlConnection(obj.getConnection());

                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Sales (Sales_Type,Amount,Date) VALUES (@Sales_Type,@amount,@Date)", con);
                cmd.Parameters.AddWithValue("@Sales_Type", salesType);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert Successfully", "Insert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            
        }

        private void TotalChargeTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
