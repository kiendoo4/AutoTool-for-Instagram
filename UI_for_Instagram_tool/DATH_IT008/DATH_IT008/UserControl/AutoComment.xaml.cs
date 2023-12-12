using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
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
using OpenQA.Selenium.Support;
using SeleniumExtras.WaitHelpers;
using System.Net.Http;
using Microsoft.Office.Interop.Excel;
using System.Net;

namespace DATH_IT008.UserControl
{
    /// <summary>
    /// Interaction logic for AutoComment.xaml
    /// </summary>
    public partial class AutoComment
    {
        string userChoice = null;
        List<ChromeDriver> chromedrivers = new List<ChromeDriver>(); 
        List<string> commentList = new List<string>();
        List<string> directoryList = new List<string>();
        public AutoComment()
        {
            InitializeComponent();
            Cb_choose.SelectedIndex = 0;
        }
        public AutoComment(MainWindow mainWindow)
        {
            InitializeComponent();
            chromedrivers = mainWindow.chromedrivers;
            Cb_choose.SelectedIndex = 0;
            SLUser.Text = "1";
            label2toshow.Content = "chứa các username.";
        }
        private void FinishClick(object sender, RoutedEventArgs e)
        {
            if (Cb_choose.Text == "cho bài viết")
            {
                if (Comment.Text == "" || Directory.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ đường dẫn và comment", "Thông báo");
                }
                else
                {
                    commentList = new List<string>();
                    directoryList = new List<string>();
                    foreach (var item in Comment.Text.Split('\n'))
                    {
                        commentList.Add(item);
                    }
                    foreach (var item in Directory.Text.Split('\n'))
                    {
                        directoryList.Add(item);
                    }
                    Random random = new Random();
                    foreach (ChromeDriver chromeDriver in chromedrivers)
                    {
                        WebDriverWait wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));
                        for (int i = 0; i < directoryList.Count; i++)
                        {
                            chromeDriver.Navigate().GoToUrl(directoryList[i]);

                            // Wait until a condition is true
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("textarea[aria-label=\"Add a comment…\"]")));
                            try
                            {
                                try
                                {
                                    int randomNumber = random.Next(0, commentList.Count);
                                    //IWebElement textarea = chromeDriver.FindElement(By.CssSelector("textarea[aria-label=\"Add a comment…\"]"));
                                    //IWebElement textarea = chromeDriver.FindElement(By.TagName("textarea"));
                                    ////textarea.SendKeys();
                                    //textarea.SendKeys(commentList[randomNumber]);
                                    //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(text(),'Post')]")));
                                    //var button = chromeDriver.FindElement(By.XPath("//div[contains(text(),'Post')]"));
                                    //button.Click();
                                    //Thread.Sleep(1000);
                                    chromeDriver.Navigate().GoToUrl(directoryList[i]);
                                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("textarea[aria-label=\"Add a comment…\"]")));
                                    var textarea = chromeDriver.FindElement(By.XPath("//textarea[@placeholder='Add a comment…']"));
                                    try
                                    {
                                        textarea.SendKeys(commentList[randomNumber]);
                                    }
                                    catch
                                    {
                                        textarea = chromeDriver.FindElement(By.XPath("//textarea[@placeholder='Add a comment…']"));
                                        textarea.SendKeys(commentList[randomNumber]);
                                    }
                                    var button = chromeDriver.FindElement(By.XPath("//div[contains(text(),'Post')]"));
                                    button.Click();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            finally
                            {

                            }
                        }
                    }
                }
            }
            else
            {
                if (Comment.Text == "" || Directory.Text == "" || SLUser.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ đường dẫn, comment và số lượng comment", "Thông báo");
                }
                else
                {
                    commentList = new List<string>();
                    directoryList = new List<string>();
                    foreach (var item in Comment.Text.Split('\n'))
                    {
                        commentList.Add(item);
                    }
                    foreach (var item in Directory.Text.Split('\n'))
                    {
                        directoryList.Add(item);
                    }
                    Random random = new Random();
                    foreach (ChromeDriver chromeDriver in chromedrivers)
                    {
                        WebDriverWait wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));
                        for (int i = 0; i < directoryList.Count; i++)
                        {
                            //MessageBox.Show(Convert.ToString(directoryList.Count));
                            chromeDriver.Navigate().GoToUrl($"https://www.instagram.com/{directoryList[i]}");
                            IWebElement check = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("article")));
                            Thread.Sleep(1000);
                            //Lấy tên target
                            IWebElement post = chromeDriver.FindElement(By.TagName("main"));
                            IWebElement targetName = post.FindElement(By.XPath(".//div/header/section/div[1]/a/h2"));
                            string target = targetName.GetAttribute("innerText");
                            post = post.FindElement(By.XPath(".//div/header/section/ul/li[1]/span/span"));
                            string rawNum = post.GetAttribute("innerText");
                            rawNum = new string(rawNum.Where(char.IsDigit).ToArray());
                            int maxQuantity = Convert.ToInt32(rawNum);
                            HashSet<string> urlBag = new HashSet<string>();
                            try
                            {
                                IJavaScriptExecutor js = (IJavaScriptExecutor)chromeDriver;

                                while (true)
                                {
                                    // Cuộn xuống cuối trang
                                    js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                                    // Chờ một khoảng thời gian ngắn để trang web tải dữ liệu mới
                                    System.Threading.Thread.Sleep(1000);
                                    //lay cac element chua anh--------------------------------------------------
                                    IWebElement article = chromeDriver.FindElement(By.TagName("article"));
                                    Thread.Sleep(50);
                                    List<IWebElement> href = article.FindElements(By.XPath(".//div[@class='_ac7v  _al3n']/div[@class='_aabd _aa8k  _al3l']/a")).ToList();
                                    for (int k = 0; k < href.Count; k++)
                                    {
                                        string postUrl = href[k].GetAttribute("href");
                                        urlBag.Add(postUrl);
                                        Thread.Sleep(50);
                                    }
                                    if (urlBag.Count > Convert.ToInt32(SLUser.Text)) break;
                                    // Kiểm tra xem đã đến cuối trang hay chưa
                                    if (urlBag.Count >= maxQuantity) break;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi:" + ex.Message, "Thông báo");
                            }
                            try
                            {
                                int count = 1;
                                foreach (string url in urlBag)
                                {
                                    IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)chromeDriver;
                                    jsExecutor.ExecuteScript($"window.open('{url}', '_blank');");
                                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[1]);
                                    //Chờ element chứa bài viết đã load xong
                                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("textarea[aria-label=\"Add a comment…\"]")));
                                    Thread.Sleep(50);
                                    try
                                    {
                                        try
                                        {
                                            int randomNumber = random.Next(0, commentList.Count);
                                            var textarea = chromeDriver.FindElement(By.CssSelector("textarea[aria-label=\"Add a comment…\"]"));
                                            try
                                            {
                                                textarea.SendKeys(commentList[randomNumber]);
                                            }
                                            catch
                                            {
                                                textarea = chromeDriver.FindElement(By.XPath("//textarea[@placeholder='Add a comment…']"));
                                                textarea.SendKeys(commentList[randomNumber]);
                                            }
                                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(text(),'Post')]")));
                                            var button = chromeDriver.FindElement(By.XPath("//div[contains(text(),'Post')]"));
                                            button.Click();

                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                            chromeDriver.Close();

                                            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);
                                        }
                                    }
                                    finally
                                    {

                                    }
                                    Thread.Sleep(50);
                                    chromeDriver.Close();
                                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);

                                    count++;
                                    if (count > Convert.ToInt32(SLUser.Text)) break;

                                    Thread.Sleep(50);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }    
        }

        private void ChooseCommentList(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt|All Files|*.*", // Filter for text files
                Title = "Chọn file txt"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                string textFilePath = openFileDialog.FileName;
                string[] fileContent = System.IO.File.ReadAllLines(textFilePath);
                for (int i = 0; i < fileContent.Length; i++)
                {
                    if (Comment.Text == "")
                        Comment.Text = fileContent[i];
                    else
                        Comment.Text = Comment.Text + '\n' + fileContent[i]; 
                }
                MessageBox.Show("Đã cập nhật danh sách comment", "Thông báo");
            }
            else
            {
                MessageBox.Show("Hủy thực thi", "Thông báo");
            }
        }
        private void ChooseDirectiryClick(object sender, RoutedEventArgs e)
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
                string[] fileContent = System.IO.File.ReadAllLines(textFilePath);
                for (int i = 0; i < fileContent.Length; i++)
                {
                    if (Directory.Text == "")
                        Directory.Text = fileContent[i];
                    else
                        Directory.Text = Directory.Text + '\n' + fileContent[i];
                }
                MessageBox.Show("Đã cập nhật danh sách đường dẫn", "Thông báo");
            }
            else
            {
                MessageBox.Show("Hủy thực thi", "Thông báo");
            }
        }

        private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userChoice = ((ComboBoxItem)Cb_choose.SelectedItem).Content.ToString();
            if (userChoice == "cho user")
            {
                Label1op1.Content = "Nhập các username để tự động bình luận";
                label2toshow.Content = "chứa các username.";
                LabelSLUser.Visibility = Visibility.Visible;
                SLUser.Visibility = Visibility.Visible;
            }
            else
            {
                Label1op1.Content = "Nhập các url bài viết để tự động bình luận";
                label2toshow.Content = "chứa các url bài viết.";
                LabelSLUser.Visibility = Visibility.Hidden;
                SLUser.Visibility = Visibility.Hidden;
            }
        }
    }
}
