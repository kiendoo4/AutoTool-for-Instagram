﻿using Microsoft.Win32;
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
                                IWebElement textarea = chromeDriver.FindElement(By.CssSelector("textarea[aria-label=\"Add a comment…\"]"));
                                textarea.SendKeys(commentList[randomNumber]);
                                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(text(),'Post')]")));
                                var button = chromeDriver.FindElement(By.XPath("//div[contains(text(),'Post')]"));
                                button.Click();
                                //Thread.Sleep(1000);
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
