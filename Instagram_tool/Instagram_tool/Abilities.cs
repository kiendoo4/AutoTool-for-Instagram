using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V117.Console;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Instagram_tool
{
    internal class Abilities
    {
        private int SLEEP_TIME = 5000; // Sleep time for Thread.Sleep

        // Like + Unlike function
        public void like_unlike(ChromeDriver driver, HashSet<string> posts, string option)
        {
            foreach (var post in posts)
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
            Thread.Sleep(SLEEP_TIME); // Check if the function includes this function works or not (for testing)
            driver.Quit();
        }

        // Post function
        public void post(ChromeDriver driver, string upload_path, string s, string caption, bool hidelike, bool turnoffcmt, bool rndcap)
        {
            driver.Navigate().GoToUrl("https://www.instagram.com");

            try
            {
                //turn off pop up
                var i = driver.FindElement(By.XPath("//button[normalize-space()='Not Now']"));
                i.Click();
            }
            catch
            {
                //nothing   
            }
            // Wait
            Thread.Sleep(500);
            // Click create + select files from computer buttons
            var temp = driver.FindElement(By.XPath("//*[@aria-label='New post']"));
            temp.Click();
            temp = driver.FindElement(By.XPath("//button[normalize-space()='Select from computer']"));
            temp.Click();

            // Select all files in upload_path directory and upload (using AutoIt)
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "D:\\vs\\Instagram_tool\\Instagram_tool\\autoit_exe\\Select_folder.exe"; // change this to the same exe file if you use this function
            startInfo.Arguments = upload_path;
            process.StartInfo = startInfo;
            process.Start();
            Thread.Sleep(2000);
            process.Close();
            startInfo.FileName = "D:\\vs\\Instagram_tool\\Instagram_tool\\autoit_exe\\Upload_all_files.exe"; // change this to the same exe file if you use this function
            startInfo.Arguments = s;
            process.StartInfo = startInfo;
            process.Start();
            Thread.Sleep(2000);
            process.Close();

            // Click next button
            var webbtn_next = driver.FindElement(By.XPath("//div[contains(text(),'Next')]"));
            webbtn_next.Click();
            Thread.Sleep(500);
            webbtn_next = driver.FindElement(By.XPath("//div[contains(text(),'Next')]"));
            webbtn_next.Click();


            // Create new post
            if (rndcap) // check if random_caption is checked or not
            {
                // use random caption in the file caption.txt
                string filePath = "D:\\vs\\Instagram_tool\\Instagram_tool\\txt_files\\captions.txt"; // change this to the same exe file if you use this function
                string content;
                using (StreamReader reader = new StreamReader(filePath))
                {
                    content = reader.ReadToEnd();
                }
                string[] captions = content.Split("\r\n");
                Random random = new Random();
                caption = captions[random.Next(0, captions.Length)];
            }
            temp = driver.FindElement(By.XPath("//div[@aria-label='Write a caption...']"));
            temp.SendKeys(caption); // Caption
            temp = driver.FindElement(By.XPath("//div[4]//div[1]//span[2]//*[@aria-label='Down chevron icon']"));
            temp.Click();
            temp = driver.FindElement(By.XPath("//div[5]//div[1]//span[2]//*[@aria-label='Down chevron icon']"));
            temp.Click();
            if (hidelike) // check if hide_like is checked or not
            {
                // turn on hide-likes-and-views feature
                temp = driver.FindElement(By.XPath("//div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1e56ztr x1n2onr6 x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']//input[@role='switch']"));
                temp.Click();
            }
            if (turnoffcmt) // check if turn_off_cmt is checked or not
            {
                // turn on turn-off-comment feature
                temp = driver.FindElement(By.XPath("//body/div[@class='x1n2onr6 xzkaem6']/div[@class='x9f619 x1n2onr6 x1ja2u2z']/div[@class='x78zum5 xdt5ytf xg6iff7 x1n2onr6 xwnxf6m']/div[@class='x1uvtmcs x4k7w5x x1h91t0o x1beo9mf xaigb6o x12ejxvf x3igimt xarpa2k xedcshv x1lytzrv x1t2pt76 x7ja8zs x1n2onr6 x1qrby5j x1jfb8zj']/div[@class='x1qjc9v5 x9f619 x78zum5 xdt5ytf x1iyjqo2 xl56j7k']/div[@class='x1cy8zhl x9f619 x78zum5 xl56j7k x2lwn1j xeuugli x47corl']/div[@aria-label='Create new post']/div[@class='xs83m0k x1sy10c2 x1h5jrl4 xieb3on xmn8rco x1iy3rx x1n2onr6']/div[@role='dialog']/div/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1n2onr6 x6ikm8r x10wlt62 x1iyjqo2 x2lwn1j xeuugli xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']/div[@class='x15wfb8v x3aagtl x6ql1ns x78zum5 xdl72j9 x1iyjqo2 xs83m0k x13vbajr x1ue5u6n']/div[@class='xhk4uv x26u7qi xy80clv x9f619 x78zum5 x1n2onr6 x1f4304s']/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1n2onr6 x1plvlek xryxfnj x1iyjqo2 x2lwn1j xeuugli xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']/div[@class='xmz0i5r xh8midk x1rife3k']/div/div[@class='_ac2p']/div[@class='_abm8']/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1pi30zi x1swvt13 xjkvuk6 x1iorvi4 x1n2onr6 x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x12nagc x1n2onr6 x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x6s0dn4 x1oa3qoh x1nhvcw1']/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1n2onr6 x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1n2onr6 x1plvlek xryxfnj x1c4vz4f x2lah0s x1q0g3np xqjyukv x6s0dn4 x1oa3qoh x1qughib']/div[@class='x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1uhb9sk x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x1qjc9v5 x1oa3qoh x1nhvcw1']/div[@class='x1ja2u2z x1a2a7pz x1bhb7k3 xfh8nwu xoqspk4 x12v9rci x138vmkv x972fbf xcfux6l x1qhh985 xm0m39n x9f619 x1rg5ohu x1hc1fzr x6ikm8r x10wlt62 xexx8yu x4uap5 x18d9i69 xkhd6sd x1n2onr6 x1eub6wo x19991ni x1d72o xxk0z11 x100vrsf']/input[1]"));
                temp.Click();
            }

            // Click upload post
            temp = driver.FindElement(By.XPath("//div[contains(text(),'Share')]"));
            temp.Click();

            // Wait for uploading
            WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[@class='x1lliihq x1plvlek xryxfnj x1n2onr6 x193iq5w xeuugli x1fj9vlw x13faqbe x1vvkbs x1s928wv xhkezso x1gmr53x x1cpjm7i x1fgarty x1943h6x x1i0vuye x1ms8i2q xo1l8bm x5n08af x2b8uid x4zkp8e xw06pyt x10wh9bi x1wdrske x8viiok x18hxmgj']")));

            // Close "create post"
            temp = driver.FindElement(By.XPath("//div[@class='x160vmok x10l6tqk x1eu8d0j x1vjfegm']//div[@class='x6s0dn4 x78zum5 xdt5ytf xl56j7k']//*[@aria-label='Close']"));
            temp.Click();

            Thread.Sleep(SLEEP_TIME); // Check if the function includes this function works or not (for testing)
            
        }
    }
}
