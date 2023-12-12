using OpenQA.Selenium.Chrome;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net.Http;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.IO;
using System.Net;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Configuration;
namespace DATH_IT008.UserControl
{
    /// <summary>
    /// Interaction logic for AutoPost.xaml
    /// </summary>
    
    public partial class AutoPost
    {
        public List<ChromeDriver> chromedrivers = new List<ChromeDriver>();
        List<string> selectedImagePaths = new List<string>();
        List<string> acc_username = new List<string>();
        List<string> acc_password = new List<string>();
        public AutoPost()
        {
            InitializeComponent();
            //Cb_choose.SelectedIndex = 0;
            //LabelToShow.Content = "Đăng một bài duy nhất";
        }
        private void FinishClick(object sender, RoutedEventArgs e)
        {

        }

        private void ChooseAccountClick(object sender, RoutedEventArgs e)
        {
            acc_username = new List<string>();
            acc_password = new List<string>();
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

        private void ChooseDirectoryClick(object sender, RoutedEventArgs e)
        {
            selectedImagePaths = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePaths = openFileDialog.FileNames.ToList();
            }
            MessageBox.Show("Đã chọn xong", "Thông báo");
            MessageBox.Show(selectedImagePaths[0]);
        }

        private void MoreOptionClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
