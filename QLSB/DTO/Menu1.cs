using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanBong.DTO
{
    public class Menu1
    {
        public Menu1(string ten, int giaTien)
        {

            
            this.Ten = ten;
            this.GiaTien = giaTien;

        }

        public Menu1(DataRow row)
        {
            
            this.Ten = row["Ten"].ToString();
            this.GiaTien = Convert.ToInt32(row["GiaTien"]);
        


        }

        private string ten;
        public string Ten
        {
            get { return ten; }
            set { ten = value; }
        }


        private int giaTien;
        public int GiaTien
        {
            get { return giaTien; }
            set { giaTien = value; }
        }

       
    }
}
