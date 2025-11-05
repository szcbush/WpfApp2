using System;
using System.Collections.Generic;
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
using Microsoft.Data.Sqlite;
using System.IO;


namespace WpfApp2.page
{
    public partial class LoginPage : Page
    {
        private const string DatabaseFile = "users.db";
        private const string ConnectionString = "Data Source=users.db";

        public LoginPage()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                UsernameTextBox.Focus();
                InitializeDatabase(); // 启动时自动初始化数据库
            };
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入用户名和密码！", "登录失败",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ValidateLogin(username, password))
            {
                NavigationService?.Navigate(new MainMenu());
            }
            else
            {
                MessageBox.Show("用户名或密码错误！", "登录失败",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateLogin(string username, string password)
        {
            try
            {
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE username=@username AND password=@password";
                    using (var cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        long count = (long)(cmd.ExecuteScalar() ?? 0);
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"数据库错误：{ex.Message}", "错误",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void InitializeDatabase()
        {
            try
            {
                if (!File.Exists(DatabaseFile))
                {
                    using (var conn = new SqliteConnection(ConnectionString))
                    {
                        conn.Open();

                        string createTable = @"
                            CREATE TABLE IF NOT EXISTS users (
                                id INTEGER PRIMARY KEY AUTOINCREMENT,
                                username TEXT NOT NULL UNIQUE,
                                password TEXT NOT NULL,
                                gender TEXT NOT NULL,
                                birthday DATE NOT NULL,
                                phone_number TEXT NOT NULL
                            );
                        ";

                        using (var cmd = new SqliteCommand(createTable, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        string insertDefaultUser = @"
                            INSERT INTO users (id,username, password,gender,birthday,phone_number)
                            VALUES (1,'admin', '123456','女','2000-07-27','17574611406'),
                            (2,'szc', '111111','男','1999-03-17','13865265471'),
                            (3,'wtj', '444444','男','1987-04-21','13365859654');
                        ";

                        using (var cmd = new SqliteCommand(insertDefaultUser, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("数据库初始化完成！默认账号：admin / 123456",
                        "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"数据库初始化失败：{ex.Message}", "错误",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

