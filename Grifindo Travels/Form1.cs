using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Grifindo_Travels
{
    public partial class Form1 : Form
    {
        public static string Username = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if(UsernameTextBox.Text == "admin" && PasswordTextBox.Text == "1234")
            {
                dash_board1 dash = new dash_board1();
                dash.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please enter the correct user name or password");
            }
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            label2.Visible = false;
            label1.Visible = false;
        }
    }
}
