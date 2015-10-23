using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// “空白应用程序”模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 天气预报
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    public sealed partial class App : Application
    {
        public  static List<Citys> nearFindCity =new List<Citys> ();
        //最近天气
        public static List<Citys> citynameList;
        private static Citys cityname1;
        
        //所有城市
        private TransitionCollection transitions;

        /// <summary>
        /// 初始化单一实例应用程序对象。    这是执行的创作代码的第一行，
        /// 逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
           
  
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;

            App.citynameList = new List<Citys>();
            readXmlFile();
            //string[] allcity =
            //{"北京","海淀","朝阳","顺义","怀柔","通州","昌平","延庆","丰台","石景山","大兴","房山","密云","门头沟","平谷",
            //"上海","闵行","宝山","川沙","嘉定","金山","青浦","松江","奉贤","崇明","浦东新区",
            //"天津","武清","宝坻","东丽","西青","北辰","宁河","静海","津南","塘沽", "蓟县",
            //" 重庆","永川","合川","江津","黔江","涪陵","云阳","巫山","梁平","丰都",
            //"郑州","三门峡","济源","焦作","新乡","安阳","鹤壁","濮阳","开封","商丘","许昌","周口","漯河","驻马店","信阳","南阳","平顶山","洛阳",
            //"昆明","昭通","曲靖","文山","红河","玉溪","普洱","西双版纳","普洱","临沧","保山","德宏","大理","楚雄","丽江","怒江","迪庆",
            //"长沙","张家界","怀化","常德","岳阳","益阳","株洲","湘潭","衡阳","邵阳","湘西",
            //"合肥","淮南","芜湖","黄山","宣城","池州","阜阳","淮北","安庆",
            //"哈尔滨","大兴安岭","黑河","伊春","鹤岗","佳木斯","双鸭山","七台河","鸡西","牡丹江","绥化","大庆","齐齐哈尔",
            //"长春","白城","松原","四平","吉林","延边","辽源","白山","通化",
            //"沈阳","铁岭","抚顺","本溪","辽阳","丹东","鞍山","营口","大连","盘锦","锦州","阜新","朝阳","葫芦岛",
            //"呼和浩特","阿拉善","巴彦淖尔","包头","乌海","鄂尔多斯","乌兰察布","锡林郭勒","赤峰","通辽","兴安","呼伦贝尔",
            //"石家庄","张家口","承德","秦皇岛","唐山","保定","廊坊","沧州","衡水","邢台","邯郸",
            //"太原","大同","朔州","忻州","阳泉","晋中","吕梁","长治","临汾","运城","晋城",
            //"西安","汉中","安康","商洛","咸阳","宝鸡","铜川","渭南","延安","榆林",
            //"成都","西宁","武汉 ","南京","南昌","萍乡","西宁","广州","贵阳","杭州","福州","台北","兰州","拉萨","银川","南宁","乌鲁木齐"};
            //foreach (string cityname in allcity)
            //{
            //    cityname1 = new Citys();
            //    cityname1.city = cityname;
            //    App.citynameList.Add(cityname1);
            //}
           
           
        }
        public async void readXmlFile()
        {
            //StorageFile file1 = null;
            string text;
            Citys cityname;
            List<Citys> citys = new List<Citys>();
            //var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("NewFolder");
            //foreach (StorageFile file in await folder.GetFilesAsync())
            //{
            //    if (file.Name == "XMLFile.xml")
            //    {
            //        file1 = file;
            //        break;
            //    }
            //}
            var file1 = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("CitysXmlFile.xml");
            if (file1 == null)
                return;
            IRandomAccessStream accesssStream = await file1.OpenReadAsync();
        
            using (StreamReader stream = new StreamReader(accesssStream.AsStreamForRead((int)accesssStream.Size)))
            {
                text = stream.ReadToEnd();
            }
            string a = "<string>";
            string b = "</string>";
            int indexa = 1;
            int indexb = 1;
            for (int i = 0; i < text.Length; i++)
            {
                indexa = text.IndexOf(a, indexa + 1);
                indexb = text.IndexOf(b, indexb + 1);
                if (indexa == -1 && indexb == -1)
                    break;
                string s = text.Substring(indexa + a.Length, indexb - indexa - a.Length);
                cityname1 = new Citys();
                cityname1.city = s;
                App.citynameList.Add(cityname1);
               
            }
                 

        }
        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 当启动应用程序以打开特定的文件或显示搜索结果等操作时，
        /// 将使用其他入口点。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                // TODO: 将此值更改为适合您的应用程序的缓存大小
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // 删除用于启动的旋转门导航。
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;

                // 当导航堆栈尚未还原时，导航到第一页，
                // 并通过将所需信息作为导航参数传入来配置
                // 新页面
                if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // 确保当前窗口处于活动状态
            Window.Current.Activate();
        }

        /// <summary>
        /// 启动应用程序后还原内容转换。
        /// </summary>
        /// <param name="sender">附加了处理程序的对象。</param>
        /// <param name="e">有关导航事件的详细信息。</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。    在不知道应用程序
        /// 将被终止还是恢复的情况下保存应用程序状态，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起的请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }
    }
}