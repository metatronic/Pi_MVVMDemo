using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMDemo.Models
{
    public class User : INotifyPropertyChanged
    {
        private int userID;

        public int UserID
        {
            get => userID;
            set { userID = value; OnPrpertyChanged("UserID"); }
        }

        private string userName;

        public string UserName
        {
            get => userName;
            set { userName = value; OnPrpertyChanged("UserName"); }
        }

        private string mobileNo;

        public string MobileNo
        {
            get => mobileNo;
            set { mobileNo = value; OnPrpertyChanged("MobileNo"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPrpertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}