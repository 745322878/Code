using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 布局管理
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    ///
    public sealed partial class MainPage : Page
    {
        //前一次按的的运算符
        private string Operation = "";
        //结果
        private float   num = 0;
        private string num1 = "0";
        private int flag = 0;
        bool KeyPreview = true;
        public MainPage()
        {
            this.InitializeComponent();
           
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
      
        //数字按键的事件
        private void DigitBtn_Click(object sender ,RoutedEventArgs e)
        {
            //如果之前的操作时按下了等于符号，清除之前的操作
            if (Operation == "=")
            {
                Result.Text = "";
                //结果框
                Formula.Text = "";
                //公式框
                Operation = "";
                //操作符
                num1 = "0";
                //结果
                flag = 1;
            }
            string s = ((Button)sender).Content.ToString();
            if (s == "." && Result.Text.Contains(".")) 
                ;
            else
            {
                //获取接下来按下的数字
                Result.Text = Result.Text + s;

                //结果框显示
                Formula.Text = Formula.Text + s;
                //公式框显示
            }
         }   
        //运算符+ - * / 的事件
        private void OperationBtn_Click(object sender ,RoutedEventArgs e)
        {
            if (num1 == "Error")
            {
                Result.Text = "";
                Formula.Text = "";
                Operation = "";
                num1 = "0";
            }
            else
            {
                OperationNum("=");
                Result.Text = num1;
                //如果之前的运算符是等号
                if (Operation == "=" || Operation == "+" || Operation == "-" || Operation == "*" || Operation == "/")
                {
                    Formula.Text = Result.Text;
                    //刚才的结果显示到公式框中
                    Operation = "";
                    //初始化运算符
                }


                string s = (((Button)sender).Content.ToString());
                //获取下来按下的运算符        
                Formula.Text = Formula.Text + s;
                //公式框显示
                OperationNum(s);
                //通过运算符s计算
                Result.Text = "";
                //初始化结果框
            }
                
          
        }
        //=  的事件
        private void Result_Click(object sender ,RoutedEventArgs e)
        {
            OperationNum("=");
            //input框显示结果num
            Result.Text = num1;
        }   
        //del 的事件
        private  void Del_Click(object sender ,RoutedEventArgs e)
        {
            //全部清空
            Result.Text = "";
            //运算结果
            Formula.Text = "";
            //公式
            Operation = "";
            //运算符
            num1 = "0";

            flag = 1;
        }
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            int l = Result.Text.Length;
            int l1 = Formula.Text.Length;
            if (Operation == "=")
            {
                //全部清空
                Result.Text = "";
                //运算结果
                Formula.Text = "";
                //公式
                Operation = "";
                //运算符
                num1= "0";

                flag = 1;
            }
            else
            {
                if (l > 0)
                {
                    Result.Text = Result.Text.Substring(0, l - 1);

                }
                if (l1 > 0)
                    Formula.Text = Formula.Text.Substring(0, l1 - 1);
            }
        }
        //通过运算符进行运算
        private async void OperationNum(string s)
        {
            //结果框有数字
            if (Result.Text != "")
            {
                switch (Operation)
                {
                    case "":
                        num = float.Parse(Result.Text);
                        num1 = num.ToString();
                        //结果就是结果框的数
                        Operation = s;
                        //记录运算符
                        break;
                    case "+":
                        if (Result.Text == ".")
                            num += 0;
                        else
                         num += float.Parse(Result.Text);
                        num1 = num.ToString();
                        Operation = s;
                        break;
                    case "-":
                        if (Result.Text == ".")
                            num -= 0;
                        else
                              num -= float.Parse(Result.Text);
                        num1 = num.ToString();
                        Operation = s;
                        break;
                    case "*":
                        if (Result.Text == ".")
                            num *= 0;
                        else
                             num *= float.Parse(Result.Text);
                        num1 = num.ToString();
                        Operation = s;
                        break;
                    case "/":
                        if (Result.Text !="0"&&Result .Text!=".")
                        {
                            num /= float.Parse(Result.Text);
                            num1 = num.ToString();
                            
                        }
                        else
                        {
                            num1 = "";
                            await new MessageDialog("Error").ShowAsync();
                        }
                        Operation = s;
                        break;
                    default: break;


                }
            }
            //结果框不是数字，即就是运算符
            else
                Operation = s;
        }
        

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。
        }

     
    }
}
