using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V117.FedCm;
using OpenQA.Selenium.Interactions;
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
using static System.Net.WebRequestMethods;

namespace Instagram_tool
{
    /// <summary>
    /// Interaction logic for FastPost_invidual.xaml
    /// </summary>
    public partial class FastPost_invidual : Page
    {
        private string path;
        private Abilities ab = new Abilities();    
        public FastPost_invidual(string path)
        {
            this.path = path;
            InitializeComponent();
        }
        // for testing 
        private ChromeOptions LoadProfile()
        {
            
            ChromeOptions options = new ChromeOptions();
            string ProfilePath = "C:\\Users\\Admin\\AppData\\Local\\Google\\Chrome\\User Data\\TestUser"; // change this directory to your chrome profile path if you use this code
            if (!Directory.Exists(ProfilePath))
            {
                Directory.CreateDirectory(ProfilePath);
            }
            options.AddArgument("user-data-dir=" + ProfilePath);
            return options;
        }

        //for login
        private void login(ChromeDriver driver, string username, string password)
        {
            driver.Navigate().GoToUrl("https://www.instagram.com");
            var us = driver.FindElement(By.XPath("//input[@name='username']"));
            us.SendKeys(username);
            var pa = driver.FindElement(By.XPath("//input[@name='password']"));
            pa.SendKeys(password);
            var button = driver.FindElement(By.XPath("//button[@type='submit']"));
            button.Click();
        }

        //init components (multiple account)
        private void lets_post(ChromeDriver driver, string username, string password, string file_s, string caption, bool hidelike, bool turnoffcmt, bool rndcap)
        {
            login(driver, username, password);
            Thread.Sleep(5000);// Use wait here but i am lazy :(

            ab.post(driver, path, file_s, caption, hidelike, turnoffcmt, rndcap);
        }

        // Click btn_post event
        private void btn_post_Click(object sender, RoutedEventArgs e)
        {
            string filePath, content, file_s;
            ChromeDriver driver;
            // for multiple account
            if (chk_multi_acc.IsChecked == true)
            {
                // load account.txt
                filePath = "D:\\vs\\Instagram_tool\\Instagram_tool\\txt_files\\account.txt";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    content = reader.ReadToEnd();
                }
                string[] list_acc = content.Split("\r\n");

                // use the code in file txt
                if (chk_filetxt.IsChecked == true)
                {
                    filePath = "D:\\vs\\Instagram_tool\\Instagram_tool\\txt_files\\FastPost\\FastPost_indv_multiacc.txt"; // change this to the same exe file if you use this function
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        content = reader.ReadToEnd();
                    }
                }

                // use the code in textbox
                else
                {
                    content = tbx_multi_acc.Text;
                }
                string[] activities = content.Split("\r\n");
                foreach(string activity in activities)
                {
                    string[] part = activity.Split("\\");
                    string[] acc = list_acc[int.Parse(part[0])-1].Split(" ");
                    string[] files = part[1].Split("@");
                    file_s = "";
                    for (int i = 0; i < files.Length ; i++)
                    {
                        file_s += "\"" + files[i] + "\"";
                    }
                    ChromeDriver temp = new ChromeDriver();
                    temp.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                    //Không dùng đồng thời được do bị xung đột với file autoit
                    /*Thread t = new Thread(() => lets_post(temp, acc[0], acc[1], file_s, part[2], bool.Parse(part[3]), bool.Parse(part[4]), bool.Parse(part[5])));
                    t.Start();*/
                    lets_post(temp, acc[0], acc[1], file_s, part[2], bool.Parse(part[3]), bool.Parse(part[4]), bool.Parse(part[5])); // dùng lần lượt (thay thế)
                    temp.Quit();
                }
            }
            // for individual account
            else
            {
                // start app
                driver = new ChromeDriver(LoadProfile());
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); // change max wait time (second) here
                driver.Manage().Window.Maximize();

                // init components for the post function
                bool hidelike = chk_hidelike.IsChecked ?? false;
                bool turnoffcmt = chk_turnoffcmt.IsChecked ?? false;
                bool rndcap = chk_rndcap.IsChecked ?? false;
                int numfiles, start_pos;
                string[] files;

                // init files
                if (tbx_files.Text != "")
                {
                    files = tbx_files.Text.Split("\r\n");
                    start_pos = 0;
                    numfiles = files.Length;
                }
                else
                {
                    files = Directory.GetFiles(path);
                    if (chk_allfiles.IsChecked ?? false) numfiles = 10;
                    else numfiles = int.Parse(tbx_numfiles.Text);
                    numfiles = numfiles > 10 ? 10 : numfiles;
                    start_pos = int.Parse(tbx_startpos.Text);
                }
                file_s = "";
                for (int i = start_pos; i < numfiles + start_pos; i++)
                {
                    file_s += "\"" + files[i] + "\"";
                }

                // post function
                ab.post(driver, path, file_s, tbx_caption.Text, hidelike, turnoffcmt, rndcap);

                driver.Quit();
            }
            MessageBox.Show("Complete", "Complete", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
