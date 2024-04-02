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
    public partial class Vehicle : UserControl
    {
        public Vehicle()
        {
            InitializeComponent();
            DisplayVehicle();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-2LLKKSH\SQLEXPRESS;Initial Catalog=Grifindo_Travels;Integrated Security=True");

        private void DisplayVehicle()
        {
            con.Open();
            string query = "SELECT * FROM Vehicle_Type";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            bunifuDataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void clear()
        {
            vehicleIDtextbox.Text = "";
            VehicleTypeBox.Text = "";
            PerDayCostTextbox.Text = "";
            WeekCostTextbox.Text = "";
            MonthCostTextbox.Text = "";
            DriverCostTextbox.Text = "";
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (vehicleIDtextbox.Text == "" || VehicleTypeBox.Text == "" ||  PerDayCostTextbox.Text == "" || WeekCostTextbox.Text == "" || MonthCostTextbox.Text == "" || DriverCostTextbox.Text == "")
            {
                MessageBox.Show("Missing Information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Vehicle_Type (Vehicle_ID, Vehicle_Type, per_Day_Cost, Per_Week_Cost, Monthly_Cost, Driver_Cost) VALUES (@VID, @VT, @PDC, @PWC, @MC, @DC)", con);

                    // Assuming vehicleIDtextbox.Text contains an integer value
                    cmd.Parameters.AddWithValue("@VID", int.Parse(vehicleIDtextbox.Text));

                    // Assuming VehicleTypeBox.Text contains a string value for vehicle type
                    cmd.Parameters.AddWithValue("@VT", VehicleTypeBox.Text);

                    // Assuming PerDayCostTextbox.Text, WeekCostTextbox.Text, MonthCostTextbox.Text, and DriverCostTextbox.Text
                    // contain decimal values for cost
                    cmd.Parameters.AddWithValue("@PDC", Convert.ToDecimal(PerDayCostTextbox.Text));
                    cmd.Parameters.AddWithValue("@PWC", Convert.ToDecimal(WeekCostTextbox.Text));
                    cmd.Parameters.AddWithValue("@MC", Convert.ToDecimal(MonthCostTextbox.Text));
                    cmd.Parameters.AddWithValue("@DC", Convert.ToDecimal(DriverCostTextbox.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Details Added successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    DisplayVehicle();
                    clear();

                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
            }
        }

        private void WeekCostTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Vehicle_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (vehicleIDtextbox.Text == "" || VehicleTypeBox.Text == "" || PerDayCostTextbox.Text == "" || WeekCostTextbox.Text == "" || MonthCostTextbox.Text == "" || DriverCostTextbox.Text == "")
            {
                MessageBox.Show("Missing Information!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            { 
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Vehicle_Type SET Vehicle_Type = @VT, per_Day_Cost = @PDC , Per_Week_Cost = @PWC, Monthly_Cost = @mo , Driver_Cost = @dr WHERE Vehicle_ID = @vid", con);
                    cmd.Parameters.AddWithValue("@vid", vehicleIDtextbox.Text);
                    cmd.Parameters.AddWithValue("@VT", VehicleTypeBox.Text);
                    cmd.Parameters.AddWithValue("@PDC", PerDayCostTextbox.Text);
                    cmd.Parameters.AddWithValue("PWC", WeekCostTextbox.Text);
                    cmd.Parameters.AddWithValue("@mo", MonthCostTextbox.Text);
                    cmd.Parameters.AddWithValue("@dr", DriverCostTextbox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Successfull", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    DisplayVehicle();
                    clear();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }
        int key = 0;

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            key = Convert.ToInt32(bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            vehicleIDtextbox.Text = bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            VehicleTypeBox.Text = bunifuDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            PerDayCostTextbox.Text = bunifuDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            WeekCostTextbox.Text = bunifuDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            MonthCostTextbox.Text = bunifuDataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            DriverCostTextbox.Text = bunifuDataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Information!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
           else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Vehicle_Type  WHERE Vehicle_ID = @vid", con);
                    cmd.Parameters.AddWithValue("@vid", vehicleIDtextbox.Text);
                    cmd.Parameters.AddWithValue("@VT", VehicleTypeBox.Text);
                    cmd.Parameters.AddWithValue("@PDC", PerDayCostTextbox.Text);
                    cmd.Parameters.AddWithValue("PWC", WeekCostTextbox.Text);
                    cmd.Parameters.AddWithValue("@mo", MonthCostTextbox.Text);
                    cmd.Parameters.AddWithValue("@dr", DriverCostTextbox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Successfull", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    DisplayVehicle();
                    clear();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
