using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace FilmManagerment
{
    public class Film
    {

        public string Name { get; set; }
        public string Actor { get; set; }
        public string Dictor { get; set; }
        public string Type { get; set; }
        public string Score { get; set; }
        public string Introduction { get; set; }
        public string PhotoSource { get; set; }

        /// <summary>
        /// 对实体类对象进行序列化的方法
        /// </summary>
        /// <returns></returns>
        public static string ToJsonData(object item)
        {
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            return jsonString;
        }
        /// <summary>
        /// Json字符串反序列化成实体类对象
        /// </summary>
        /// <typeparam name="Film"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static Film DataJsonSerializer(string jsonString)
        {
            //Film film =(Film)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(Film));
            Film film = (Film)ds.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(jsonString)));
            return film;
        }
        public static List<Film> DataSerializer(string jsonstring)
        { 
            int startpoint=0;
            int endpoint=0;
            int j=0;
            string a="{";
            string b="}";
            string[] str = new string[50];
            Film f ;
            List<Film> film = new List<Film>();
            for(int i=0;i<jsonstring.Length;i++)
            {
                startpoint=jsonstring.IndexOf(a,startpoint+1);
                endpoint=jsonstring.IndexOf(b,endpoint+1);
                if(startpoint==-1&&endpoint==-1)
                    break;
                string s = jsonstring.Substring(startpoint, endpoint - startpoint + 1);
                str[j] = s;
                j++;
            }
            for(int k=0;k<j;k++)
            {
                f= new Film();
                f=Film.DataJsonSerializer(str[k]);
                film.Add(f);
            }
            return film;
        }
        /// <summary>
        /// 获取图片路径
        /// </summary>
        public async void GetPhotoSource()
        {
            StorageFolder storage = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var folder = await storage.GetFolderAsync("FilmPhoto");
            foreach (var file in await folder.GetFilesAsync())
            {
               App.filmPhoto.Add(file.Path);
            }
        }
        /// <summary>
        /// 将jsonstring字符串写入文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async static Task WriteFile(string fileName, string content)
        {
            StorageFolder storage = await ApplicationData.Current.LocalFolder.CreateFolderAsync("FilmInformationFolder", CreationCollisionOption.OpenIfExists);
            StorageFile file = await storage.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
           
            await FileIO.WriteTextAsync(file, content);
        }
        /// <summary>
        /// 读取文件内容，返回字符串
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<string> ReadFile(string fileName)
        {
            string text;
            try
            {
                StorageFolder storage = await ApplicationData.Current.LocalFolder.CreateFolderAsync("FilmInformationFolder",CreationCollisionOption.OpenIfExists);
                StorageFile file = await storage.GetFileAsync(fileName);
                
                IRandomAccessStream accessStream = await file.OpenReadAsync();
                using (StreamReader streamReader = new StreamReader(accessStream.AsStreamForRead((int)accessStream.Size)))
                {
                    text = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                text = "文件读取错误" + e.Message;
            }
            return text;
        }
    }
}
