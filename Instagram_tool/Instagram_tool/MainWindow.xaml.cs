using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Instagram_tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChromeDriver driver;
        private void LoadProfile()
        {
            ChromeOptions options = new ChromeOptions();
            string ProfilePath = "C:\\Users\\Admin\\AppData\\Local\\Google\\Chrome\\User Data\\TestUser"; // change this directory to your chrome profile path if you use this code
            if (!Directory.Exists(ProfilePath))
            {
                Directory.CreateDirectory(ProfilePath);
            }
            options.AddArgument("user-data-dir=" + ProfilePath);
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); // change max wait time (second) here
            driver.Manage().Window.Maximize();
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_liketool_Click(object sender, RoutedEventArgs e)
        {
            Liketool liketool = new Liketool();
            liketool.Show();
        }

        private void btn_fastpost_Click(object sender, RoutedEventArgs e)
        {
            FastPost fastPost = new FastPost();
            fastPost.Show();
        }

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            LoadProfile();
            driver.Navigate().GoToUrl("https://www.instagram.com/hatakacder/");
            var a = driver.FindElement(By.XPath("//button[contains(@class,'_acan _acap _acat _aj1- _ap30')]"));
            a.Click();
            Thread.Sleep(3000);
            a = driver.FindElement(By.XPath("//span[contains(text(),'Unfollow')]"));
            a.Click();

        }
    }
}
