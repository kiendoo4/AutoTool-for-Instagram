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
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.IO;
using System.Threading;
using System.Net;

namespace InstaCrawl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChromeDriver driver = new ChromeDriver();
        public MainWindow()
        {
            InitializeComponent();
            login(ref driver, "altriatheclown", "12345678abc"); //Nhap account, co the truyen tham so khac
            search(ref driver, "thiziz"); //search user voi keyword cho truoc
            crawl(ref driver, "Test", 50);
        }

        private void login(ref ChromeDriver driver, string username, string password)
        {
            driver.Url = "https://www.instagram.com/";
            driver.Navigate();
            Thread.Sleep(1000);
            try
            {
                IWebElement form = driver.FindElement(By.TagName("form"));
                Thread.Sleep(150);
                List<IWebElement> login = form.FindElements(By.TagName("input")).ToList();
                Thread.Sleep(150);
                login[0].SendKeys(username);
                Thread.Sleep(150);
                login[1].SendKeys(password);
                Thread.Sleep(150);
                List<IWebElement> buttons = form.FindElements(By.TagName("button")).ToList();
                foreach (IWebElement button in buttons)
                {
                    if (button.GetAttribute("type") == "submit")
                    {
                        button.Click();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void search(ref ChromeDriver driver, string seachStr)
        {
            //Chờ web đã đăng nhập
            while (driver.FindElement(By.TagName("html")).GetAttribute("class").Contains("_aa4c"))
            {
                Thread.Sleep(500);
            }
            //Mở thanh seach
            try
            {
                IWebElement body = driver.FindElement(By.TagName("body"));
                Thread.Sleep(200);
                List<IWebElement> svg = body.FindElements(By.TagName("svg")).ToList();
                IWebElement search = null;
                foreach (IWebElement element in svg)
                {
                    if (element.GetAttribute("aria-label") == "Search")
                    {
                        search = element;
                        Thread.Sleep(200);
                        break;
                    }
                }
                search.Click();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //Chọn user đầu tiên
            try
            {
                IWebElement body = driver.FindElement(By.TagName("body"));
                Thread.Sleep(50);
                IWebElement search = body.FindElement(By.TagName("input"));
                Thread.Sleep(50);
                search.SendKeys(seachStr + "\n");
                Thread.Sleep(700);
                IWebElement elder = body.FindElement(By.XPath(".//div[2]/div/div/div[@class='x9f619 x1n2onr6 x1ja2u2z']"));
                Thread.Sleep(50);
                IWebElement grand = elder.FindElement(By.XPath(".//div/div/div/div/div[1]"));
                Thread.Sleep(50);
                grand = grand.FindElement(By.XPath(".//div[1]/div/div"));
                Thread.Sleep(50);
                grand = grand.FindElement(By.XPath(".//div[@class='x10l6tqk x1u3tz30 x1ja2u2z']/div/div"));
                Thread.Sleep(50);
                grand = grand.FindElement(By.XPath(".//div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1n2onr6 x1plvlek xryxfnj x1iyjqo2 x2lwn1j xeuugli xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']/div/div"));
                Thread.Sleep(50);
                grand = grand.FindElement(By.XPath(".//div[@class='x6s0dn4 x78zum5 xdt5ytf x5yr21d x1odjw0f x1n2onr6 xh8yej3']/div"));
                Thread.Sleep(50);
                List<IWebElement> userList = grand.FindElements(By.TagName("a")).ToList();
                userList[0].Click();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void crawl(ref ChromeDriver driver, string target, int quantity)
        {
            string path = $".\\downloaded\\{target}";
            if (Directory.Exists(path))
            {
                MessageBox.Show("Đối tượng đã tồn tại.");
                return;
            }
            else
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                //wait for user info loading
                Thread.Sleep(5000);
                HashSet<string> urlBag = new HashSet<string>();

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                long lastHeight = (long)js.ExecuteScript("return document.body.scrollHeight;");

                while (true)
                {
                    // Cuộn xuống cuối trang
                    js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                    // Chờ một khoảng thời gian ngắn để trang web tải dữ liệu mới
                    System.Threading.Thread.Sleep(1500); //co the can tang thoi gian cho neu mang cham
                    //lay cac element chua anh--------------------------------------------------
                    IWebElement article = driver.FindElement(By.TagName("article"));
                    Thread.Sleep(50);
                    List<IWebElement> img = article.FindElements(By.XPath(".//div[@class='_ac7v  _al3n']/div[@class='_aabd _aa8k  _al3l']/a/div/div/img")).ToList();
                    for (int i = 0; i < img.Count; i++)
                    {
                        string imageUrl = img[i].GetAttribute("src");
                        urlBag.Add(imageUrl);
                        Thread.Sleep(50);
                    }
                    //Lấy chiều cao mới của trang-----------------------------------------------
                    long newHeight = (long)js.ExecuteScript("return document.body.scrollHeight;");
                    // Kiểm tra xem đã đến cuối trang hay chưa
                    if (newHeight == lastHeight) break;
                    lastHeight = newHeight;
                }
                //download----------------------------------------------------------------------
                int count = 1;
                foreach (string url in urlBag)
                {
                    string localPath = $".\\downloaded\\{target}\\image{(count++).ToString()}.png";
                    using (WebClient webClient = new WebClient())
                    {
                        webClient.DownloadFile(url, localPath);
                    }
                    Thread.Sleep(50);
                }
                MessageBox.Show("Done crawling");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
