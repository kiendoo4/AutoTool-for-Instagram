using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Shapes;

namespace InstaCrawl
{
    /// <summary>
    /// Interaction logic for AutoCrawl.xaml
    /// </summary>
    public partial class AutoCrawl : Window
    {
        public AutoCrawl()
        {
            InitializeComponent();
        }

        private void login(ref ChromeDriver driver, string username, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = "https://www.instagram.com/";
            driver.Navigate();
            try
            {
                Random random = new Random();
                IWebElement form = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("form")));
                Thread.Sleep(100);
                List<IWebElement> login = form.FindElements(By.TagName("input")).ToList();
                Thread.Sleep(100);
                login[0].SendKeys(username);
                Thread.Sleep(random.Next(100, 150));
                login[1].SendKeys(password);
                Thread.Sleep(random.Next(100, 150));
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
            //Chờ page load data
            while (true)
            {
                IWebElement check = driver.FindElement(By.TagName("html"));
                if (check.GetAttribute("class").Contains("_aa4c")) Thread.Sleep(500);
                else break;
            }
            //Mở thanh seach
            try
            {
                Thread.Sleep(200);
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
                Thread.Sleep(1500);
                IWebElement elder = body.FindElement(By.XPath(".//div[2]/div/div/div[@class='x9f619 x1n2onr6 x1ja2u2z']"));
                Thread.Sleep(50);
                IWebElement grand = elder.FindElement(By.XPath(".//div/div/div[1]/div[1]/div[1]"));
                Thread.Sleep(50);
                grand = grand.FindElement(By.XPath(".//div/div"));
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

        private void gotoTarget(ref ChromeDriver driver, string targetUrl)
        {
            //Chờ page load data
            while (true)
            {
                IWebElement check = driver.FindElement(By.TagName("html"));
                if (check.GetAttribute("class").Contains("_aa4c")) Thread.Sleep(500);
                else break;
            }
            Thread.Sleep(500);
            driver.Navigate().GoToUrl(targetUrl);
        }

        private void crawl(ref ChromeDriver driver, int quantity)
        {
            //wait for user info loading
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement check = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("article")));
            Thread.Sleep(100);
            //Lấy tên target
            IWebElement post = driver.FindElement(By.TagName("main"));
            IWebElement targetName = post.FindElement(By.XPath(".//div/header/section/div[1]/a/h2"));
            string target = targetName.GetAttribute("innerText");
            //Lấy số lượng bài tối đa
            post = post.FindElement(By.XPath(".//div/header/section/ul/li[1]/span/span"));
            int maxQuantity = Convert.ToInt32(post.GetAttribute("innerText"));
            //Kiểm tra đối tượng đã được crawl chưa
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
            //Lấy url của bài viết
            HashSet<string> urlBag = new HashSet<string>();
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                while (true)
                {
                    // Cuộn xuống cuối trang
                    js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                    // Chờ một khoảng thời gian ngắn để trang web tải dữ liệu mới
                    System.Threading.Thread.Sleep(1000);
                    //lay cac element chua anh--------------------------------------------------
                    IWebElement article = driver.FindElement(By.TagName("article"));
                    Thread.Sleep(50);
                    List<IWebElement> href = article.FindElements(By.XPath(".//div[@class='_ac7v  _al3n']/div[@class='_aabd _aa8k  _al3l']/a")).ToList();
                    for (int i = 0; i < href.Count; i++)
                    {
                        string postUrl = href[i].GetAttribute("href");
                        urlBag.Add(postUrl);
                        Thread.Sleep(50);
                    }
                    // Kiểm tra nếu đã đủ số ảnh yêu cầu
                    if (urlBag.Count > quantity) break;
                    // Kiểm tra xem đã đến cuối trang hay chưa
                    if (urlBag.Count >= maxQuantity) break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //Crawl ảnh và comment
            try
            {
                int count = 1;
                foreach (string url in urlBag)
                {
                    IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
                    jsExecutor.ExecuteScript($"window.open('{url}', '_blank');");
                    driver.SwitchTo().Window(driver.WindowHandles[1]);
                    //Chờ element chứa bài viết đã load xong
                    WebDriverWait localWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    IWebElement main = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("main")));
                    Thread.Sleep(50);
                    main = main.FindElement(By.XPath(".//div/div[1]/div"));
                    //Img crawling-----------------------------------
                    IWebElement img = main.FindElement(By.XPath(".//div[1]/div/div/div/div"));
                    List<IWebElement> imgBag = img.FindElements(By.TagName("img")).ToList();
                    if (imgBag.Count > 0)
                    {
                        string imageUrl = imgBag[0].GetAttribute("src");
                        string localPath = $".\\downloaded\\{target}\\image{(count).ToString()}.png";
                        using (WebClient webClient = new WebClient())
                        {
                            webClient.DownloadFile(imageUrl, localPath);
                        }
                    }
                    Thread.Sleep(50);
                    //Cmt crawling-----------------------------------
                    IWebElement frame = main.FindElement(By.XPath(".//div[2]/div/div[2]/div"));
                    List<IWebElement> cmtList = main.FindElements(By.XPath(".//div[2]/div/div[2]/div/div[@class='x78zum5 xdt5ytf x1iyjqo2']/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1uhb9sk x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']")).ToList();
                    int cmtCount = cmtList.Count;
                    //int flag = 0;
                    //while (true)
                    //{
                    //    Actions actions = new Actions(driver);
                    //    actions.ScrollToElement(frame).MoveToElement(frame, frame.Size.Width - 5, frame.Size.Height - 5).Click().SendKeys(Keys.ArrowDown).Perform();
                    //    Thread.Sleep(100);
                    //    cmtList = main.FindElements(By.XPath(".//div[2]/div/div[2]/div/div[@class='x78zum5 xdt5ytf x1iyjqo2']/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1uhb9sk x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']")).ToList();
                    //    cmtCount = cmtList.Count;
                    //    if (cmtCount == cmtList.Count) flag++;
                    //    else flag = 0;
                    //    if (flag == 50) break;
                    //    if (cmtCount > 20) break;
                    //}
                    for (int i = 0; i < Math.Min(20, cmtCount); i++)
                    {
                        try
                        {
                            cmtList[i] = cmtList[i].FindElement(By.XPath(".//div/div/div[2]/div[1]/div[1]/div/div[2]/span"));
                        }
                        catch
                        {
                            Thread.Sleep(50);
                            continue;
                        }
                        string filePath = $".\\downloaded\\{target}\\post{(count).ToString()}.txt";
                        File.AppendAllText(filePath, (cmtList[i].GetAttribute("innerText") + "\n"));
                        Thread.Sleep(50);
                    }
                    //-----------------------------------------------
                    driver.Close();

                    driver.SwitchTo().Window(driver.WindowHandles[0]);

                    count++;
                    if (count > quantity) break;

                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Done crawling");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChromeDriver driver = new ChromeDriver();
            login(ref driver, tbUsername.Text, pbPassword.Password);
            gotoTarget(ref driver, tbSearchTarget.Text);
            crawl(ref driver, Convert.ToInt32(tbQuantity.Text));
        }
    }
}
