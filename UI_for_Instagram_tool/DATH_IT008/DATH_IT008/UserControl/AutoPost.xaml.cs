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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net.Http;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.IO;
using System.Net;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Configuration;
using DATH_IT008.OtherWindows;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using EnvDTE80;
using Path = System.IO.Path;

namespace DATH_IT008.UserControl
{
    /// <summary>
    /// Interaction logic for AutoPost.xaml
    /// </summary>

    public partial class AutoPost
    {
        public List<ChromeDriver> chromedrivers = new List<ChromeDriver>();
        private List<string> selectedImagePaths = new List<string>();
        List<string> acc_username = new List<string>();
        List<string> acc_password = new List<string>();
        //List<string> commentList = new List<string>();
        bool a = false, b = false;
        public AutoPost()
        {
            InitializeComponent();
            //Cb_choose.SelectedIndex = 0;
            //LabelToShow.Content = "Đăng một bài duy nhất";
        }
        bool login(string us, string pa, ChromeDriver driver)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.instagram.com");
            driver.Navigate().GoToUrl(client.BaseAddress);
            var username = driver.FindElement(By.CssSelector("input[name='username']"));
            username.SendKeys(us);
            var password = driver.FindElement(By.CssSelector("input[name='password']"));
            password.SendKeys(pa);
            var button = driver.FindElement(By.CssSelector("button[class=' _acan _acap _acas _aj1- _ap30']"));
            button.Click();
            Thread.Sleep(5000);
            if (driver.Url != "https://www.instagram.com")
            {
                return true;
                //MainWindow mainWindow = new MainWindow(this);
                //mainWindow.Show();
                //this.Close();
            }
            return false;

        }
        private void FinishClick(object sender, RoutedEventArgs e)
        {
            if (CaptionList.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ít nhất 1 caption", "Thông báo");
            }
            else
            {
                if (acc_username.Count == 0 || acc_password.Count == 0)
                    MessageBox.Show("Vui lòng kiểm tra lại file excel", "Thông báo");
                else
                {
                    for (int i = 0; i < acc_username.Count; i++)
                    {
                        string username = acc_username[i];
                        string password = acc_password[i];
                        ChromeDriver driver2 = new ChromeDriver();
                        driver2.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                        try
                        {
                            login(username, password, driver2);
                        }
                        catch
                        {
                            driver2.Quit();
                        }
                        //Random num = new Random();
                        //int rand = num.Next(0, cnum);
                        foreach (var image_path in selectedImagePaths)
                        {
                            lets_post(driver2, image_path, CaptionList.Text, a, b);
                        }
                        driver2.Quit();
                        Thread.Sleep(5000);
                    }
                }
            }
        }

        private void ChooseAccountClick(object sender, RoutedEventArgs e)
        {
            acc_username = new List<string>();
            acc_password = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.Title = "Select an Excel File";

            if (openFileDialog.ShowDialog() == true)
            {
                string excelFilePath = openFileDialog.FileName;
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Open(excelFilePath);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                Excel.Range usedRange = worksheet.UsedRange;
                int rowCount = usedRange.Rows.Count;
                int colCount = usedRange.Columns.Count;
                for (int row = 1; row <= rowCount; row++)
                {
                    string column1 = Convert.ToString((usedRange.Cells[row, 1] as Excel.Range).Value2);
                    string column2 = Convert.ToString((usedRange.Cells[row, 2] as Excel.Range).Value2);
                    acc_username.Add(column1);
                    acc_password.Add(column2);
                }

                workbook.Close(false);
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                MessageBox.Show("Đã chọn file, vui lòng bấm vào nút đăng nhập", "Thông báo");
            }
        }

