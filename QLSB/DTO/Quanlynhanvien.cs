using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanBong.DTO
{
    public class Quanlynhanvien
    {

  
            public Quanlynhanvien(int iD_NV, string hoTen, string sDT,  string queQuan, string username)
            {   
                this.ID_NV = iD_NV;
                this.HoTen = hoTen;
                this.SDT = sDT;
                this.QueQuan = queQuan;
                this.Username = username;
            }
            public Quanlynhanvien(DataRow row)
            {
            this.ID_NV = (int)row["ID_NV"];
            this.HoTen = row["HoTen"].ToString();
            this.SDT = row["SDT"].ToString();
            this.QueQuan = row["QueQuan"].ToString();
            this.Username = row["Username"].ToString();
            }
            private int iD_NV;

            public int ID_NV
            {
                get { return iD_NV; }
                set { iD_NV = value; }
            }

            private string hoTen;

            public string HoTen
            {
                get { return hoTen; }
                set { hoTen = value; }
            }

            private string sDT;

            public string SDT
            {
                get { return sDT; }
                set { sDT = value; }
            }

            private string queQuan;

            public string QueQuan
            {
                get { return queQuan; }
                set { queQuan = value; }
            }

            private string username;

            public string Username
            {
                get { return username; }
                set { username = value; }
            }

           
        }
    }


