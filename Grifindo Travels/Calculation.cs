using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Grifindo_Travels
{
    public class Calculation
    {
        static SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-2LLKKSH\SQLEXPRESS;Initial Catalog=Grifindo_Travels;Integrated Security=True");
        //public int Rent_calculation(string vehicleNo, DateTime rentDate, DateTime returnDate, bool withDriver)
        //{
        //    TimeSpan ts = rentDate - returnDate;
        //    int totalDays = ts.Days;
        //    int totalMomth = totalDays / 30;
        //    int totalWeek = (totalDays - (totalMomth * 30)) / 7;
        //    int remaindays = totalDays - (totalMomth * 30) - (totalWeek * 7);
        //    con.Open();
        //    SqlCommand com = new SqlCommand("SELECT Vehicle_ID FROM Vehicle WHERE Vehicle_Number = '" + vehicleNo + "'", con);
        //    SqlDataReader Reader = com.ExecuteReader();
        //    string VehicleType = "";
        //    while (Reader.Read())
        //    {
        //        VehicleType = Reader[0].ToString();
        //    }
        //    //return vehicleType;
        //    con.Close();

        //    con.Open();
        //    SqlCommand com1 = new SqlCommand("SELECT Monthly_Cost ,Per_Week_Cost,per_Day_Cost,Driver_Cost FROM Vehicle_Type WHERE Vehicle_ID = '" + VehicleType + "'", con);
        //    SqlDataReader Reader1 = com.ExecuteReader();
        //    int Month_Cost = 0;
        //    int Week_Cost = 0;
        //    int Perday_Cost = 0;
        //    int Driver_Cost = 0;
        //    while (Reader1.Read())
        //    {
        //        Month_Cost = int.Parse(Reader1[0].ToString());
        //        Week_Cost = int.Parse(Reader1[0].ToString());
        //        Perday_Cost = int.Parse(Reader1[0].ToString());
        //        Driver_Cost = int.Parse(Reader1[0].ToString());
        //    }
        //    int TotalRent = 0;

        //    if (withDriver == true)
        //    {
        //         TotalRent = totalMomth * Month_Cost + totalWeek * Week_Cost + remaindays * Perday_Cost + totalDays * Driver_Cost;
        //    }
        //    else
        //    {
        //         TotalRent = totalMomth * Month_Cost + totalWeek * Week_Cost + remaindays * Perday_Cost;
        //    }

        //    return TotalRent;

        //}

        public string getConnection()
        {
            return @"Data Source=DESKTOP-2LLKKSH\SQLEXPRESS;Initial Catalog=Grifindo_Travels;Integrated Security=True";
        }

        public int Rent_calculation(string vehicleNo, DateTime rentDate, DateTime returnDate, bool withDriver)
        {
            // convert date time format to time span 
            TimeSpan ts = returnDate - rentDate;

            int totalDays = ts.Days + 1;
            int totalMomth = totalDays / 30;
            int totalWeek = (totalDays - (totalMomth * 30)) / 7;
            int remaindays = totalDays - (totalMomth * 30) - (totalWeek * 7);

            int Month_Cost = 0;
            int Week_Cost = 0;
            int Perday_Cost = 0;
            int Driver_Cost = 0;


            con.Open();
            string sql = "SELECT Monthly_Cost ,Per_Week_Cost,per_Day_Cost,Driver_Cost FROM Vehicle_Type WHERE Vehicle_ID = (SELECT Vehicle_ID FROM Vehicle WHERE Vehicle_Number = '" + vehicleNo + "');";
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            adapter2.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                // convrt string to INT and Splite last digit and get 1st digit
                Month_Cost = int.Parse(dt.Rows[0][0].ToString().Split('.')[0]);
                Week_Cost = int.Parse(dt.Rows[0][1].ToString().Split('.')[0]);
                Perday_Cost = int.Parse(dt.Rows[0][2].ToString().Split('.')[0]);
                Driver_Cost = int.Parse(dt.Rows[0][3].ToString().Split('.')[0]);
            }

            int TotalRent = 0;

            if (withDriver == true)
            {
                TotalRent = totalMomth * Month_Cost + totalWeek * Week_Cost + remaindays * Perday_Cost + totalDays * Driver_Cost;
            }
            else
            {
                TotalRent = totalMomth * Month_Cost + totalWeek * Week_Cost + remaindays * Perday_Cost;
            }

            return TotalRent;

        }



        public static void Day_Tour_calculation(string vehicleNo, string Pachage_Type, DateTime Start_Time, DateTime End_Time, int start_KM, int End_KM,
            ref int waiting_charge, ref int base_hire_charge, ref int ExtraKM_Charge)
        {
            int totalKM = End_KM - start_KM;

            
            TimeSpan THours = End_Time - Start_Time;
            int total_hours = THours.Hours;
            int extraKM = End_KM - start_KM;

            int Standard_rate = 0;

            con.Open();
            string SQL = "select Standard_rate from Hire_Tariff where Vehicle_Type = '"+ vehicleNo + "'  and Package_Type = '"+ Pachage_Type + "'";
            SqlDataAdapter adapter2 = new SqlDataAdapter(SQL, con);
            DataTable dt = new DataTable();
            adapter2.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                Standard_rate = int.Parse(dt.Rows[0][0].ToString().Split('.')[0]);
            }
            base_hire_charge = Standard_rate;

            int Maximum_KM = 0;
            int Maximum_Hour = 0;
            int ExtraHour_Cost = 0;
            int ExtraKM_cost = 0;

            con.Open();
            string sql2 = "select Maximum_KM, ExtraKM_cost,Maximum_Hour,ExtraHour_Cost from package where Type = '"+ Pachage_Type + "'";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql2, con);
            DataTable dt2 = new DataTable();
            adapter1.Fill(dt2);
            con.Close();

            if (dt2.Rows.Count > 0)
            {
                Maximum_KM = int.Parse(dt2.Rows[0]["Maximum_KM"].ToString());
                ExtraKM_cost = int.Parse(dt2.Rows[0]["ExtraKM_cost"].ToString());
                Maximum_Hour = int.Parse(dt2.Rows[0]["Maximum_Hour"].ToString());
                ExtraHour_Cost = int.Parse(dt2.Rows[0]["ExtraHour_Cost"].ToString());
            }
            if (extraKM < 0)
            {
                ExtraKM_Charge = 0;
            }
            else
            {
                ExtraKM_Charge = totalKM * ExtraKM_cost;
            }
            if (total_hours < 0 )
            {
                waiting_charge = 0;
            }
            else
            {
                waiting_charge = total_hours * ExtraHour_Cost;
            }

        }

        public static void LongTour_Hire_calculation (string Vehicle_No, String Package_Type, DateTime start_Date, DateTime End_Date, int start_Km,
            int End_Km, ref int Base_Hire_Charge, ref int overnight_stay_charge, ref int extra_km_charge)
        {
            TimeSpan TotalDays = End_Date - start_Date;
            int Total_Days = TotalDays.Days + 1;
            int Total_Km = End_Km - start_Km;
            

            int Maximum_KM = 0;
            int ExtraKM_cost = 0;
            int Standard_rate = 0;
            int Driver_Night_Charge = 0;
            int vehicle_Night_park = 0;

            con.Open();
            String SQl = "SELECT package.Maximum_KM,package.ExtraKM_cost,Hire_Tariff.Standard_rate,Hire_Tariff.Driver_Night_Charge,Hire_Tariff.vehicle_Night_park  FROM Hire_Tariff FULL JOIN package ON Hire_Tariff.Package_Type = package.Type WHERE Hire_Tariff.Vehicle_Type = '"+ Vehicle_No + "' AND package.Type = '"+ Package_Type + "' ";
            SqlDataAdapter adepter = new SqlDataAdapter(SQl, con);
            DataTable table1 = new DataTable();
            adepter.Fill(table1);
            con.Close();

            if (table1.Rows.Count > 0)
            {
                Maximum_KM = int.Parse(table1.Rows[0]["Maximum_KM"].ToString());
                ExtraKM_cost = int.Parse(table1.Rows[0]["ExtraKM_cost"].ToString());
                Standard_rate = int.Parse(table1.Rows[0]["Standard_rate"].ToString());
                Driver_Night_Charge = int.Parse(table1.Rows[0]["Driver_Night_Charge"].ToString());
                vehicle_Night_park = int.Parse(table1.Rows[0]["vehicle_Night_park"].ToString());
            }
            Base_Hire_Charge = Standard_rate * Total_Days;
            overnight_stay_charge = (Driver_Night_Charge + vehicle_Night_park) * (Total_Days + 1);
            int extra_km = Total_Km - Maximum_KM;
            int maximumKm = Maximum_KM * Total_Days;
            if (extra_km < 0)
            {
                extra_km_charge = Total_Km * ExtraKM_cost;
            }
        }
        public static int Display_Total_Sales(String type_of_sales, string Start_date, string End_date)
        {
            con.Open();
            string SQL = "SELECT SUM(Amount) As Tot FROM Sales where  Sales_Type = '" + type_of_sales + "' and Date BETWEEN '" + Start_date + "' and '" + End_date + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(SQL, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            int Total_Sales_Value = 0;
            if (dt.Rows.Count > 0)
            {
                string totalSalesString = dt.Rows[0]["Tot"].ToString();
                if (int.TryParse(totalSalesString, out Total_Sales_Value))
                {
                    // Parsing successful
                }
                else
                {
                    // Parsing failed, handle the error
                    // You may want to log this or display an error message
                    Console.WriteLine("Failed to parse Total Sales Value");
                }
            }
            return Total_Sales_Value;


        }

    }
}
