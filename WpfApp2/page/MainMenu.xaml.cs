using System.Windows;
using System.Windows.Controls;

namespace WpfApp2.page
{
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void NavigateToPage(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            Page targetPage = null;

            // 根据按钮名称导航到不同页面
            switch (button.Name)
            {
                case "LanguageKnowledgeBtn":
                    targetPage = new MenuPage(); // 导航到你的 MenuPage
                    break;
                //case "DevelopmentAssessmentBtn":
                //    targetPage = new AssessmentPage(); // 需要创建这个页面
                //    break;
                //case "TrainingGuideBtn":
                //    targetPage = new TrainingPage(); // 需要创建这个页面
                //    break;
                case "ProgressReportBtn":
                    targetPage = new MyUser(); // 需要创建这个页面
                    break;
            }

            if (targetPage != null)
            {
                // 获取导航服务并导航
                NavigationService?.Navigate(targetPage);
            }
        }
    }
}