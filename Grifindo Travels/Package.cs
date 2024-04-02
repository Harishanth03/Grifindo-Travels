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
    public partial class Package : UserControl
    {
        public Package()
        {
            InitializeComponent();
            DisplayPackage();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-2LLKKSH\SQLEXPRESS;Initial Catalog=Grifindo_Travels;Integrated Security=True");
        private void DisplayPackage()
        {
            con.Open();
            string query = "SELECT * FROM package";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PackageGrideView.DataSource = ds.Tables[0];
            con.Close();
        }
        private void clear()
        {
            packageidtextbox.Text = "";
            packagetype.Text = "";
            maximumkmtextbox.Text = "";
            extrakmcosttextboiox.Text = "";
            maximumhourtextbox.Text = "";
            extrahourtextbox.Text = "";
        }
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (packageidtextbox.Text == ""|| packagetype.Text == ""|| maximumkmtextbox.Text == "" || extrakmcosttextboiox.Text == "" || maximumhourtextbox.Text == "" || extrahourtextbox.Text == "")
            {
                MessageBox.Show( "Missing Information", "Information",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO package (Package_ID ,Type ,Maximum_KM,Maximum_Hour,ExtraKM_cost,ExtraHour_Cost) VALUES(@packageID,@type,@maximumKm,@maximumhour,@extrakmcost,@extrahourhost)", con);
                    cmd.Parameters.AddWithValue("@packageID", packageidtextbox.Text);
                    cmd.Parameters.AddWithValue("@type", packagetype.Text);
                    cmd.Parameters.AddWithValue("@maximumKm", maximumkmtextbox.Text);
                    cmd.Parameters.AddWithValue("@maximumhour", maximumhourtextbox.Text);
                    cmd.Parameters.AddWithValue("@extrakmcost", extrakmcosttextboiox.Text);
                    cmd.Parameters.AddWithValue("@extrahourhost", extrahourtextbox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Package Details Added successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    DisplayPackage();
                    clear();
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (packageidtextbox.Text == "" || packagetype.Text == "" || maximumkmtextbox.Text == "" || extrakmcosttextboiox.Text == "" || maximumhourtextbox.Text == "" || extrahourtextbox.Text == "")
            {
                MessageBox.Show("Missing Information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE package SET Type = @type,Maximum_KM = @maximumKm , Maximum_Hour = @maximumhour ,ExtraKM_cost = @extrakmcost ,ExtraHour_Cost = @extrahourhost WHERE Package_ID = @packageID", con);
                    cmd.Parameters.AddWithValue("@packageID", packageidtextbox.Text);
                    cmd.Parameters.AddWithValue("@type", packagetype.Text);
                    cmd.Parameters.AddWithValue("@maximumKm", maximumkmtextbox.Text);
                    cmd.Parameters.AddWithValue("@maximumhour", maximumhourtextbox.Text);
                    cmd.Parameters.AddWithValue("@extrakmcost", extrakmcosttextboiox.Text);
                    cmd.Parameters.AddWithValue("@extrahourhost", extrahourtextbox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Package Details Added successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    DisplayPackage();
                    clear();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int key = 0;

        private void PackageGrideView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            key = Convert.ToInt32(PackageGrideView.SelectedRows[0].Cells[0].Value.ToString());
            packageidtextbox.Text = PackageGrideView.SelectedRows[0].Cells[0].Value.ToString();
            packagetype.Text = PackageGrideView.SelectedRows[0].Cells[1].Value.ToString();
            maximumkmtextbox.Text = PackageGrideView.SelectedRows[0].Cells[2].Value.ToString();
            maximumhourtextbox.Text = PackageGrideView.SelectedRows[0].Cells[3].Value.ToString();
            extrakmcosttextboiox.Text = PackageGrideView.SelectedRows[0].Cells[4].Value.ToString();
            extrahourtextbox.Text = PackageGrideView.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Information", "information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE from package  WHERE Package_ID = @PID", con);
                    cmd.Parameters.AddWithValue("@PID", int.Parse(packageidtextbox.Text));
                    cmd.Parameters.AddWithValue("@SD", packagetype.Text);
                    cmd.Parameters.AddWithValue("@PT", maximumkmtextbox.Text);
                    cmd.Parameters.AddWithValue("@CID", maximumhourtextbox.Text);
                    cmd.Parameters.AddWithValue("@STT", extrakmcosttextboiox.Text);
                    cmd.Parameters.AddWithValue("@PRID", extrahourtextbox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Package Details Deleted successfully", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    DisplayPackage();
                    clear();
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
            }
        }
    }
}
