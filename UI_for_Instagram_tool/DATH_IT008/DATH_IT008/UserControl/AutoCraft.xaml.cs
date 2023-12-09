using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.UI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DATH_IT008.UserControl
{
    /// <summary>
    /// Interaction logic for AutoCraft.xaml
    /// </summary>
    public partial class AutoCraft
    {
        List<ChromeDriver> chromedrivers = new List<ChromeDriver>();
        List<string> directoryList = new List<string>();
        bool check2 = false;
        string saveDir = null;
        public AutoCraft()
        {
            InitializeComponent();
        }
        public AutoCraft(MainWindow mainWindow)
        {
            InitializeComponent();
            chromedrivers = mainWindow.chromedrivers;
        }
        private void ChooseDirectoryList(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt|All Files|*.*", // Filter for text files
                Title = "Chọn file txt"
            };
            // Show the dialog and check if the user clicked OK
            if (openFileDialog.ShowDialog() == true)
            {
                string textFilePath = openFileDialog.FileName;
                string[] fileContent = File.ReadAllLines(textFilePath);
                for (int i = 0; i < fileContent.Length; i++)
                {
                    if(Urluser.Text == "")
                        Urluser.Text = fileContent[i];
                    else Urluser.Text = Urluser.Text + '\n' + fileContent[i];
                }
                MessageBox.Show("Đã cập nhật danh sách đường dẫn", "Thông báo");
                check2 = true;
                if (check2)
                    FinishButton.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Hủy thực thi", "Thông báo");
            }
        }

        private void crawl(ref ChromeDriver driver, int quantity, string saveDirr)
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
            string rawNum = post.GetAttribute("innerText");
            rawNum = new string(rawNum.Where(char.IsDigit).ToArray());
            int maxQuantity = Convert.ToInt32(rawNum);
            if(quantity == 0)
            {
                quantity = maxQuantity;
            }    
            //Kiểm tra đối tượng đã được crawl chưa
            string path = $"{saveDirr}\\{target}";
            //MessageBox.Show(path);
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
                MessageBox.Show("Lỗi:" + ex.Message, "Thông báo");
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
                        string localPath = $"{saveDir}\\{target}\\image{(count).ToString()}.png";
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
                        string filePath = $"{saveDirr}\\{target}\\post{(count).ToString()}.txt";
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
            //
        }

        private void ChooseSaveFile(object sender, RoutedEventArgs e)
        {
            using (var folderDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = folderDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    saveDir = folderDialog.SelectedPath;
                    System.Windows.MessageBox.Show("Thư mục đã chọn: " + saveDir, "Thông báo");
                }
                else
                {
                    MessageBox.Show("Hủy thực thi", "Thông báo");
                }    
            }
        }

        private void FinishClick(object sender, RoutedEventArgs e)
        {
            foreach (var sthUrl in Urluser.Text.Split('\n'))
            {
                directoryList.Add($"https://www.instagram.com/{sthUrl}/");
            }
            ChromeDriver chromeDriver = (ChromeDriver)chromedrivers[0];
            for(int i = 0; i < directoryList.Count; i++)
            {
                chromeDriver.Navigate().GoToUrl(directoryList[i]);
                if (SL_Craft.Text != "")
                {
                    crawl(ref chromeDriver, Convert.ToInt32(SL_Craft.Text), saveDir);
                }
                else
                {
                    crawl(ref chromeDriver, 0, saveDir);
                }    
            }
            MessageBox.Show("Đã crawl xong", "Thông báo");
        }
    }
}
