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
using QuanLySanBong.DTO;
using QuanLySanBong.DAO;

namespace QuanLySanBong
{
    public partial class Thongtintaikhoan : Form
    {
        private string connectionString = @"Data Source=LAPTOP-B6TL2FTQ;Initial Catalog=QuanLySanBong;Integrated Security=True";
 
        public Thongtintaikhoan()
        {
            InitializeComponent();
        }

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    Trangchu f = new Trangchu();
        //    f.Show();
        //    this.Hide();
        //}

        private void cbhienthi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbhienthi.Checked)
            {
                txtmk.PasswordChar = (char)0;
                txtmkm.PasswordChar = (char)0;
                txtnmkm.PasswordChar = (char)0;
            }
            else
            {
                txtmk.PasswordChar = '*';
                txtmkm.PasswordChar = '*';
                txtnmkm.PasswordChar = '*';
            }
        }
        public bool KiemTra()
        {
            if (txtTenTK.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản !!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTK.Focus();
                return false;
            }
         
            else if (txtmk.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại !!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmk.Focus();
                return false;
            }
            else if (txtmkm.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới !!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmkm.Focus();
                return false;
            }
            else if (txtnmkm.Text == "")
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu !!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnmkm.Focus();
                return false;
            }
            else if (txtmkm.Text != txtnmkm.Text)
            {
                MessageBox.Show("Mật khẩu mới và mật khẩu xác nhận không trùng khớp", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnmkm.Focus();
                txtnmkm.SelectAll();
                return false;
            }
            else
            {
                // Nếu không có lỗi, có thể hiển thị thông báo thành công
                MessageBox.Show("Kiểm tra thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (KiemTra())
            {
                try
                {
                    using (SqlConnection Cnn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand Cmd = new SqlCommand())
                        {
                            Cmd.Connection = Cnn;
                            Cmd.CommandType = CommandType.StoredProcedure;
                            Cmd.CommandText = "SP_Update_Pass";
                            Cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = txtTenTK.Text;
                            Cmd.Parameters.Add("@OldPass", SqlDbType.NVarChar).Value = txtmk.Text;
                            Cmd.Parameters.Add("@NewPass", SqlDbType.NVarChar).Value = txtmkm.Text;
                            Cmd.Parameters.Add("@DisplayName", SqlDbType.NVarChar).Value = txtTHT.Text;
                            Cmd.Parameters.Add("@SDT", SqlDbType.NVarChar).Value = txtSDT.Text;
                            Cmd.Connection = Cnn;
                            Cnn.Open();

                            using (SqlDataReader dr = Cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    if (dr.GetInt32(0) == 1)
                                    {
                                        MessageBox.Show(dr.GetString(1), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtnmkm.Text = "";
                                        txtmk.Text = "";
                                        txtmkm.Text = "";
                                        txtmk.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show(dr.GetString(1), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtmk.Focus();
                                        txtmk.SelectAll();
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Thongtintaikhoan_Load(object sender, EventArgs e)
        {

        }
    }
}
