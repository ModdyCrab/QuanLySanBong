using QuanLySanBong.DAO;
using QuanLySanBong.DTO;
using QuanLySanBong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace QuanLySanBong
{
    public partial class Datsan : Form
    {
        private string connectionString = @"Data Source=LAPTOP-B6TL2FTQ;Initial Catalog=QuanLySanBong;Integrated Security=True";
        bool Thoat = true;
        string trangThai = "";
        private Account loginAccount;
        public Datsan(Account acc)
        {
            InitializeComponent();
            LoadTable();
            LoadCategory1();
            this.loginAccount = acc;
        }


        void LoadFoodListByCategoryID(int id)
        {
        
        }
        //private void logo_Click(object sender, EventArgs e)
        //{
        //    Trangchu f = new Trangchu();
        //    f.Show();
        //    this.Hide();
        //}

        //private void btDong_Click(object sender, EventArgs e)
        //{
        //    Trangchu f = new Trangchu();
        //    f.Show();
        //    this.Hide();
        //}
        private void Datsan_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Thoat)
                Application.Exit();
        }
        void LoadTable()
        {
            List<Table> tableList = TableDAO.Instance.LoadTableList();
            foreach (Table item in tableList)
            {
                Button btn = new Button()
                {
                    Width = TableDAO.TableWidth,
                    Height = TableDAO.TableHeight
                };
                btn.Text = item.Ten+Environment.NewLine+item.TrangThai;
                btn.Click += btn_Click;
                btn.Tag = item;

                switch(item.TrangThai)
                    {
                    case "Trống":
                        btn.BackColor = Color.AliceBlue;
                        break;
                    default: btn.BackColor = Color.Red; break;
                }
                flpTable.Controls.Add(btn);
                trangThai = item.TrangThai;
            }
           
        }
        
        void LoadCategory1()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            comboBox1.DataSource = listCategory;
            comboBox1.DisplayMember = "TenLoaiDV";
        }
        void GetDVByCategoryID(int id)
        {
            List<DV> listDV = DVDAO.Instance.GetDVByCategoryID(id);
            comboBox2.DataSource = listDV;
            comboBox2.DisplayMember = "TenDV";
        }


        void ShowBill(int id)
        {
            listView1.Items.Clear();
            tbTongTien.Clear();
            List<DTO.Menu> listBillInfo;
            List<DTO.Menu1> listBillInfo1 = MenuDAO1.Instance.GetList1MenuByTable(id);
            int TongTien = 0;

            // Lấy trạng thái từ MenuDAO1.Instance.GetList1MenuByTable
            string TrangThai = MenuDAO1.Instance.GetTableStatus(id);

            if (TrangThai == "Trống")
            {
                foreach (DTO.Menu1 item in listBillInfo1)
                {
                    ListViewItem lsvItem = new ListViewItem(item.Ten.ToString());
                    lsvItem.SubItems.Add(item.GiaTien.ToString());
                    listView1.Items.Add(lsvItem);
                    btnDatSan.Enabled = true;
                    btnThanhToan.Enabled = false;
                }
            }
            else
            {
                listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);

                foreach (DTO.Menu item in listBillInfo)
                {
                    ListViewItem lsvItem = new ListViewItem(item.Ten.ToString());
                    lsvItem.SubItems.Add(item.GiaTien.ToString());
                    lsvItem.SubItems.Add(item.TenDV.ToString());
                    lsvItem.SubItems.Add(item.GiaTienDV.ToString());
                    lsvItem.SubItems.Add(item.TongTien.ToString());
                    TongTien += item.TongTien;
                    listView1.Items.Add(lsvItem);
                    btnDatSan.Enabled = false;
                    btnThanhToan.Enabled = true;
                }


                CultureInfo culture = new CultureInfo("vi-VN");

                tbTongTien.Text = TongTien.ToString("c", culture);
            }
        }



        private void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID_KVSB;
            listView1.Tag = (sender as Button).Tag; // Đảm bảo bạn đã đặt giá trị đúng vào Tag
            ShowBill(tableID);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Thoat = false;
            this.Close();
            FormDangNhap f = new FormDangNhap();
            f.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Thongtintaikhoan f = new Thongtintaikhoan();
            f.Show();
            this.Hide();
        }

        private void flpTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            id = selected.ID_LDV;

            GetDVByCategoryID(id);
        }




        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            Table table = listView1.Tag as Table;

            int ID_KVSB = BillDAO.Instance.GetUncheckBillByTableID(table.ID_KVSB);

            if (MessageBox.Show("Bạn có chắc muốn trả sân " + table.Ten, "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                BillDAO.Instance.CheckOut(ID_KVSB);
                tbTongTien.Clear();
                LoadTable();
            }
        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
        
        }

        private void Datsan_Load(object sender, EventArgs e)
        {
            if (loginAccount != null)
            {
                layDS(loginAccount.UserName);
            }
        }
        public void layDS(string userName)
        {
            try
            {
                using (SqlConnection Cnn = new SqlConnection(connectionString))
                {
                    string query = "SELECT a.ID_HoaDon, e.ID_KVSB, e.Ten, e.GiaTien, c.TenDV, c.GiaTienDV, b.SoLuong, c.GiaTienDV * b.SoLuong + e.GiaTien AS TongTien, a.NgayLapBill, a.NgayXuatBill " +
                                   "FROM dbo.HoaDon AS a, dbo.ChiTietHoaDon AS b, dbo.DV AS c, dbo.ChiTietLichDat_SanBong AS d, dbo.KhuVuc_SanBong AS e, dbo.Account AS acc " +
                                   "WHERE a.ID_HoaDon = b.ID_HoaDon AND b.ID_DV = c.ID_DV AND a.ID_CTLDSB = d.ID_CTLDSB AND d.ID_KVSB = e.ID_KVSB AND acc.Username = @UserName";

                    using (SqlCommand Cmd = new SqlCommand(query, Cnn))
                    {
                        Cmd.Parameters.AddWithValue("@UserName", userName); // Chèn giá trị của userName vào câu truy vấn
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

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }
}
