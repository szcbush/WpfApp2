using Microsoft.Data.Sqlite;
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
using WpfApp2.page.entity;
namespace WpfApp2.page
{
    /// <summary>
    /// MyUser.xaml 的交互逻辑
    /// </summary>
    public partial class MyUser : Page
    {
        private ObservableCollection<UserInfo> _users;
        private string _statusMessage;


        private const string ConnectionString = "Data Source=users.db";
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

        // 从数据库加载用户数据
        private void LoadUsersFromDatabase()
        {
            try
            {
                // 这里替换为实际的数据库查询代码
                ObservableCollection < UserInfo > u1 = new ObservableCollection<UserInfo>();
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT id,username,gender,birthday,phone_number from users";
                    using (var cmd = new SqliteCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                u1.Add(new UserInfo
                                {
                                    UserId = reader["id"].ToString(),
                                    Name = reader["username"].ToString(),
                                    Gender = reader["gender"].ToString(),
                                    Birthday = DateTime.Parse(reader["birthday"].ToString()),
                                    PhoneNumber = reader["phone_number"].ToString()
                                });
                             }
                            Users = u1;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

}
