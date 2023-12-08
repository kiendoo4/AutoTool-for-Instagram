using DATH_IT008.OtherWindows;
using DATH_IT008.UserControl;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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

namespace DATH_IT008
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ChromeDriver> chromedrivers = new List<ChromeDriver>();
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Login login)
        {
            InitializeComponent();
            chromedrivers = login.chromedrivers;
        }
        private void HideClick(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow as Window;
            window.WindowState = WindowState.Minimized;
        }
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void ByeByeClick(object sender, RoutedEventArgs e)
        {
            //App.Current.Shutdown();
        }

        private void AutoCommentClick(object sender, RoutedEventArgs e)
        {
            CurrentUC.Children.Clear();
            AutoComment autoComment = new AutoComment(this);
            ContentForUC.Visibility = Visibility.Hidden;
            CurrentUC.Children.Add(autoComment);
        }

        private void AutoCraftImage_Click(object sender, RoutedEventArgs e)
        {
            CurrentUC.Children.Clear();
            AutoCraft autoCraft = new AutoCraft(this);
            ContentForUC.Visibility = Visibility.Hidden;
            CurrentUC.Children.Add(autoCraft);
        }


        private void PostClick(object sender, RoutedEventArgs e)
        {

        }

        private void Follow_Unfollow_Click(object sender, RoutedEventArgs e)
        {
            CurrentUC.Children.Clear();
            AutoFollow autoFollow = new AutoFollow(this);
            ContentForUC.Visibility = Visibility.Hidden;
            CurrentUC.Children.Add(autoFollow);
        }

        private void ReportClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
