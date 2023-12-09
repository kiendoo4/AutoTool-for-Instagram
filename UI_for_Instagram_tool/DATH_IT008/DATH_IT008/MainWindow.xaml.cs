using DATH_IT008.OtherWindows;
using DATH_IT008.UserControl;
using DATH_IT008.View;
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
            leftGroup.CraftButton.ButtonClick += AutoCraftImage_Click;
            leftGroup.CommentButton.ButtonClick += AutoCommentClick;
            leftGroup.FLButton.ButtonClick += Follow_Unfollow_Click;
            leftGroup.ReportButton.ButtonClick += ReportClick;
            leftGroup.LikeUnlikeButton.ButtonClick += Auto_Like_Unlike_Button;
        }

        public MainWindow(Login login)
        {
            InitializeComponent();
            chromedrivers = login.chromedrivers;
            leftGroup.CraftButton.ButtonClick += AutoCraftImage_Click;
            leftGroup.CommentButton.ButtonClick += AutoCommentClick;
            leftGroup.FLButton.ButtonClick += Follow_Unfollow_Click;
            leftGroup.ReportButton.ButtonClick += ReportClick;
            leftGroup.LikeUnlikeButton.ButtonClick += Auto_Like_Unlike_Button;
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

        private void AutoCommentClick(object sender, EventArgs e)
        {
            CurrentUC.Children.Clear();
            AutoComment autoComment = new AutoComment(this);
            ContentForUC.Visibility = Visibility.Hidden;
            CurrentUC.Children.Add(autoComment);
        }

        private void AutoCraftImage_Click(object sender, EventArgs e)
        {
            ContentForUC.Visibility = Visibility.Hidden;
            leftGroup.CraftButton.IsButtonPressed = true;
            CurrentUC.Children.Clear();
            AutoCraft autoCraft = new AutoCraft(this);
            
            CurrentUC.Children.Add(autoCraft);
        }
        private void PostClick(object sender, RoutedEventArgs e)
        {

        }

        private void Follow_Unfollow_Click(object sender, EventArgs e)
        {
            CurrentUC.Children.Clear();
            AutoFollow autoFollow = new AutoFollow(this);
            ContentForUC.Visibility = Visibility.Hidden;
            CurrentUC.Children.Add(autoFollow);
        }

        private void ReportClick(object sender, EventArgs e)
        {
            CurrentUC.Children.Clear();
            AutoReport autoReport = new AutoReport(this);
            ContentForUC.Visibility = Visibility.Hidden;
            CurrentUC.Children.Add(autoReport);
        }

        private void Auto_Like_Unlike_Button(object sender, EventArgs e)
        {
            CurrentUC.Children.Clear();
            AutoLike autolike = new AutoLike(this);
            ContentForUC.Visibility = Visibility.Hidden;
            CurrentUC.Children.Add(autolike);
        }
    }
}
