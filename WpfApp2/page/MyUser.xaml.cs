using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2.page
{
    /// <summary>
    /// MyUser.xaml 的交互逻辑
    /// </summary>
    public partial class MyUser : Page
    {
        private ObservableCollection<UserInfo> _users;
        private string _statusMessage;
        public MyUser()
        {
            InitializeComponent();
            DataContext = this;
            LoadUsersFromDatabase();
        }
        public ObservableCollection<UserInfo> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        //public string StatusMessage
        //{
        //    get { return _statusMessage; }
        //    set
        //    {
        //        _statusMessage = value;
        //        OnPropertyChanged(nameof(StatusMessage));
        //    }
        //}

        // 从数据库加载用户数据
        private void LoadUsersFromDatabase()
        {
            try
            {
                // 这里替换为实际的数据库查询代码
                Users = new ObservableCollection<UserInfo>
                {
                    new UserInfo { UserId = "0005", Name = "测试1号", Gender = "男", Birthday = new DateTime(2023, 4, 2), PhoneNumber = "9" },
                    new UserInfo { UserId = "0004", Name = "测试1号", Gender = "男", Birthday = new DateTime(2023, 4, 2), PhoneNumber = "9" },
                    new UserInfo { UserId = "0003", Name = "测试1号", Gender = "男", Birthday = new DateTime(2023, 4, 2), PhoneNumber = "1234234324" },
                    new UserInfo { UserId = "0002", Name = "测试1号", Gender = "男", Birthday = new DateTime(2023, 4, 2), PhoneNumber = "1234234324" },
                    new UserInfo { UserId = "0001", Name = "测试1号", Gender = "男", Birthday = new DateTime(2023, 4, 2), PhoneNumber = "1234234324" },
                    new UserInfo { UserId = "001", Name = "小曾", Gender = "男", Birthday = new DateTime(2021, 12, 7), PhoneNumber = "157657989" }
                };

                //StatusMessage = $"共找到 {Users.Count} 个用户";
            }
            catch (Exception ex)
            {
                //StatusMessage = $"加载数据失败: {ex.Message}";
            }
        }

        // 查看报告按钮点击事件
        private void ViewReport_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string userId = button.Tag.ToString();
                // 这里实现查看报告的逻辑
                MessageBox.Show($"查看用户 {userId} 的报告");
            }
        }

        // 修改用户按钮点击事件
        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string userId = button.Tag.ToString();
                // 这里实现修改用户的逻辑
                MessageBox.Show($"修改用户 {userId} 的信息");
            }
        }

        // 添加用户按钮点击事件
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            // 这里实现添加用户的逻辑
            MessageBox.Show("打开添加用户窗口");
        }

        // INotifyPropertyChanged 实现
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenu());
        }
    }

    public class UserInfo : INotifyPropertyChanged
    {
        private string _userId;
        private string _name;
        private string _gender;
        private DateTime _birthday;
        private string _phoneNumber;

        public string UserId
        {
            get { return _userId; }
            set { _userId = value; OnPropertyChanged(nameof(UserId)); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; OnPropertyChanged(nameof(Gender)); }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; OnPropertyChanged(nameof(Birthday)); }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
