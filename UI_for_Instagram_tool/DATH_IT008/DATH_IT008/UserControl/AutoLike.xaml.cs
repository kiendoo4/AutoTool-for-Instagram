using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace DATH_IT008.UserControl
{
    /// <summary>
    /// Interaction logic for AutoLike.xaml
    /// </summary>
    public partial class AutoLike
    {
        string userChoice = null;
        List<ChromeDriver> chromedrivers = new List<ChromeDriver>();
        List<string> directoryList = new List<string>();
        public AutoLike()
        {
            InitializeComponent();
        }
        public AutoLike(MainWindow mainWindow)
        {
            InitializeComponent();
            chromedrivers = mainWindow.chromedrivers;
            Cb_choose.SelectedIndex = 0;
            LabelToShow.Content = "Thả like";
            LabelSLUser.Visibility = Visibility.Hidden;
            SLUser.Visibility = Visibility.Hidden;
        }
        private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userChoice = ((ComboBoxItem)Cb_choose.SelectedItem).Content.ToString();
            if (userChoice == "Tùy chọn 1")
            {
                LabelToShow.Content = "Thả like bài viết bất kì";
                Label1op1.Content = "Nhập đường dẫn bài viết bạn muốn like";
                Label2toshow.Content = "chứa đường dẫn bài viết";
                LabelSLUser.Visibility = Visibility.Hidden;
                SLUser.Visibility = Visibility.Hidden;
                Directory.Text = "";
            }
            else if (userChoice == "Tùy chọn 2")
            {
                LabelToShow.Content = "Thả unlike bài viết bất kì";
                Label1op1.Content = "Nhập đường dẫn bài viết bạn muốn unlike";
                Label2toshow.Content = "chứa đường dẫn bài viết";
                LabelSLUser.Visibility = Visibility.Hidden;
                SLUser.Visibility = Visibility.Hidden;
                Directory.Text = "";
            }
            else if (userChoice == "Tùy chọn 3")
            {
                LabelToShow.Content = "Thả like bài viết của user";
                Label1op1.Content = "Nhập các username bạn muốn like bài viết";
                Label2toshow.Content = "chứa các username";
                LabelSLUser.Visibility = Visibility.Visible;
                SLUser.Visibility = Visibility.Visible;
                Directory.Text = "";
            }
            else
            {
                LabelToShow.Content = "Thả like bài viết của user";
                Label1op1.Content = "Nhập các username bạn muốn unlike";
                Label2toshow.Content = "chứa các username";
                LabelSLUser.Visibility = Visibility.Visible;
                SLUser.Visibility = Visibility.Visible;
                Directory.Text = "";
            }
        }
        private void ChooseDirectoryClick(object sender, RoutedEventArgs e)
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
                if (userChoice == "Tùy chọn 1" || userChoice == "Tùy chọn 2")
                {
                    string textFilePath = openFileDialog.FileName;
                    string[] fileContent = System.IO.File.ReadAllLines(textFilePath);
                    for (int i = 0; i < fileContent.Length; i++)
                    {
                        if (Directory.Text == "")
                        {
                            Directory.Text = fileContent[i];
                        }
                        else
                            Directory.Text += '\n' + fileContent[i];
                    }
                    MessageBox.Show("Đã cập nhật danh sách các đường dẫn bài viết", "Thông báo");
                }
                else
                {
                    string textFilePath = openFileDialog.FileName;
                    string[] fileContent = System.IO.File.ReadAllLines(textFilePath);
                    for (int i = 0; i < fileContent.Length; i++)
                    {
                        if (Directory.Text == "")
                        {
                            Directory.Text = fileContent[i];
                        }
                        else
                            Directory.Text += '\n' + fileContent[i];
                    }
                    MessageBox.Show("Đã cập nhật danh sách các username", "Thông báo");
                }    
            }
            else
            {
                MessageBox.Show("Hủy thực thi", "Thông báo");
            }
        }
        public void like_unlike(ChromeDriver driver, List<string>directoryListhehe, string option)
        {
            foreach (var post in directoryListhehe)
            {
                try
                {
                    driver.Navigate().GoToUrl(post);
                    var btn = driver.FindElement(By.XPath($"//div[@class='x1i10hfl x6umtig x1b1mbwd xaqea5y xav7gou x9f619 xe8uvvx xdj266r x11i5rnm xat24cr x1mh8g0r x16tdsg8 x1hl2dhg xggy1nq x1a2a7pz x6s0dn4 xjbqb8w x1ejq31n xd10rxx x1sy0etr x17r0tee x1ypdohk x78zum5 xl56j7k x1y1aw1k x1sxyh0 xwib8y2 xurb0ha xcdnw81']//span//*[@aria-label='{option}']"));
                    btn.Click();
                }
                catch
                {
                    // skip if the post is already like/unlike (option)
                    continue;
                }
            }
            Thread.Sleep(5000); // Check if the function includes this function works or not (for testing)
            //driver.Quit();
        }
        private void FinishClick(object sender, RoutedEventArgs e)
        {
            if (Directory.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đường dẫn bài viết hoặc username!", "Thông báo");
            }
            else
            {
                foreach (var item in Directory.Text.Split('\n'))
                {
                    directoryList.Add(item);
                }
                userChoice = ((ComboBoxItem)Cb_choose.SelectedItem).Content.ToString();
                if (userChoice == "Tùy chọn 1")
                {
                    for (int i = 0; i < chromedrivers.Count; i++)
                    {
                        like_unlike(chromedrivers[i], directoryList, "Like");
                    }
                }
                else if(userChoice == "Tùy chọn 2")
                {
                    for (int i = 0; i < chromedrivers.Count; i++)
                    {
                        like_unlike(chromedrivers[i], directoryList, "Unlike");
                    }
                
                }
                else if(userChoice == "Tùy chọn 3")
                {
                    if (Directory.Text == "" || SLUser.Text == "")
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ đường dẫn, comment và số lượng comment", "Thông báo");
                    }
                    else
                    {
                        directoryList = new List<string>();
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
                                            string option = "Like";
                                            var btn = chromeDriver.FindElement(By.XPath($"//div[@class='x1i10hfl x6umtig x1b1mbwd xaqea5y xav7gou x9f619 xe8uvvx xdj266r x11i5rnm xat24cr x1mh8g0r x16tdsg8 x1hl2dhg xggy1nq x1a2a7pz x6s0dn4 xjbqb8w x1ejq31n xd10rxx x1sy0etr x17r0tee x1ypdohk x78zum5 xl56j7k x1y1aw1k x1sxyh0 xwib8y2 xurb0ha xcdnw81']//span//*[@aria-label='{option}']"));
                                            btn.Click();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                            chromeDriver.Close();

                                            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);
                                        }
                                        Thread.Sleep(50);
                                        chromeDriver.Close();
                                        chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);

                                        count++;
                                        if(count > maxQuantity) break;
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
                else
                {
                    if (Directory.Text == "" || SLUser.Text == "")
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ đường dẫn, comment và số lượng comment", "Thông báo");
                    }
                    else
                    {
                        directoryList = new List<string>();
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
                                            string option = "Unlike";
                                            var btn = chromeDriver.FindElement(By.XPath($"//div[@class='x1i10hfl x6umtig x1b1mbwd xaqea5y xav7gou x9f619 xe8uvvx xdj266r x11i5rnm xat24cr x1mh8g0r x16tdsg8 x1hl2dhg xggy1nq x1a2a7pz x6s0dn4 xjbqb8w x1ejq31n xd10rxx x1sy0etr x17r0tee x1ypdohk x78zum5 xl56j7k x1y1aw1k x1sxyh0 xwib8y2 xurb0ha xcdnw81']//span//*[@aria-label='{option}']"));
                                            btn.Click();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                            chromeDriver.Close();

                                            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);
                                        }
                                        Thread.Sleep(50);
                                        chromeDriver.Close();
                                        chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);

                                        count++;
                                        if (count > maxQuantity) break;
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
        }
    }
}
