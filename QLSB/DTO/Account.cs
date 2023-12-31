﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanBong.DTO
{
    public class Account
    {
        public Account(string userName,string displayName,int userType, string passWord = null)
        {
            this.UserName = userName;
            this.DisplayName = displayName;
            this.UserType = userType;
            this.PassWord = passWord;
        }

        public Account(DataRow row)
        {
            this.UserName = row["Username"].ToString();
            this.DisplayName = row["DisplayName"].ToString();
            this.UserType = (int)row["UserType"];
            this.PassWord = row["PassWord"].ToString();
        }

        private int userType;

        public int UserType
        {
            get { return userType; }
            set { userType = value; }
        }

        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }

        private string displayName;

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private string userName;


        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}
