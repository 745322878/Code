using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation_Cortana.DAL
{
    public class DAL
    {
        public static async  void GetMusicPath()
        {
            var local = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Music");
            if (local != null)
            {
                var list = await local.GetFilesAsync();
                foreach (var item in list)
                {
                    App.musicPath.Add(item.Path );
                    App.musicName.Add(item.DisplayName);
                }
            }
        }
         
    }
}
