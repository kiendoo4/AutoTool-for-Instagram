using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        }
        private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userChoice = ((ComboBoxItem)Cb_choose.SelectedItem).Content.ToString();
            if (userChoice == "Tùy chọn 1")
            {
                LabelToShow.Content = "Thả like";
                Label1op1.Content = "Nhập đường dẫn bài viết bạn muốn like";
            }
            else
            {
                LabelToShow.Content = "Thả unlike";
                Label1op1.Content = "Nhập đường dẫn bài viết bạn muốn unlike";
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
                string textFilePath = openFileDialog.FileName;
                string[] fileContent = System.IO.File.ReadAllLines(textFilePath);
                for (int i = 0; i < fileContent.Length; i++)
                {
                    if(Directory.Text == "")
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
            driver.Quit();
        }
        private void FinishClick(object sender, RoutedEventArgs e)
        {
            if (Directory.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đường dẫn!", "Thông báo");
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
                else
                {
                    for (int i = 0; i < chromedrivers.Count; i++)
                    {
                        like_unlike(chromedrivers[i], directoryList, "Unlike");
                    }
                
                }
            }
        }
    }
}
