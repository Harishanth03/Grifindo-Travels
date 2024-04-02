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
    public partial class VehicleType : UserControl
    {
        public VehicleType()
        {
            InitializeComponent();
            Displayvehicle();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-2LLKKSH\SQLEXPRESS;Initial Catalog=Grifindo_Travels;Integrated Security=True");

        private void Displayvehicle()
        {
            con.Open();
            string query = "SELECT * FROM Vehicle";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            VehicleGridView.DataSource = ds.Tables[0];
            con.Close();
        }
        private void clear()
        {
            vehicleREGNOtextbox.Text = "";
            VehicleIDtextbox.Text = "";
            VehicleModelTextBox.Text = "";
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (vehicleREGNOtextbox.Text == "" || VehicleIDtextbox.Text == "" || VehicleModelTextBox.Text == "")
            {
                MessageBox.Show("Missing Information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Vehicle (Vehicle_Number,Vehicle_ID,Vehicle_Model) VALUES (@vehiclenumber,@VehicleID,@Vehiclemodel)", con);
                    cmd.Parameters.AddWithValue("@vehiclenumber", vehicleREGNOtextbox.Text);
                    cmd.Parameters.AddWithValue("@VehicleID", VehicleIDtextbox.Text);
                    cmd.Parameters.AddWithValue("@Vehiclemodel", VehicleModelTextBox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Details added Successfully","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    con.Close();
                    Displayvehicle();
                    clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (vehicleREGNOtextbox.Text == "" || VehicleIDtextbox.Text == "" || VehicleModelTextBox.Text == "")
            {
                MessageBox.Show("Missing Information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Vehicle SET Vehicle_ID = @VehicleID , Vehicle_Model = @Vehiclemodel WHERE Vehicle_Number = @vehiclenumber ", con);
                    cmd.Parameters.AddWithValue("@vehiclenumber", vehicleREGNOtextbox.Text);
                    cmd.Parameters.AddWithValue("@VehicleID", VehicleIDtextbox.Text);
                    cmd.Parameters.AddWithValue("@Vehiclemodel", VehicleModelTextBox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Details added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    Displayvehicle();
                    clear();
                }
                catch(Exception  ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void VehicleGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            vehicleREGNOtextbox.Text = VehicleGridView.SelectedRows[0].Cells[0].Value.ToString();
            VehicleIDtextbox.Text = VehicleGridView.SelectedRows[0].Cells[1].Value.ToString();
            VehicleModelTextBox.Text = VehicleGridView.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (vehicleREGNOtextbox.Text == "" || VehicleIDtextbox.Text == "" || VehicleModelTextBox.Text == "")
            {
                MessageBox.Show("Missing Information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE from Vehicle  WHERE Vehicle_Number = @PID", con);
                    cmd.Parameters.AddWithValue("@PID",vehicleREGNOtextbox.Text);
                    cmd.Parameters.AddWithValue("@SD", VehicleIDtextbox.Text);
                    cmd.Parameters.AddWithValue("@PT", VehicleModelTextBox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Staff Details Deleted successfully", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    Displayvehicle();
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
