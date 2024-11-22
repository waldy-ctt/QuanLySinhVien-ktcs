using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace QuanLySinhVien
{
    public partial class Form1 : Form
    {
        SqlConnection cn;
        SqlDataAdapter da;
        SqlCommand cmd;
        String ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\hieu\\khanht\\QuanLySinhVien\\QuanLySinhVien\\App_Data\\QuanLySinhVien.mdf;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
            LoadLop();
            LoadNgayThangNam();
            LoadThongTinSinhVien(comboBox_Lop.SelectedValue.ToString());
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

        private void LoadLop()
        {
            Connect();

            cmd = new SqlCommand("sp_Select", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@tenbang", SqlDbType.NVarChar).Value = "LOP";
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox_Lop.DisplayMember = "tenlop";
            comboBox_Lop.ValueMember = "malop";
            comboBox_Lop.DataSource = dt;


            Disconnect();
        }

        private void LoadNgayThangNam()
        {
            for (int i = 0; i <= 31; i++)
            {
                comboBox_Ngay.Items.Add(i.ToString());
            }

            for (int i = 0; i <= 12; i++)
            {
                comboBox_Thang.Items.Add(i.ToString());
            }

            for (int i = 1950; i <= DateTime.Now.Year; i++)
            {
                comboBox_Nam.Items.Add(i.ToString());
            }
        }

        String mssv, hoten, diachi, malop;
        int gioitinh;
        DateTime ngaysinh;
        Byte[] hinhanh;

        private void button_browse_Click(object sender, EventArgs e)
        {
            openFile.InitialDirectory = @"C:";
            openFile.FileName = "";
            openFile.Filter = "All files(*.*)|*.*";
            openFile.RestoreDirectory = true;
            openFile.ShowDialog();
            if(openFile.FileName != "")
            {
                textBox_HinhAnh.Text = openFile.SafeFileName.Substring(openFile.SafeFileName.LastIndexOf(@"\") + 1);
                pictureBox1.Image = Image.FromFile(openFile.FileName);

            }

            FileStream fsFile = new FileStream(openFile.FileName, FileMode.Open, FileAccess.Read);
            hinhanh = new byte[fsFile.Length];
            fsFile.Read(hinhanh, 0, hinhanh.Length);
            fsFile.Close();
        }

        private void button_Nhap_Click(object sender, EventArgs e)
        {
            LayDuLieuTuForm();
            LuuHinh();
            Connect();

            cmd = new SqlCommand("sp_Insert", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mssv", SqlDbType.NChar).Value = mssv;
            cmd.Parameters.Add("@hoten", SqlDbType.NVarChar).Value = hoten;
            cmd.Parameters.Add("@gioitinh", SqlDbType.Bit).Value = gioitinh;
            cmd.Parameters.Add("@ngaysinh", SqlDbType.DateTime).Value = ngaysinh;
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = diachi;
            cmd.Parameters.Add("@hinhanh", SqlDbType.Image).Value = hinhanh;
            cmd.Parameters.Add("@malop", SqlDbType.NChar).Value = malop;

            cmd.ExecuteNonQuery();
            Disconnect();
            LoadThongTinSinhVien(comboBox_Lop.SelectedValue.ToString());
        }

        private void LoadThongTinSinhVien(String malop)
        {
            Connect();

            cmd = new SqlCommand("sp_Select", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@tenbang", SqlDbType.NVarChar).Value = "SINHVIEN where malop='"+ malop +"'";
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            Disconnect();
        }

        OpenFileDialog openFile = new OpenFileDialog();

        private void LayDuLieuTuForm()
        {
            mssv = textBox_MSSV.Text;
            hoten = textBox_HoTen.Text;
            gioitinh = comboBox_GioiTinh.SelectedIndex;
            ngaysinh = DateTime.Parse(comboBox_Nam.Text + "/" + comboBox_Thang.Text + "/" + comboBox_Ngay.Text);
            diachi = textBox_DiaChi.Text;
            malop = comboBox_Lop.SelectedValue.ToString();
        }

        private void LuuHinh()
        {
            if(openFile.FileName != "")
            {
                switch (openFile.FilterIndex)
                {
                    case 1:
                        pictureBox1.Image.Save(System.IO.Directory.GetCurrentDirectory() + @"\Images\"+textBox_HinhAnh.Text, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        pictureBox1.Image.Save(System.IO.Directory.GetCurrentDirectory() + @"\Images\" + textBox_HinhAnh.Text, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case 3:
                        pictureBox1.Image.Save(System.IO.Directory.GetCurrentDirectory() + @"\Images\" + textBox_HinhAnh.Text, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case 4:
                        pictureBox1.Image.Save(System.IO.Directory.GetCurrentDirectory() + @"\Images\" + textBox_HinhAnh.Text, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }
            }
        }
    }
}
