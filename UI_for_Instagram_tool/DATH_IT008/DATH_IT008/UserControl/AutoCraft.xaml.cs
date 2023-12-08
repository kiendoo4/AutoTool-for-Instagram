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
        bool check1 = false, check2 = false;
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
            directoryList = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt|All Files|*.*", // Filter for text files
                Title = "Chọn file txt"
            };

            // Show the dialog and check if the user clicked OK
            if (openFileDialog.ShowDialog() == true)
            {
                string textFilePath = openFileDialog.FileName;
                string[] fileContent = System.IO.File.ReadAllLines(textFilePath);
                for (int i = 0; i < fileContent.Length; i++)
                {
                    directoryList.Add(fileContent[i]);
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

        private void crawl(ref ChromeDriver driver, string target, int quantity, string saveDir)
        {
            //Kiểm tra đối tượng đã được crawl chưa
            string path = saveDir;
            //wait for user info loading
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement check = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("article")));
            Thread.Sleep(100);
            //Lấy số lượng bài tối đa
            IWebElement post = driver.FindElement(By.TagName("main"));
            post = post.FindElement(By.XPath(".//div/header/section/ul/li[1]/span/span"));
            int maxQuantity = Convert.ToInt32(post.GetAttribute("innerText"));
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
            MessageBox.Show("Done crawling", "Thông báo");
        }

        private void ChooseSaveFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Set the properties of the SaveFileDialog
            saveFileDialog.Title = "Select a directory to save the file";
            saveFileDialog.Filter = "All Files|*.*"; // You can set specific filters if needed
            saveFileDialog.FileName = "NewFile.txt"; // Default file name
            if (saveFileDialog.ShowDialog() == true)
            {
                // Get the selected directory
                string saveDir = System.IO.Path.GetDirectoryName(saveFileDialog.FileName);

                // Now you can use the selectedDirectory for saving your file or perform other actions
                MessageBox.Show("Thư mục đã chọn: " + saveDir, "Thông báo");
            }
            else
            {
                
            }
        }

        private void FinishClick(object sender, RoutedEventArgs e)
        {
            ChromeDriver chromeDriver = (ChromeDriver)chromedrivers[0];
            for(int i = 0; i < directoryList.Count; i++)
            {
                chromeDriver.Navigate().GoToUrl(directoryList[i]);
                //search(ref chromeDriver, directoryList[i]);
                crawl(ref chromeDriver, directoryList[i], Convert.ToInt32(SL_Craft.Text), saveDir);
            }
        }
    }
}
