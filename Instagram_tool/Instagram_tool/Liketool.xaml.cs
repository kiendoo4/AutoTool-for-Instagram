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
using System.Windows.Shapes;

namespace Instagram_tool
{
    /// <summary>
    /// Interaction logic for Liketool.xaml
    /// </summary>
    public partial class Liketool : Window
    {
        Abilities ab = new Abilities();
        ChromeDriver driver;
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
        public Liketool()
        {
            InitializeComponent();
        }

        private HashSet<string> getPosts()
        {
            HashSet<string> posts = new HashSet<string>();
            if (tbx_posts.Text != "")
            {
                posts = new HashSet<string>(tbx_posts.Text.Split("\r\n"));
            }
            else
            {
                // use dummy posts
                posts = new HashSet<string>{
                    "https://www.instagram.com/p/Czs308Pv-Yc/",
                    "https://www.instagram.com/p/CzeG_YgvSj6",
                    "https://www.instagram.com/p/CzaR2gQPUxc/"
                };
            }
            return posts;
        }
        
        

        // Like button
        private void btn_like_Click(object sender, RoutedEventArgs e)
        {
            LoadProfile();
            HashSet<string> posts = getPosts();
            ab.like_unlike(driver, posts, "Like");
        }

        // Unlike button
        private void btn_unlike_Click(object sender, RoutedEventArgs e)
        {
            LoadProfile();
            HashSet<string> posts = getPosts();
            ab.like_unlike(driver, posts, "Unlike");
        }
    }
}
