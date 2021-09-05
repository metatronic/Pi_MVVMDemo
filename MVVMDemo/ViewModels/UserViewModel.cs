using MVVMDemo.Commands;
using MVVMDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMDemo.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPopertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly User domObject = new User();
        private ObservableCollection<User> _UserList;
        public ObservableCollection<User> Users
        {
            get => _UserList;
            set { _UserList = value; OnPopertyChanged("Users"); }
        }

        public string UserID
        {
            get => domObject.UserID.ToString();
            set
            {
                if (value != "")
                {
                    domObject.UserID = Convert.ToInt32(value);
                }
                else
                {
                    domObject.UserID = 0;
                }
                OnPopertyChanged("UserID");
            }
        }
        public string UserName
        {
            get => domObject.UserName;
            set { domObject.UserName = value; OnPopertyChanged("UserName"); }
        }
        public string MobileNo
        {
            get => domObject.MobileNo;
            set { domObject.MobileNo = value; OnPopertyChanged("MobileNo"); }
        }

        public User SelectedUser
        {
            set
            {
                if (value != null)
                {
                    UserID = value.UserID.ToString();
                    UserName = value.UserName;
                    MobileNo = value.MobileNo;
                }
            }
        }

        public UserViewModel()
        {
            Users = new ObservableCollection<User> { };
            AddUserCmd = new RelayCommand(Add, CanAdd);
            DeleteUserCmd = new RelayCommand(Delete, CanDelete);
            SearchUserCmd = new RelayCommand(Search, CanSearch);
            UpdateUser = new RelayCommand(Update, CanUpdate);
        }

        public ICommand AddUserCmd { get; set; }
        public ICommand SearchUserCmd { get; set; }
        public ICommand DeleteUserCmd { get; set; }
        public ICommand UpdateUser { get; set; }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                if (columnName == "UserID")
                {
                    if(UserID == "")
                    {
                        result = "User ID cannot be empty !";
                    }
                }
                return result;
            }
        }

        public bool CanAdd(object obj)
        {
            return UserID != "" && UserID != null && UserID != "0"
                && UserName != "" && UserName != null
                && MobileNo != "" && MobileNo != null
                && MobileNo.Length == 10;
        }
        public bool CanDelete(object obj)
        {
            return UserID != "" && UserID != "0";
        }
        public bool CanSearch(object obj)
        {
            return UserID != "" && UserID != "0";
        }
        public bool CanUpdate(object obj)
        {
            return UserID != null && UserID != "" && UserID != "0";
        }

        public void Add(object obj)
        {
            User user = new User
            {
                UserID = Convert.ToInt32(UserID),
                UserName = UserName,
                MobileNo = MobileNo
            };
            Users.Add(user);
        }
        public void Delete(object obj)
        {
            User userDelete = Users.FirstOrDefault(e => e.UserID.ToString() == UserID);
            if (userDelete != null)
            {
                _ = Users.Remove(userDelete);
            }
        }
        public void Search(object obj)
        {
            User userSearch = Users.FirstOrDefault(e => e.UserID.ToString() == UserID);
            if (userSearch != null)
            {
                UserName = userSearch.UserName;
                MobileNo = userSearch.MobileNo;
            }
        }
        public void Update(object obj)
        {
            User userUpdate = Users.FirstOrDefault(e => e.UserID.ToString() == UserID);
            if (userUpdate != null)
            {
                userUpdate.UserName = UserName;
                userUpdate.MobileNo = MobileNo;
            }
        }
    }
}
