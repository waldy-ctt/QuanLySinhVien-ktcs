using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class QuanLySinhVien : Form
    {
        public QuanLySinhVien()
        {
            InitializeComponent();
        }

        private void nhapSinhVienMoiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frmSinhVien = new Form1();
            frmSinhVien.MdiParent = this;
            frmSinhVien.Show();
        }

        private void nhapLopMoiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyLop frmQuanLyLop = new QuanLyLop();
            frmQuanLyLop.MdiParent = this;
            frmQuanLyLop.Show();
        }
    }
}
