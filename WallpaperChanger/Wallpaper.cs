using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace WallpaperChanger
{
     static class Wallpaper
    {
        const string path = @"images";
        static DirectoryInfo directory = new DirectoryInfo(path);
        static string _imagePath;
        static FileInfo _currentImage;
        const int SetDesktopWallpaper = 20;
        const int UpdateIniFile = 0x01;
        const int SendWinInichange = 0x02;
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

       

        public static void Set()
        {
            
            var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);

            key.SetValue(@"WallpaperStyle", 10.ToString());
            key.SetValue(@"TileWallpaper", 0.ToString()); 
        
            SystemParametersInfo(SetDesktopWallpaper,0, _imagePath, UpdateIniFile | SendWinInichange);
        }

        public static void Update()
        {
            
             
            _imagePath = directory.FullName+@"\"+ _currentImage;
            Set();
            
        }

        public static void GetWallpaper()
        {
            _currentImage = (from img in directory.GetFiles() orderby img.LastWriteTime descending select img).First();
            var url = "http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US";
            var jsonString = new WebClient().DownloadString(url);
            var json = JObject.Parse(jsonString);
            var imgUrl = json["images"][0]["url"].ToString();
            var startDate = json["images"][0]["startdate"].ToString();

          
            using (var client = new WebClient())
            {
                client.DownloadFile("http://bing.com" + imgUrl,directory.FullName + @"\image" + startDate + ".jpg");
            }

        }
    }
}

