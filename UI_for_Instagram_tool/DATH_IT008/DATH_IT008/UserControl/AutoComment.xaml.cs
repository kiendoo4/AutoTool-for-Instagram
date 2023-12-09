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
        }
        private void FinishClick(object sender, RoutedEventArgs e)
        {
            if(Comment.Text == ""|| Directory.Text=="")
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
                for (int i = 0; i < directoryList.Count; i++)
                {
                    foreach (ChromeDriver chromeDriver in chromedrivers)
                    {
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri(directoryList[i]);
                        chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);
                        chromeDriver.Navigate().GoToUrl(client.BaseAddress);
                        WebDriverWait wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));

                        // Wait until a condition is true
                        wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                        try
                        {
                            try
                            {
                                var element = chromeDriver.FindElement(By.CssSelector("textarea[class='x1i0vuye xvbhtw8 x1ejq31n xd10rxx x1sy0etr x17r0tee x5n08af x78zum5 x1iyjqo2 x1qlqyl8 x1d6elog xlk1fp6 x1a2a7pz xexx8yu x4uap5 x18d9i69 xkhd6sd xtt52l0 xnalus7 xs3hnx8 x1bq4at4 xaqnwrm']"));
                                int randomNumber = random.Next(0, commentList.Count);
                                element.SendKeys(commentList[randomNumber]);
                                //.Until(ExpectedConditions.TextToBePresentInElementValue(element, commentList[randomNumber]));
                                var button = chromeDriver.FindElement(By.CssSelector("div[class='x1i10hfl xjqpnuy xa49m3k xqeqjp1 x2hbi6w xdl72j9 x2lah0s xe8uvvx xdj266r x11i5rnm xat24cr x1mh8g0r x2lwn1j xeuugli x1hl2dhg xggy1nq x1ja2u2z x1t137rt x1q0g3np x1lku1pv x1a2a7pz x6s0dn4 xjyslct x1ejq31n xd10rxx x1sy0etr x17r0tee x9f619 x1ypdohk x1f6kntn xwhw2v2 xl56j7k x17ydfre x2b8uid xlyipyv x87ps6o x14atkfc xcdnw81 x1i0vuye xjbqb8w xm3z3ea x1x8b98j x131883w x16mih1h x972fbf xcfux6l x1qhh985 xm0m39n xt0psk2 xt7dq6l xexx8yu x4uap5 x18d9i69 xkhd6sd x1n2onr6 x1n5bzlp x173jzuc x1yc6y37']"));
                                button.Click();
                            }
                            catch
                            {
                                continue;
                            }
                        }
                        finally
                        {
                            
                            //chromeDriver.Quit();
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
                LabelSLUser.Visibility = Visibility.Visible;
                SLUser.Visibility = Visibility.Visible;
            }
            else
            {
                Label1op1.Content = "Nhập các url bài viết để tự động bình luận";
                LabelSLUser.Visibility = Visibility.Hidden;
                SLUser.Visibility = Visibility.Hidden;
            }
        }
    }
}
