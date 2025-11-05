using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp2.page.entity;
namespace WpfApp2.page
{
    public partial class MyUser : Page
    {
        private ObservableCollection<UserInfo> _users;

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
                ObservableCollection<UserInfo> u1 = new ObservableCollection<UserInfo>();
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT id,username,gender,birthday,phone_number from users";
                    using var cmd = new SqliteCommand(query, conn);
                    using var reader = cmd.ExecuteReader();
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string userId = button.Tag.ToString();
                // 
            }
        }

        // 修改用户按钮
        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string userId = button.Tag.ToString();

                UserInfo user = new UserInfo();
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT id,username,gender,birthday,phone_number from users where id = @userId";
                    using var cmd = new SqliteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user = new UserInfo
                        {
                            UserId = reader["id"].ToString(),
                            Name = reader["username"].ToString(),
                            Gender = reader["gender"].ToString(),
                            Birthday = DateTime.Parse(reader["birthday"].ToString()),
                            PhoneNumber = reader["phone_number"].ToString()
                        };
                    }
                }
                EditUserIdText.Text = user.UserId;
                EditNameTextBox.Text = user.Name;
                EditBirthdayDatePicker.SelectedDate = user.Birthday;
                EditPhoneTextBox.Text = user.PhoneNumber;
                foreach (ComboBoxItem item in EditGenderComboBox.Items)
                {
                    if (item.Content.ToString() == user.Gender)
                    {
                        EditGenderComboBox.SelectedItem = item;
                        break;
                    }
                }

                EditModalOverlay.Visibility = Visibility.Visible;
            }
        }

        private void CloseEditModal_Click(object sender, RoutedEventArgs e)
        {
            closeModal();
        }

        private void closeModal() {
            EditModalOverlay.Visibility = Visibility.Collapsed;
            EditUserIdText.Text = string.Empty;
            EditNameTextBox.Text = string.Empty;
            EditGenderComboBox.SelectedIndex = 0;
            EditBirthdayDatePicker.SelectedDate = null;
            EditPhoneTextBox.Text = string.Empty;
        }

        // 修改
        // 修改
        private void SaveModal_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EditNameTextBox.Text))
            {
                MessageBox.Show("请输入姓名", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                EditNameTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(EditPhoneTextBox.Text))
            {
                MessageBox.Show("请输入电话号码", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                EditPhoneTextBox.Focus();
                return;
            }

            UserInfo user = new UserInfo();
            user.UserId = EditUserIdText.Text.Trim();
            user.Name = EditNameTextBox.Text.Trim();
            user.Gender = (EditGenderComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "男";
            user.Birthday = EditBirthdayDatePicker.SelectedDate ?? DateTime.Now;
            user.PhoneNumber = EditPhoneTextBox.Text.Trim();

            // 数据库修改用户代码
            try
            {
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    string query = @"UPDATE users 
                            SET username = @UserName, 
                                gender = @Gender, 
                                birthday = @Birthday, 
                                phone_number = @PhoneNumber 
                            WHERE id = @UserId";

                    using var cmd = new SqliteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserName", user.Name);
                    cmd.Parameters.AddWithValue("@Gender", user.Gender);
                    cmd.Parameters.AddWithValue("@Birthday", user.Birthday.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    cmd.Parameters.AddWithValue("@UserId", user.UserId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // 更新本地数据集合
                        var localUser = Users.FirstOrDefault(u => u.UserId == user.UserId);
                        if (localUser != null)
                        {
                            localUser.Name = user.Name;
                            localUser.Gender = user.Gender;
                            localUser.Birthday = user.Birthday;
                            localUser.PhoneNumber = user.PhoneNumber;

                            // 触发UI更新
                            OnPropertyChanged(nameof(Users));
                        }

                        MessageBox.Show("用户信息修改成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                        closeModal();
                    }
                    else
                    {
                        MessageBox.Show("修改失败，未找到对应的用户", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改用户信息失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 添加用户
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            
        }

       
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
