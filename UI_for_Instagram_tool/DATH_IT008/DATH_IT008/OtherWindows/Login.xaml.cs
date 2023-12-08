                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Http;
using System.Threading;
using System.Windows.Navigation;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.IO;
using System.Net;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Configuration;

namespace DATH_IT008.OtherWindows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public List<ChromeDriver> chromedrivers = new List<ChromeDriver>();
        List<string> acc_username = new List<string>();
        List<string> acc_password = new List<string>();
        public Login()
        {
            InitializeComponent();
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {

        }

        private void HideClick(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow as Window;
            window.WindowState = WindowState.Minimized;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        bool login(string us, string pa, ChromeDriver driver)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.instagram.com");
            driver.Navigate().GoToUrl(client.BaseAddress);
            var username = driver.FindElement(By.CssSelector("input[name='username']"));
            username.SendKeys(us);
            var password = driver.FindElement(By.CssSelector("input[name='password']"));
            password.SendKeys(pa);
            var button = driver.FindElement(By.CssSelector("button[class=' _acan _acap _acas _aj1- _ap30']"));
            button.Click();
            Thread.Sleep(5000);
            if (driver.Url != "https://www.instagram.com")
            {
                return true;
                //MainWindow mainWindow = new MainWindow(this);
                //mainWindow.Show();
                //this.Close();
            }
            return false;
            
        }
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;
            if (Username.IsEnabled)
            {
                ChromeDriver driver = new ChromeDriver();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                chromedrivers.Add(driver);
                try
                {
                    if(login(username, password, driver))
                    {
                        MainWindow mainWindow = new MainWindow(this);
                        mainWindow.Show();
                        this.Close();
                    }
                }
                catch
                {
                    Window messageBoxWindow = new Window
                    {
                        Title = "Thông báo",
                        Content = "Không thể đăng nhập, vui lòng kiểm tra lại tài khoản, mật khẩu và sau đó thử lại :(",
                        SizeToContent = SizeToContent.WidthAndHeight,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStyle = WindowStyle.ToolWindow,
                        Topmost = true
                    };
                    messageBoxWindow.ShowDialog();
                    driver.Quit();
                }
            }
            else
            {
                
                bool check_open = false;
                for(int i = 0; i < acc_username.Count; i++)
                {
                    username = acc_username[i];
                    password = acc_password[i];
                    ChromeDriver driver2 = new ChromeDriver();
                    chromedrivers.Add(driver2);
                    driver2.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                    try
                    {
                        if(login(username, password, driver2)) check_open = true;
                    }
                    catch
                    {
                        driver2.Quit();
                    }
                }
                if(check_open)
                {
                    MainWindow mainWindow = new MainWindow(this);
                    mainWindow.Show();
                    this.Close();
                }
            }
        }

        private void ChooseAccountClick(object sender, RoutedEventArgs e)
        {
            acc_username = new List<string>();
            acc_password = new List<string>();
            Username.IsEnabled = false;
            Password.IsEnabled = false;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.Title = "Select an Excel File";

            if (openFileDialog.ShowDialog() == true)
            {
                string excelFilePath = openFileDialog.FileName;
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Open(excelFilePath);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                Excel.Range usedRange = worksheet.UsedRange;
                int rowCount = usedRange.Rows.Count;
                int colCount = usedRange.Columns.Count;
                for (int row = 1; row <= rowCount; row++)
                {
                    string column1 = Convert.ToString((usedRange.Cells[row, 1] as Excel.Range).Value2);
                    string column2 = Convert.ToString((usedRange.Cells[row, 2] as Excel.Range).Value2);
                    acc_username.Add(column1);
                    acc_password.Add(column2);
                }

                workbook.Close(false);
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                MessageBox.Show("Đã chọn file, vui lòng bấm vào nút đăng nhập", "Thông báo");
            }
        }

        private void ChoosePasswordClick(object sender, RoutedEventArgs e)
        {
            Username.IsEnabled = false;
            Password.IsEnabled = false;
        }

        private void EnableClick(object sender, RoutedEventArgs e)
        {
            Username.IsEnabled = true;
            Password.IsEnabled = true;
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          