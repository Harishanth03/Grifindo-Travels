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
    public partial class dash_board1 : Form
    {
        public dash_board1()
        {
            InitializeComponent();
        }
        public void add_panel(Control c)
        {
            c.Dock = DockStyle.Fill;
            Main_Panel.Controls.Clear();
            Main_Panel.Controls.Add(c);
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            Vehicle vehicle = new Vehicle();
            add_panel(vehicle);
        }

        private void dash_board1_Load(object sender, EventArgs e)
        {
            Vehicle vehicle = new Vehicle();
            add_panel(vehicle);
        }

        private void RentButton_Click(object sender, EventArgs e)
        {
            Rent rent = new Rent();
            add_panel(rent);
        }

        private void PackageButton_Click(object sender, EventArgs e)
        {
            Package PACK = new Package();
            add_panel(PACK);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Day_tour_Hire Day = new Day_tour_Hire();
            add_panel(Day);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            LongTour longtour = new LongTour();
            add_panel(longtour);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            add_panel(sales);
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            VehicleType vehi = new VehicleType();
            add_panel(vehi);
        }
    }
}
