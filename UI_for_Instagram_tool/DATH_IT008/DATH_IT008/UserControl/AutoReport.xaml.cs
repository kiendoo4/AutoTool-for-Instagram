﻿using OpenQA.Selenium.Chrome;
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
        }
        public AutoReport(MainWindow mainWindow)
        {
            InitializeComponent();
            chromedrivers = mainWindow.chromedrivers;
            Cb_choose.SelectedIndex = 0;
            LabelToShow.Content = "Report user";
        }
        private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userChoice = ((ComboBoxItem)Cb_choose.SelectedItem).Content.ToString();
            if (userChoice == "Tùy chọn 1")
            {
                LabelToShow.Content = "Follow user trên Insta";
                Label1op1.Content = "Nhập tên tài khoản user bạn muốn follow";
            }
            else
            {
                LabelToShow.Content = "Unfollow user trên Insta";
                Label1op1.Content = "Nhập tên tài khoản user bạn muốn unfollow";
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
                    directoryUserList.Add(fileContent[i]);
                }
                MessageBox.Show("Đã cập nhật danh sách các user", "Thông báo");
            }
            else
            {
                MessageBox.Show("Hủy thực thi", "Thông báo");
            }
        }
        private void FinishClick(object sender, RoutedEventArgs e)
        {
            if (userChoice == null)
            {
                MessageBox.Show("Vui lòng chọn follow hoặc unfollow", "Thông báo");
            }
            else
            {
                userChoice = ((ComboBoxItem)Cb_choose.SelectedItem).Content.ToString();
                if (userChoice == "Tùy chọn 1")
                {
                    for (int i = 0; i < chromedrivers.Count; i++)
                    {
                        for (int j = 0; j < directoryUserList.Count; j++)
                        {
                            chromedrivers[i].Navigate().GoToUrl($"https://www.instagram.com/{directoryUserList[j]}/");
                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
                            try
                            {
                                var button = chromedrivers[i].FindElement(By.CssSelector("button[class=' _acan _acap _acas _aj1- _ap30']"));
                                if (button != null)
                                {
                                    button.Click();
                                }
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < chromedrivers.Count; i++)
                    {
                        for (int j = 0; j < directoryUserList.Count; j++)
                        {
                            chromedrivers[i].Navigate().GoToUrl($"https://www.instagram.com/{directoryUserList[j]}/");
                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(3, 8)));
                            var a = chromedrivers[i].FindElement(By.XPath("//button[contains(@class,'_acan _acap _acat _aj1- _ap30')]"));
                            try
                            {
                                if (a != null)
                                {
                                    a.Click();
                                    Thread.Sleep(3000);
                                    a = chromedrivers[i].FindElement(By.XPath("//span[contains(text(),'Unfollow')]"));
                                    a.Click();
                                }
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }
    }
}
