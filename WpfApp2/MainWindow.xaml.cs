using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;
using WpfApp2;
using WpfApp2.page;

namespace LanguageAssessmentSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 启动时直接显示登录页面
            MainFrame.Navigate(new LoginPage());
        }
    }
}