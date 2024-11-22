using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class QuanLyLop : Form
    {
        SqlConnection cn;
        SqlDataAdapter da;
        SqlCommand cmd;
        String ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\hieu\\khanht\\QuanLySinhVien\\QuanLySinhVien\\App_Data\\QuanLySinhVien.mdf;Integrated Security=True";
        public QuanLyLop()
        {
            InitializeComponent();
            LoadLopLenLuoi();
        }

        public void LoadLopLenLuoi()
        {
            Connect();
            cmd = new SqlCommand("sp_Select", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@tenbang", SqlDbType.NVarChar).Value = "LOP";
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            Disconnect();
        }

        public void Connect()
        {
            try
            {
                cn = new SqlConnection(ConnectionString);
                cn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Disconnect()
        {
            cn.Close();
        }

            String malop,tenlop;
        int siso;

        private void button_Xoa_Click(object sender, EventArgs e)
        {
            malop = textBox_maLop.Text;

            Connect();

            cmd = new SqlCommand("sp_DeleteLop", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@malop", SqlDbType.NChar).Value = malop;
            cmd.ExecuteNonQuery();

            Disconnect();
            LoadLopLenLuoi();
        }

        private void button_Sua_Click(object sender, EventArgs e)
        {
            malop = textBox_maLop.Text;
            tenlop = textBox_tenLop.Text;
            siso = int.Parse(textBox_siSo.Text);

            Connect();

            cmd = new SqlCommand("sp_UpdateLop", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@malop", SqlDbType.NChar).Value = malop;
            cmd.Parameters.Add("@tenlop", SqlDbType.NVarChar).Value = tenlop;
            cmd.Parameters.Add("@siso", SqlDbType.Int).Value = siso;
            cmd.ExecuteNonQuery();

            Disconnect();
            LoadLopLenLuoi();
        }

        private void button_Them_Click(object sender, EventArgs e)
        {
            malop = textBox_maLop.Text;
            tenlop = textBox_tenLop.Text;
            siso = int.Parse(textBox_siSo.Text);

            Connect();

            cmd = new SqlCommand("sp_InsertLop", cn);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.Add("@malop", SqlDbType.NChar).Value = malop;
            cmd.Parameters.Add("@tenlop", SqlDbType.NVarChar).Value = tenlop;
            cmd.Parameters.Add("@siso", SqlDbType.Int).Value = siso;
            cmd.ExecuteNonQuery();

            Disconnect();
            LoadLopLenLuoi();
        }
    }
}
