using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace WallpaperChanger
{
    class Update : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            
            Wallpaper.GetWallpaper();
            Wallpaper.Update();
        }
    }
}
