using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V117.Profiler;
using OpenQA.Selenium.Firefox;

namespace Selenium1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--user-data-dir=C:\\Users\\PHAT PC\\AppData\\Local\\Google\\Chrome\\User Data\\");
            chromeOptions.AddArgument("--profile-directory=Profile 3");

            var driver = new ChromeDriver(chromeDriverService,chromeOptions);
            
            driver.Navigate().GoToUrl("https://www.instagram.com/");

           
            var usernamesToFollow = File.ReadAllLines("usernames.txt");

            foreach (var username in usernamesToFollow)
            {

                driver.Navigate().GoToUrl($"https://www.instagram.com/{username}/");


                Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));


                var button = driver.FindElement(By.CssSelector("button[class=' _acan _acap _acas _aj1- _ap30']"));

                button.Click();

            }


        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            try {
                System.IO.FileStream fs = new System.IO.FileStream("usernames.txt", FileMode.Create,FileAccess.Write, FileShare.None); 
                StreamWriter writer = new StreamWriter(fs);
                string data = tbValue.Text;
                data = data.Replace(" ", "\n");
               
                writer.WriteLine(data);
                writer.Flush();
                writer.Close();
                fs.Close();
                MessageBox.Show("Lưu user thành công");
                tbValue.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lưu user thất bại");
            }
            
            
            
        }
    }
}

        