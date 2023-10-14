using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Security.Cryptography;

namespace BT2._6
{
    internal class Database
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=QLSV;Integrated Security=True";
        private SqlConnection connection;
        private string sql;
        private DataTable dt;
        private SqlCommand cmd;
        public Database()
        {
            try
            {
                connection = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public DataTable SelectData()
        {
            try
            {
                connection.Open();
                sql = "SELECT * FROM tblSinhVien";
                cmd = new SqlCommand(sql, connection);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
        public void InsertData(Dictionary<string, string> a)
        {
            connection.Open();
            sql = "SELECT * FROM tblSinhVien WHERE MSSV = @MSSV";
            cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@MSSV", a.First().Value);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count == 0) {
                sql = "INSERT INTO tblSinhVien (MSSV, Ho, Ten, GioiTinh, NgaySinh, Khoa, Lop, SDT) VALUES " +
                    "(@MSSV, @Ho, @Ten, @GioiTinh, @NgaySinh, @Khoa, @Lop, @SDT)";
            } else
            {
                sql = "UPDATE tblSinhVien SET ";
                foreach (var i in a)
                {
                    if (i.Value == "") continue;
                    sql = string.Concat(sql, i.Key + " = @" + i.Key + ",");
                }
                sql = sql.Remove(sql.Length - 1);
                sql = string.Concat(sql, " WHERE MSSV = " + a.First().Value);
            }
            cmd = new SqlCommand(sql, connection);
            foreach (var i in a)
            {
                cmd.Parameters.AddWithValue("@" + i.Key, i.Value);
            }
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteData(string a1)
        {
            connection.Open();
            sql = "SELECT * FROM tblSinhVien WHERE MSSV = @MSSV";
            cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@MSSV", a1);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count == 0) throw new Exception();
            sql = "DELETE FROM tblSinhVien WHERE MSSV = @MSSV";
            cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@MSSV", a1);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }   
}
