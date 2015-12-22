using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace FilmManagerment
{
    //帐号与密码类
    public class SignIn
    {
        //管理员默认密码
        public async void First()
        {
            StorageFolder storage = await ApplicationData.Current.LocalFolder.CreateFolderAsync("AdminPassword", CreationCollisionOption.OpenIfExists);
            XmlDocument _doc = new XmlDocument();
            XmlElement _item = _doc.CreateElement("Admin");
            _item.SetAttribute("password", "Admin");
            _doc.AppendChild(_item);
            StorageFile file = await storage.CreateFileAsync("Admin.xml", CreationCollisionOption.ReplaceExisting);
            await _doc.SaveToFileAsync(file);
        }
        /// <summary>
        /// 获取用户或管理员密码字典
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> LoadPasswordFile(string fileName)
        {
            if(fileName=="AdminPassword")
            {
                First();
            }     
            StorageFolder storage = await ApplicationData.Current.LocalFolder.CreateFolderAsync(fileName, CreationCollisionOption.OpenIfExists);
            var files = await storage.GetFilesAsync();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var file in files)
            {
                XmlDocument doc = await XmlDocument.LoadFromFileAsync(file);
                string password = doc.DocumentElement.Attributes.GetNamedItem("password").NodeValue.ToString();
                dic.Add(file.DisplayName, password);
            }
            return dic;
        }
        /// <summary>
        /// 帐号注册
        /// </summary>
        public async static void Registered(string fileName, string user, string password)
        {
            StorageFolder storge = await ApplicationData.Current.LocalFolder.CreateFolderAsync(fileName, CreationCollisionOption.OpenIfExists);
            XmlDocument _doc = new XmlDocument();
            XmlElement _item = _doc.CreateElement(user);
            _item.SetAttribute("password", password);
            _doc.AppendChild(_item);
            StorageFile file = await storge.CreateFileAsync(user + ".xml", CreationCollisionOption.ReplaceExisting);
            await _doc.SaveToFileAsync(file);
            if (fileName == "UserPassword")
            {
                App.userpassworddictionary.Remove(user);
                App.userpassworddictionary.Add(user, password);
            }
            else if (fileName == "AdminPassword")
            {
                App.adminpassworddictionary.Remove(user);
                App.adminpassworddictionary.Add(user, password);
            }
        }
        /// <summary>
        /// 帐号是否被注册
        /// </summary>
        /// <returns></returns>
        public static bool IsSignUp_User(string user)
        {
            foreach (var d in App.userpassworddictionary)
            {
                if (d.Key == user)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsSignUp_Admin(string admin)
        {
            foreach (var d in App.adminpassworddictionary)
            {
                if (d.Key == admin)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
