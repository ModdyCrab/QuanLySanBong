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
using System.Xml.Linq;

namespace QuanLySanBong
{
    public partial class Quanlykh : Form
    {
        private string connectionString = @"Data Source=LAPTOP-B6TL2FTQ;Initial Catalog=QuanLySanBong;Integrated Security=True";
        bool Thoat = true;
        public Quanlykh()
        {
            InitializeComponent();
        }

        //private void logo_Click(object sender, EventArgs e)
        //{
        //    Trangchu f = new Trangchu();
        //    f.Show();
        //    this.Hide();
        //}

        //private void button9_Click(object sender, EventArgs e)
        //{
        //    Trangchu f = new Trangchu();
        //    f.Show();
        //    this.Hide();
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    Datsan f = new Datsan();
        //    f.Show();
        //    this.Hide();
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            Thanhtoan f = new Thanhtoan();
            f.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Quanlynhanvien f = new Quanlynhanvien();
            f.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Thongtintaikhoan f = new Thongtintaikhoan();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thoat = false;
            this.Close();
            FormDangNhap f = new FormDangNhap();
            f.ShowDialog();
        }

      

        private void Quanlykh_Load(object sender, EventArgs e)
        {
            layDS();
        }
        public void layDS()
        {
            try
            {
                using (SqlConnection Cnn = new SqlConnection(connectionString))
                {
                    using (SqlCommand Cmd = new SqlCommand("SELECT b.ID_KH, b.HoTen, b.SDT,b.FB ,b.SoLanDat,b.Loai_KH,a.DisplayName, a.Username, a.PassWord , a.UserType\r\nFROM dbo.Account AS a\r\nJOIN dbo.KhachHang AS b ON a.Username = b.Username\r\nWHERE a.UserType = '2'\r\nGROUP BY  b.ID_KH, b.HoTen, b.SDT,b.FB ,b.SoLanDat,b.Loai_KH,a.DisplayName, a.Username, a.PassWord , a.UserType", Cnn))
                    {
                        Cmd.CommandType = CommandType.Text;
                        Cnn.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(Cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dgv.DataSource = dt;
                        }
                        Cnn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgv.CurrentRow.Cells["HoTen"].Value.ToString();
            txtSDT.Text = dgv.CurrentRow.Cells["SDT"].Value.ToString();
            txtFB.Text = dgv.CurrentRow.Cells["FB"].Value.ToString();
            txtSLD.Text = dgv.CurrentRow.Cells["SoLanDat"].Value.ToString();
            txtLKH.Text = dgv.CurrentRow.Cells["Loai_KH"].Value.ToString();
            txtDisplayName.Text = dgv.CurrentRow.Cells["DisplayName"].Value.ToString();
            txtUserName.Text = dgv.CurrentRow.Cells["Username"].Value.ToString();
            txtPassWord.Text = dgv.CurrentRow.Cells["PassWord"].Value.ToString();
            txtUserType.Text = dgv.CurrentRow.Cells["UserType"].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
                try
                {
                    using (SqlConnection Cnn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand Cmd = new SqlCommand())
                        {
                            Cmd.Connection = Cnn;
                            Cmd.CommandType = CommandType.StoredProcedure;
                            Cmd.CommandText = "addKhachHang1";
                            Cnn.Open();
                            Cmd.Parameters.AddWithValue("@HoTen", txtName.Text);
                            Cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                            Cmd.Parameters.AddWithValue("@FB", txtFB.Text);
                            Cmd.Parameters.AddWithValue("@Loai_KH", txtLKH.Text);
                        Cmd.Parameters.AddWithValue("@SoLanDat", int.Parse(txtSLD.Text));
                        Cmd.Parameters.AddWithValue("@DisplayName", txtDisplayName.Text);
                            Cmd.Parameters.AddWithValue("@Username", txtUserName.Text);
                            Cmd.Parameters.AddWithValue("@PassWord", txtPassWord.Text);
                            Cmd.Parameters.AddWithValue("@UserType", int.Parse(txtUserType.Text));



                            int i = Cmd.ExecuteNonQuery();

                            if (i == 0)
                            {
                                MessageBox.Show("Thêm thất bại");
                            }
                            else
                            {
                                MessageBox.Show("Thêm thành công");
                            }
                            Cnn.Close();
                            layDS();
                        }
                    }
                }
                catch (System.FormatException ex)
                {
                    MessageBox.Show("Bạn phải điền đủ các trường dữ liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult kq = MessageBox.Show("Bạn nuốn xóa không?", "thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (kq == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection Cnn = new SqlConnection(connectionString))
                        {
                            using (SqlCommand Cmd = new SqlCommand())
                            {
                                Cmd.Connection = Cnn;
                                Cmd.CommandType = CommandType.StoredProcedure;
                                Cmd.CommandText = "xoakhachhang";
                                Cnn.Open();
                                Cmd.Parameters.AddWithValue("@Username", txtUserName.Text);
                                int i = Cmd.ExecuteNonQuery();
                                if (i == 0)
                                {
                                    Console.WriteLine("Insert success");
                                }
                                else
                                {
                                    Console.WriteLine("Insert fail");
                                }
                                Cnn.Close();
                                layDS();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Giá trị tham chiếu đến bản ghi khác", "Lỗi không xóa được");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi", "Bản ghi bị ràng buộc khóa");
            }
        }
        public void resetForm()
        {
            txttimkiem.Clear();
            txtName.Clear();
            txtSDT.Clear();
            txtSLD.Clear();
            txtLKH.Clear();
            txtDisplayName.Clear();
            txtUserName.Clear();
            txtPassWord.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection Cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand Cmd = new SqlCommand("capnhapKH", Cnn))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@HoTen", txtName.Text);
                    Cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                    Cmd.Parameters.AddWithValue("@SoLanDat", int.Parse(txtSLD.Text));
                    Cmd.Parameters.AddWithValue("@Loai_KH", txtLKH.Text);
                    Cmd.Parameters.AddWithValue("@FB", txtFB.Text);
                    Cmd.Parameters.AddWithValue("@DisplayName", txtDisplayName.Text);
                    Cmd.Parameters.AddWithValue("@Username", txtUserName.Text);
                    Cmd.Parameters.AddWithValue("@PassWord", txtPassWord.Text);
                    Cmd.Parameters.AddWithValue("@UserType", int.Parse(txtUserType.Text));
                    Cnn.Open();
                    int i = Cmd.ExecuteNonQuery();
                    if (i == 0)
                    {
                        MessageBox.Show("Cập nhật thất bại");
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thành công");
                    }
                    Cnn.Close();
                    resetForm();
                    layDS();
                }
            }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
                layDS();
                resetForm();
            
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            using (SqlConnection Cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand Cmd = new SqlCommand("SearcKH", Cnn))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cnn.Open();
                    Cmd.Parameters.AddWithValue("@HoTen", txttimkiem.Text);
                    using (SqlDataAdapter da = new SqlDataAdapter(Cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgv.DataSource = dt;
                    }
                    Cnn.Close();
                }
            }
        }
    }
}
