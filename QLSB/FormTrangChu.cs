using QuanLySanBong.DAO;
using QuanLySanBong.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySanBong
{
    public partial class Trangchu : Form
    {


        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.UserType); }
        }

        bool Thoat = true;


        public Trangchu(Account acc)
        {

            InitializeComponent();
            this.LoginAccount = acc;

        }

      void ChangeAccount(int userType)
{

    button3.Enabled = userType == 0 || userType == 1;
    button4.Enabled = userType == 0 || userType == 1;
    button5.Enabled = userType == 0;
    button6.Enabled = userType == 0 || userType == 1;
}


        private void button1_Click(object sender, EventArgs e)
        {
            Thoat = false;
            this.Close();
            FormDangNhap f = new FormDangNhap();
            f.ShowDialog();
        }

        private void Trangchu_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Thoat)
                Application.Exit();
        }




        //private void button6_Click(object sender, EventArgs e)
        //{
        //    Quanlykh f = new Quanlykh();
        //    f.Show();
        //    this.Hide();
        //}


        //private void button3_Click(object sender, EventArgs e)
        //{
        //    Thanhtoan f = new Thanhtoan();
        //    f.Show();
        //    this.Hide();
        //}

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    Quanlynhanvien f = new Quanlynhanvien();
        //    f.Show();
        //    this.Hide();
        //}

        //private void button7_Click(object sender, EventArgs e)
        //{
        //    this.Hide();
        //    Thongtintaikhoan f = new Thongtintaikhoan();
        //    f.ShowDialog();
        //    Trangchu_Load(sender, e);
        //}

        private void Trangchu_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Datsan f = new Datsan(loginAccount); // Truyền dữ liệu vào constructor
            f.Show();
            this.Hide();
        }
    }
}