        private void ChooseDirectoryClick(object sender, RoutedEventArgs e)
        {
            selectedImagePaths = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files|*.*"
            };
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    selectedImagePaths = openFileDialog.FileNames.ToList();
                }
            }
            catch
            {
                MessageBox.Show("Hủy thực thi", "Thông báo");
            }
        }
        public void post(ChromeDriver driver, string upload_path, string caption, bool hidelike, bool turnoffcmt)
        {
            driver.Navigate().GoToUrl("https://www.instagram.com");
            try
            {
                var i = driver.FindElement(By.XPath("//button[normalize-space()='Not Now']"));
                i.Click();
            }
            catch
            {
                //nothing   
            }
            // Wait
            Thread.Sleep(500);
            // Click create + select files from computer buttons
            var temp = driver.FindElement(By.XPath("//*[@aria-label='New post']"));
            temp.Click();
            temp = driver.FindElement(By.XPath("//button[normalize-space()='Select from computer']"));
            temp.Click();

            //Select all files in upload_path directory and upload(using AutoIt)
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = "\\autoit_exe\\Select_folder.exe"; // change this to the same exe file if you use this function
            //startInfo.Arguments = upload_path;
            //process.StartInfo = startInfo;
            //process.Start();
            //Thread.Sleep(2000);
            //process.Close();

            string projectPath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;

            string absoluteFilePath = projectPath + "\\autoit_exe\\Upload_all_files.exe";
            startInfo.FileName = absoluteFilePath; // change this to the same exe file if you use this function
            startInfo.Arguments = upload_path;
            process.StartInfo = startInfo;
            process.Start();
            Thread.Sleep(2000);
            process.Close();

            // Click next button
            Thread.Sleep(2000);
            var webbtn_next = driver.FindElement(By.XPath("//div[contains(text(),'Next')]"));
            webbtn_next.Click();
            Thread.Sleep(500);
            webbtn_next = driver.FindElement(By.XPath("//div[contains(text(),'Next')]"));
            webbtn_next.Click();
            // Create new post
            temp = driver.FindElement(By.XPath("//div[@aria-label='Write a caption...']"));
            temp.SendKeys(caption); // Caption
            temp = driver.FindElement(By.XPath("//div[4]//div[1]//span[2]//*[@aria-label='Down chevron icon']"));
            temp.Click();
            temp = driver.FindElement(By.XPath("//div[5]//div[1]//span[2]//*[@aria-label='Down chevron icon']"));
            temp.Click();
            if (hidelike) // check if hide_like is checked or not
            {
                // turn on hide-likes-and-views feature
                temp = driver.FindElement(By.XPath("//div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1e56ztr x1n2onr6 x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']//input[@role='switch']"));
                temp.Click();
            }
            if (turnoffcmt) // check if turn_off_cmt is checked or not
            {
                // turn on turn-off-comment feature
                temp = driver.FindElement(By.XPath("//body/div[@class='x1n2onr6 xzkaem6']/div[@class='x9f619 x1n2onr6 x1ja2u2z']/div[@class='x78zum5 xdt5ytf xg6iff7 x1n2onr6 xwnxf6m']/div[@class='x1uvtmcs x4k7w5x x1h91t0o x1beo9mf xaigb6o x12ejxvf x3igimt xarpa2k xedcshv x1lytzrv x1t2pt76 x7ja8zs x1n2onr6 x1qrby5j x1jfb8zj']/div[@class='x1qjc9v5 x9f619 x78zum5 xdt5ytf x1iyjqo2 xl56j7k']/div[@class='x1cy8zhl x9f619 x78zum5 xl56j7k x2lwn1j xeuugli x47corl']/div[@aria-label='Create new post']/div[@class='xs83m0k x1sy10c2 x1h5jrl4 xieb3on xmn8rco x1iy3rx x1n2onr6']/div[@role='dialog']/div/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1n2onr6 x6ikm8r x10wlt62 x1iyjqo2 x2lwn1j xeuugli xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']/div[@class='x15wfb8v x3aagtl x6ql1ns x78zum5 xdl72j9 x1iyjqo2 xs83m0k x13vbajr x1ue5u6n']/div[@class='xhk4uv x26u7qi xy80clv x9f619 x78zum5 x1n2onr6 x1f4304s']/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1n2onr6 x1plvlek xryxfnj x1iyjqo2 x2lwn1j xeuugli xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']/div[@class='xmz0i5r xh8midk x1rife3k']/div/div[@class='_ac2p']/div[@class='_abm8']/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1pi30zi x1swvt13 xjkvuk6 x1iorvi4 x1n2onr6 x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x12nagc x1n2onr6 x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x6s0dn4 x1oa3qoh x1nhvcw1']/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1n2onr6 x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1n2onr6 x1plvlek xryxfnj x1c4vz4f x2lah0s x1q0g3np xqjyukv x6s0dn4 x1oa3qoh x1qughib']/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1uhb9sk x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']/div[@class='x1ja2u2z x1a2a7pz x1bhb7k3 xfh8nwu xoqspk4 x12v9rci x138vmkv x972fbf xcfux6l x1qhh985 xm0m39n x9f619 x1rg5ohu x1hc1fzr x6ikm8r x10wlt62 xexx8yu x4uap5 x18d9i69 xkhd6sd x1n2onr6 x1eub6wo x19991ni x1d72o xxk0z11 x100vrsf']/input[1]"));
                temp.Click();
            }

            // Click upload post
            temp = driver.FindElement(By.XPath("//div[contains(text(),'Share')]"));
            temp.Click();

            // Wait for uploading
            WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[@class='x1lliihq x1plvlek xryxfnj x1n2onr6 x193iq5w xeuugli x1fj9vlw x13faqbe x1vvkbs x1s928wv xhkezso x1gmr53x x1cpjm7i x1fgarty x1943h6x x1i0vuye x1ms8i2q xo1l8bm x5n08af x2b8uid x4zkp8e xw06pyt x10wh9bi x1wdrske x8viiok x18hxmgj']")));

            // Close "create post"
            temp = driver.FindElement(By.XPath("//div[@class='x160vmok x10l6tqk x1eu8d0j x1vjfegm']//div[@class='x6s0dn4 x78zum5 xdt5ytf xl56j7k']//*[@aria-label='Close']"));
            temp.Click();

            Thread.Sleep(5000); // Check if the function includes this function works or not (for testing)
        }
        private void lets_post(ChromeDriver driver, string image_path, string caption, bool hidelike, bool turnoffcmt)
        {
            post(driver, image_path, caption, hidelike, turnoffcmt);
        }
        private void MoreOptionClick(object sender, RoutedEventArgs e)
        {
            Extension extension = new Extension();
            extension.ShowDialog();
            a = extension.a; b = extension.b;
        }
    }
}
