using QuanLySanBong.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Windows.Forms;

namespace QuanLySanBong.DAO
{
    internal class test
    {

        //public BTVN_tblCongDan()
        //{
        //    InitializeComponent();
        //}
        string connectionString = @"Data Source=DESKTOP-C0C2AH8\SQLEXPRESS;Initial Catalog=btvn;Integrated Security=True";
        private void BTVN_tblCongDan_Load(object sender, EventArgs e)
        {
            layDSCD();
        }
        public void layDSCD()
        {
            try
            {
                using (SqlConnection Cnn = new SqlConnection(connectionString))
                {
                    using (SqlCommand Cmd = new SqlCommand("select * from tblCongDan", Cnn))
                    {
                        Cmd.CommandType = CommandType.Text;
                        Cnn.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(Cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            //dgv.DataSource = dt;
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
        public void resetForm()
        {
            //txtMaCD.Clear();
            //txtHoten.Clear();
            //dtp.Value = DateTime.Now;
            //txtSDT.Clear();
            //txtQueQuan.Clear();
            //txtSearch.Clear();
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
                        Cmd.CommandText = "themCD";
                        //Cnn.Open();
                        //Cmd.Parameters.AddWithValue("@maCD", txtMaCD.Text);
                        //Cmd.Parameters.AddWithValue("@hoTen", txtHoten.Text);
                        //Cmd.Parameters.AddWithValue("@ngaySinh", dtp.Value);
                        //Cmd.Parameters.AddWithValue("@gioiTinh", cbGioiTinh.Text);
                        //Cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                        //Cmd.Parameters.AddWithValue("@queQuan", txtQueQuan.Text);

                        //int i = Cmd.ExecuteNonQuery();
                        //if (txtMaCD.Text.Equals(""))
                        {
                            MessageBox.Show("Mã CD không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        //if (i == 0)
                        //{
                        //    MessageBox.Show("Thêm thất bại");
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Thêm thành công");
                        //}
                        Cnn.Close();
                        resetForm();
                        layDSCD();
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

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //txtMaCD.Text = dgv.CurrentRow.Cells["maCD"].Value.ToString();
            //txtHoten.Text = dgv.CurrentRow.Cells["hoten"].Value.ToString();
            //dtp.Text = dgv.CurrentRow.Cells["ngaySinh"].Value.ToString();
            //cbGioiTinh.Text = dgv.CurrentRow.Cells["gioiTinh"].Value.ToString();
            //txtSDT.Text = dgv.CurrentRow.Cells["sdt"].Value.ToString();
            //txtQueQuan.Text = dgv.CurrentRow.Cells["queQuan"].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection Cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand Cmd = new SqlCommand("capnhat", Cnn))
                {
                    //Cmd.CommandType = CommandType.StoredProcedure;
                    //Cmd.Parameters.AddWithValue("@maCD", txtMaCD.Text);
                    //Cmd.Parameters.AddWithValue("@hoTen", txtHoten.Text);
                    //Cmd.Parameters.AddWithValue("@ngaySinh", dtp.Value);
                    //Cmd.Parameters.AddWithValue("@gioiTinh", cbGioiTinh.Text);
                    //Cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                    //Cmd.Parameters.AddWithValue("@queQuan", txtQueQuan.Text);
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
                    layDSCD();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
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
                                Cmd.CommandText = "xoaCD";
                                Cnn.Open();
                                //Cmd.Parameters.AddWithValue("@maCD", txtMaCD.Text);
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
                                resetForm();
                                layDSCD();
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


        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection Cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand Cmd = new SqlCommand("timkiemtheoten", Cnn))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cnn.Open();
                    //Cmd.Parameters.AddWithValue("@hoTen", txtSearch.Text);
                    using (SqlDataAdapter da = new SqlDataAdapter(Cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        //dgv.DataSource = dt;
                    }
                    Cnn.Close();
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            layDSCD();
            resetForm();
        }

    }
}
