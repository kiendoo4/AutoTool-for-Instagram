using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Instagram_tool
{
    /// <summary>
    /// Interaction logic for FastPost_invidual.xaml
    /// </summary>
    public partial class FastPost_invidual : Page
    {
        /*
         * - Features:
         * + Invidual post + invidual image (video): Đăng bài đơn + caption cụ thể (hoặc random)
         * + Invidual post + multiple images (videos): Đăng bài đơn nhưng bài đó chứa nhiều ảnh hoặc video + caption cụ thể (hoặc random)
         */
        private string path;
        private Abilities ab = new Abilities();
        private ChromeDriver driver;
        // for testing
        private void LoadProfile()
        {
            ChromeOptions options = new ChromeOptions();
            string ProfilePath = "C:\\Users\\Admin\\AppData\\Local\\Google\\Chrome\\User Data\\TestUser"; // change this directory to your chrome profile path if you use this code
            if (!Directory.Exists(ProfilePath))
            {
                Directory.CreateDirectory(ProfilePath);
            }
            options.AddArgument("user-data-dir=" + ProfilePath);
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); // change max wait time (second) here
            driver.Manage().Window.Maximize();
        }
        public FastPost_invidual(string path)
        {
            this.path = path;
            InitializeComponent();
        }

        private void btn_post_Click(object sender, RoutedEventArgs e)
        {
            LoadProfile();
            
            // init components for the post function
            bool hidelike = chk_hidelike.IsChecked ?? false;
            bool turnoffcmt = chk_turnoffcmt.IsChecked ?? false;
            bool rndcap = chk_rndcap.IsChecked ?? false;
            int numfiles, start_pos;
            string[] files;
            string file_s="";

            // init files
            if (tbx_files.Text != "")
            {
                files = tbx_files.Text.Split("\r\n");
                start_pos = 0;
                numfiles = files.Length;
            }
            else
            {
                files = Directory.GetFiles(path);
                if (chk_allfiles.IsChecked ?? false) numfiles = 10;
                else numfiles = int.Parse(tbx_numfiles.Text);
                numfiles = numfiles > 10 ? 10 : numfiles;
                start_pos = int.Parse(tbx_startpos.Text);
            }
            for (int i = start_pos; i < numfiles + start_pos; i++)
            {
                file_s += "\"" + files[i] + "\"";
            }

            // post function
            ab.post(driver, path, file_s, tbx_caption.Text, hidelike, turnoffcmt, rndcap);
        }
    }
}
