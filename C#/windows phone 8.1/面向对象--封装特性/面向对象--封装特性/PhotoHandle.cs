using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace 面向对象__封装特性
{
    class PhotoHandle
    {
        public List<PhotoHandle> photoName = new List<PhotoHandle>();

        public string photoname { set; get; }

        private int num;
        public PhotoHandle()
        { 

        }
        public   PhotoHandle(int num1)
        {
            this.num = num1;
        }
        public async Task<List<PhotoHandle>> Get_PhotoFile()
        {
            PhotoHandle p ;
            var localFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            foreach (StorageFile file in await localFolder.GetFilesAsync())
            {
                p = new PhotoHandle();
                p.photoname = file.Path;
                photoName.Add(p);
            }
            return photoName;


        }
        public async Task<List<PhotoHandle>> Delete_Photo()
        {
            var localFolder=await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            photoName =await  Get_PhotoFile();
            photoName.RemoveAt(num);
            return photoName;
        }
    }
}
