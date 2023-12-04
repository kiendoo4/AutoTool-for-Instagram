using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for FastPost_multiple.xaml
    /// </summary>
    public partial class FastPost_multiple : Page
    {
        // init components
        private string path;
        private Abilities ab = new Abilities();
        private ChromeDriver driver;

        // for testing
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

        public FastPost_multiple(string path)
        {
            InitializeComponent();
            this.path = path;
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

        private void btn_Post_Click(object sender, RoutedEventArgs e)
        {
            
            string file_s = "";
            string caption = "";
            bool hidelike, turnoffcmt, rndcap;
            if (chk_multi_acc.IsChecked == true)
            {
                string filePath = "";
                string content = "";


                // load account.txt
                filePath = "D:\\vs\\Instagram_tool\\Instagram_tool\\txt_files\\account.txt";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    content = reader.ReadToEnd();
                }
                string[] list_acc = content.Split("\r\n");

                if (chk_code_multiacc.IsChecked == true)
                {
                    filePath = "D:\\vs\\Instagram_tool\\Instagram_tool\\txt_files\\FastPost\\FastPost_multi_multiacc.txt";
                    using (StreamReader stream = new StreamReader(filePath))
                    {
                        content = stream.ReadToEnd();
                    }
                }
                else
                {
                    content = tbx_code_multiacc.Text;
                }

                // regular expression cho việc lấy các thao tác của từng account
                string pattern = @"\d+\s\{[^}]*\}";
                Regex regex = new Regex(pattern);
                MatchCollection matches = regex.Matches(content);
                List<string> users_activities = new List<string>();
                foreach (Match match in matches)
                {
                    users_activities.Add(match.Value);
                }

                foreach (string l_activities in users_activities)
                {
                    string[] activities = l_activities.Split("\r\n");
                    string[] acc = list_acc[int.Parse(activities[0].Split(" ")[0])-1].Split(" ");
                    ChromeDriver temp = new ChromeDriver();

                    temp.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                    login(temp, acc[0], acc[1]);
                    Thread.Sleep(5000);
                    for (int i = 1; i < activities.Length - 1; i++)
                    {
                        string[] parts = activities[i].Split("\\");
                        int num = 1;
                        if (parts.Length == 5) caption = parts[num++];
                        hidelike = bool.Parse(parts[num++]);
                        turnoffcmt = bool.Parse(parts[num++]);
                        rndcap = bool.Parse(parts[num++]);

                        string[] filesz = parts[0].Split("@");
                        file_s = "";
                        for (int j = 0; j < filesz.Length; j++)
                        {
                            file_s += "\"" + filesz[j] + "\"";
                        }
                        
                        //Không dùng đồng thời được do bị xung đột với file autoit
                        /*Thread t = new Thread(() => lets_post(temp, acc[0], acc[1], file_s, part[2], bool.Parse(part[3]), bool.Parse(part[4]), bool.Parse(part[5])));
                        t.Start();*/
                        ab.post(temp, path, file_s, caption, hidelike, turnoffcmt, rndcap);
                    }
                    temp.Quit();
                }
            }
            else
            {
                LoadProfile();
                // init captions and files
                string[] captions = tbx_captions.Text.Split("\r\n");
                string[] files = tbx_files.Text.Split("\r\n");
                int length_post = int.Parse(tbx_numposts.Text);
                hidelike = chk_hidelike.IsChecked ?? false;
                turnoffcmt = chk_turnoffcmt.IsChecked ?? false;
                rndcap = chk_rndcap.IsChecked ?? false;

                if (chk_code_multisingle.IsChecked == true)
                {
                    string filePath = "D:\\vs\\Instagram_tool\\Instagram_tool\\txt_files\\FastPost\\FastPost_multi_single.txt"; // change this to the same exe file if you use this function
                    string content;
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        content = reader.ReadToEnd();
                    }
                    string[] activities = content.Split("\r\n");
                    foreach (string activity in activities)
                    {
                        caption = file_s = "";
                        string[] part = activity.Split("\\");
                        files = part[0].Split("@");
                        int num = 1;
                        if (part.Length == 5) caption = part[num++];
                        hidelike = bool.Parse(part[num++]);
                        turnoffcmt = bool.Parse(part[num++]);
                        rndcap = bool.Parse(part[num++]);
                        foreach (string file in files)
                        {
                            file_s += "\"" + file + "\"";
                        }
                        ab.post(driver, path, file_s, caption, hidelike, turnoffcmt, rndcap);
                    }
                }
                else
                {
                    for (int i = 0; i < length_post; i++)
                    {
                        // init caption and files per post
                        caption = "";
                        file_s = "";
                        string[] files_p;

                        caption = (i >= captions.Length) ? captions[0] : captions[i];
                        files_p = (i >= files.Length) ? files[0].Split(" ") : files[i].Split(" ");
                        foreach (string file in files_p)
                        {
                            file_s += "\"" + file + "\"";
                        }
                        ab.post(driver, path, file_s, caption, hidelike, turnoffcmt, rndcap);
                    }
                }
                driver.Quit();
            }
            MessageBox.Show("Complete", "Complete", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
