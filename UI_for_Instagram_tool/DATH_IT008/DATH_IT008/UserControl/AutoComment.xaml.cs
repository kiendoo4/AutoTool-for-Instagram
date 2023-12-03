using DATH_IT008.OtherWindows;
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

namespace DATH_IT008.UserControl
{
    /// <summary>
    /// Interaction logic for AutoComment.xaml
    /// </summary>
    public partial class AutoComment
    {
        List<ChromeDriver> chromedrivers = new List<ChromeDriver>(); 
        List<string> commentList = new List<string>();
        List<string> directoryList = new List<string>();
        public AutoComment()
        {
            InitializeComponent();
        }
        public AutoComment(MainWindow mainWindow)
        {
            InitializeComponent();
            chromedrivers = mainWindow.chromedrivers;
        }
        private void FinishClick(object sender, RoutedEventArgs e)
        {

        }

        private void ChooseCommentList(object sender, RoutedEventArgs e)
        {

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
                //// Get the selected file path
                //string textFilePath = openFileDialog.FileName;

                //// Read the content of the text file
                //string[] fileContent = System.IO.File.ReadAllLines(textFilePath);
                //for (int i = 0; i < fileContent.Length; i++)
                //{

                //    foreach (ChromeDriver chromeDriver in chromedrivers)
                //    {
                //        chromeDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
                //        try
                //        {
                //            Thread.Sleep(5000);
                //            //Find element
                //            IList<IWebElement> elements = chromeDriver.FindElements(By.CssSelector("textarea[class='x1i0vuye xvbhtw8 x1ejq31n xd10rxx x1sy0etr x17r0tee x5n08af x78zum5 x1iyjqo2 x1qlqyl8 x1d6elog xlk1fp6 x1a2a7pz xexx8yu x4uap5 x18d9i69 xkhd6sd xtt52l0 xnalus7 xs3hnx8 x1bq4at4 xaqnwrm']")).Take(5).ToList();
                //            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                //            if (elements.Count > 0)
                //            {
                //                foreach (var element in elements)
                //                {
                //                    element.SendKeys("beautiful");
                //                    Thread.Sleep(3000);
                //                    IWebElement button = chromeDriver.FindElement(By.CssSelector("div[class='x1i10hfl xjqpnuy xa49m3k xqeqjp1 x2hbi6w xdl72j9 x2lah0s xe8uvvx xdj266r x11i5rnm xat24cr x1mh8g0r x2lwn1j xeuugli x1hl2dhg xggy1nq x1ja2u2z x1t137rt x1q0g3np x1lku1pv x1a2a7pz x6s0dn4 xjyslct x1ejq31n xd10rxx x1sy0etr x17r0tee x9f619 x1ypdohk x1f6kntn xwhw2v2 xl56j7k x17ydfre x2b8uid xlyipyv x87ps6o x14atkfc xcdnw81 x1i0vuye xjbqb8w xm3z3ea x1x8b98j x131883w x16mih1h x972fbf xcfux6l x1qhh985 xm0m39n xt0psk2 xt7dq6l xexx8yu x4uap5 x18d9i69 xkhd6sd x1n2onr6 x1n5bzlp x173jzuc x1yc6y37']"));
                //                    Thread.Sleep(3000);
                //                    button.Click();
                //                    Thread.Sleep(3000);
                //                }
                //            }
                //        }
                //        finally
                //        {
                //            Thread.Sleep(10000);
                //            chromeDriver.Quit();
                //        }
                //    }
                //}
                string textFilePath = openFileDialog.FileName;
                string[] fileContent = System.IO.File.ReadAllLines(textFilePath);
                for (int i = 0; i < fileContent.Length; i++)
                {
                    directoryList.Add(fileContent[i]);
                }
                MessageBox.Show(directoryList[0]);
            }
            else
            {
                MessageBox.Show("Hủy thực thi", "Thông báo");
            }
        }
    }
}
