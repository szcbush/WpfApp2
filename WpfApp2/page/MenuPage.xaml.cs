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
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            // 返回主菜单
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                NavigationService.Navigate(new MenuPage());
            }
        }

        private void StartAssessment_Click(object sender, RoutedEventArgs e)
        {
            // 开始语言评估
            MessageBox.Show("开始语言发展评估测试", "评估开始",
                MessageBoxButton.OK, MessageBoxImage.Information);

            // 实际项目中这里会导航到评估页面
            // NavigationService.Navigate(new AssessmentPage());
        }
    }
}
