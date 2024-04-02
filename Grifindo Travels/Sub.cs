using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data;


namespace Grifindo_Travels
{
    class Sub
    {
        static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Harishanth\OneDrive\Documents\Grifindo.mdf;Integrated Security=True;Connect Timeout=30");

        public static string execute(string sql)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                int n = cmd.ExecuteNonQuery();
                return n.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                con.Close();
            }
        }
        public static DataTable GetData(string sql)
        {
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            return dt;
        }
        #region Password Generate
        public static string Password_generate(int len)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            for (int i = 1; i <= len; i++)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        #endregion

    }
}
