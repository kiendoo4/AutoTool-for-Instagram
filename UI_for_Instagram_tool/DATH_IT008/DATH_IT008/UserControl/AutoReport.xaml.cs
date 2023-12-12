using Microsoft.Win32;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.LinkLabel;

namespace DATH_IT008.UserControl
{
    /// <summary>
    /// Interaction logic for AutoReport.xaml
    /// </summary>
    public partial class AutoReport
    {
        string userChoice = null;
        List<ChromeDriver> chromedrivers = new List<ChromeDriver>();
        List<string> directoryUserList = new List<string>();
        public AutoReport()
        {
            InitializeComponent();
            LabelToShow.Content = "Report user";
            Label2ToShow.Content = "chứa các username";
        }
        public AutoReport(MainWindow mainWindow)
        {
            InitializeComponent();
            chromedrivers = mainWindow.chromedrivers;
            Cb_choose.SelectedIndex = 0;
            LabelToShow.Content = "Report user";
            Label2ToShow.Content = "chứa các username";
        }
        private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userChoice = ((ComboBoxItem)Cb_choose.SelectedItem).Content.ToString();
            if (userChoice == "Tùy chọn 1")
            {
                LabelToShow.Content = "Report user trên Insta";
                Label1op1.Content = "Nhập tên tài khoản user bạn muốn report";
                Label2ToShow.Content = "chứa các username";

            }
            else
            {
                LabelToShow.Content = "Report bài viết trên Insta";
                Label1op1.Content = "Nhập đường dẫn bài viết bạn muốn report";
                Label2ToShow.Content = "chứa các bài viết";

            }
        }
        private void ChooseDirectoryUserClick(object sender, RoutedEventArgs e)
        {
            directoryUserList = new List<string>();
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
                    if(DirectoryUser.Text == "")
                        DirectoryUser.Text = fileContent[i];
                    else
                        DirectoryUser.Text += '\n' + fileContent[i];
                }
                userChoice = ((ComboBoxItem)Cb_choose.SelectedItem).Content.ToString();
                if (userChoice == "Tùy chọn 1")
                    MessageBox.Show("Đã cập nhật danh sách các user", "Thông báo");
                else
                {
                    MessageBox.Show("Đã cập nhật danh sách các link bài viết", "Thông báo");
                }    
            }
            else
            {
                MessageBox.Show("Hủy thực thi", "Thông báo");
            }
        }
        private void FinishClick(object sender, RoutedEventArgs e)
        {
            if (DirectoryUser.Text == "")
            {
                MessageBox.Show("Vui lòng nhập username!", "Thông báo");
            }
            else
            {
                foreach (var item in DirectoryUser.Text.Split('\n'))
                {
                    directoryUserList.Add(item);
                }    
                userChoice = ((ComboBoxItem)Cb_choose.SelectedItem).Content.ToString();
                if (userChoice == "Tùy chọn 1")
                {
                    for (int i = 0; i < chromedrivers.Count; i++)
                    {
                        for (int j = 0; j < directoryUserList.Count; j++)
                        {
                            chromedrivers[i].Navigate().GoToUrl($"https://www.instagram.com/{directoryUserList[j]}/");

                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
                            var options = chromedrivers[i].FindElement(By.XPath("//div[@class='x1i10hfl x6umtig x1b1mbwd xaqea5y xav7gou x9f619 xe8uvvx xdj266r x11i5rnm xat24cr x1mh8g0r x16tdsg8 x1hl2dhg xggy1nq x1a2a7pz x6s0dn4 xjbqb8w x1ejq31n xd10rxx x1sy0etr x17r0tee x1ypdohk x78zum5 xl56j7k x1y1aw1k x1sxyh0 xwib8y2 xurb0ha xcdnw81']//div[@class='x6s0dn4 x78zum5 xdt5ytf xl56j7k']//*[@aria-label='Options']"));
                            options.Click();

                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
                            var report = chromedrivers[i].FindElement(By.XPath("//button[normalize-space()='Report']"));
                            report.Click();

                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
                            var reason = chromedrivers[i].FindElement(By.XPath("//div[normalize-space()='Report Account']"));
                            reason.Click();

                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
                            var pretending = chromedrivers[i].FindElement(By.XPath("//body"));
                            pretending.Click();

                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
                            var choose = chromedrivers[i].FindElement(By.XPath("//div[@class='_abn2'][normalize-space()='Me']"));
                            choose.Click();

                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
                            var submit = chromedrivers[i].FindElement(By.XPath("//button[normalize-space()='Submit report']"));
                            submit.Click();
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < chromedrivers.Count; i++)
                    {
                        for (int j = 0; j < directoryUserList.Count; j++)
                        {
                            chromedrivers[i].Navigate().GoToUrl($"{directoryUserList[j]}");
                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
                            var option = chromedrivers[i].FindElement(By.XPath("//div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1n2onr6 x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x6s0dn4 x1oa3qoh xl56j7k']//*[name()='svg']"));
                            option.Click();
                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
                            var report = chromedrivers[i].FindElement(By.XPath("//button[normalize-space()='Report']"));
                            report.Click();
                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
                            var spam = chromedrivers[i].FindElement(By.XPath("//div[@class='x1n2onr6 xzkaem6']//button[1]"));
                            spam.Click();
                        }
                    }
                }
            } 
        }
    }
}
