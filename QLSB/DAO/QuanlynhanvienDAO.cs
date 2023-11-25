using QuanLySanBong.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanBong.DAO
{
    public class QuanlynhanvienDAO
    {
        private static QuanlynhanvienDAO instance;

        public static QuanlynhanvienDAO Instance
        {
            get { if (instance == null) instance = new QuanlynhanvienDAO(); return QuanlynhanvienDAO.instance; }
            private set { QuanlynhanvienDAO.instance = value; }
        }
        private QuanlynhanvienDAO()
        {

        }
        public List<Quanlynhanvien> GetListQLNV()
        {
            List<Quanlynhanvien> list = new List<Quanlynhanvien>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * From dbo.NhanVien");
            foreach (DataRow item in data.Rows)
            {
                Quanlynhanvien quanlynhanvien = new Quanlynhanvien();
                list.Add(quanlynhanvien);
            }
            return list;
        }
            

        
    }
}

