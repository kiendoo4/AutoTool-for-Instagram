using System.Diagnostics;
using System.IO;
using System.Net.Http;
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


namespace AutoRp
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



        private void btn_rp(object sender, RoutedEventArgs e)
        {
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--user-data-dir=C:\\Users\\PHAT PC\\AppData\\Local\\Google\\Chrome\\User Data\\");
            chromeOptions.AddArgument("--profile-directory=Profile 3");

            var driver = new ChromeDriver(chromeDriverService, chromeOptions);

            driver.Navigate().GoToUrl("https://www.instagram.com/");
            var username = tb_value.Text;

            driver.Navigate().GoToUrl($"https://www.instagram.com/{username}/");

            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
            var options = driver.FindElement(By.XPath("//div[@class='x1i10hfl x6umtig x1b1mbwd xaqea5y xav7gou x9f619 xe8uvvx xdj266r x11i5rnm xat24cr x1mh8g0r x16tdsg8 x1hl2dhg xggy1nq x1a2a7pz x6s0dn4 xjbqb8w x1ejq31n xd10rxx x1sy0etr x17r0tee x1ypdohk x78zum5 xl56j7k x1y1aw1k x1sxyh0 xwib8y2 xurb0ha xcdnw81']//div[@class='x6s0dn4 x78zum5 xdt5ytf xl56j7k']//*[@aria-label='Options']"));
            options.Click();

            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
            var report = driver.FindElement(By.XPath("//button[normalize-space()='Report']"));
            report.Click();

            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
            var reason = driver.FindElement(By.XPath("//div[normalize-space()='Report Account']"));
            reason.Click();

            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
            var pretending = driver.FindElement(By.XPath("//body"));
            pretending.Click();

            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
            var choose = driver.FindElement(By.XPath("//div[@class='_abn2'][normalize-space()='Me']"));
            choose.Click();

            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
            var submit = driver.FindElement(By.XPath("//button[normalize-space()='Submit report']"));
            submit.Click();

            //Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
            //var close = driver.FindElement(By.XPath(""));
            //close.Click();

        }
    }
}