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
            // 默认显示第一个菜单的内容
            ShowContent(LanguageDevelopmentPanel);
            SetSelectedButton(LanguageDevelopmentBtn);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            // 隐藏所有内容面板
            HideAllContentPanels();

            // 根据按钮显示对应内容
            switch (button.Name)
            {
                case "LanguageDevelopmentBtn":
                    ShowContent(LanguageDevelopmentPanel);
                    break;
                case "VocabularyDevelopmentBtn":
                    ShowContent(VocabularyDevelopmentPanel);
                    break;
                case "SemanticDevelopmentBtn":
                    ShowContent(SemanticDevelopmentPanel);
                    break;
            }

            // 更新按钮选中状态
            SetSelectedButton(button);
        }

        private void HideAllContentPanels()
        {
            LanguageDevelopmentPanel.Visibility = Visibility.Collapsed;
            VocabularyDevelopmentPanel.Visibility = Visibility.Collapsed;
            SemanticDevelopmentPanel.Visibility = Visibility.Collapsed;
        }

        private void ShowContent(StackPanel panel)
        {
            panel.Visibility = Visibility.Visible;
        }

        private void SetSelectedButton(Button selectedButton)
        {
            // 重置所有按钮样式
            LanguageDevelopmentBtn.Style = (Style)FindResource("GradientBlueButton");
            VocabularyDevelopmentBtn.Style = (Style)FindResource("GradientBlueButton");
            SemanticDevelopmentBtn.Style = (Style)FindResource("GradientBlueButton");

            // 设置选中按钮样式
            selectedButton.Style = (Style)FindResource("SelectedButtonStyle");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenu());
        }
    }
}
