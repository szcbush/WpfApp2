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
using System.Windows.Shapes;

namespace WpfApp2.page
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            Loaded += (s, e) => UsernameTextBox.Focus();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            // 简单的登录验证
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入用户名和密码！", "登录失败",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 登录验证逻辑
            if (ValidateLogin(username, password))
            {
                // 登录成功，直接导航到菜单页面
                NavigationService.Navigate(new MenuPage());
            }
            else
            {
                MessageBox.Show("用户名或密码错误！", "登录失败",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateLogin(string username, string password)
        {
            // 示例验证逻辑
            return username == "admin" && password == "123456";
        }
    }
}
