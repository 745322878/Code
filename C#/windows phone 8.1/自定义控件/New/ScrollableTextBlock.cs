using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace New
{
    class ScrollableTextBlock :ContentControl
    {
        private StackPanel stackpanel;
        public  ScrollableTextBlock()
        {
            DefaultStyleKey = typeof(ScrollableTextBlock); 
        }
        private static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(ScrollableTextBlock ),
            new PropertyMetadata("ScrollableTextBlock", OnTextPropertyChanged)
            );
        //增加Text依赖属性 .四个参数依次是 Text依赖属性的属性名称,属性类型,属性所有者类型,属性元数据实例

        public string Text  //累死一般属性的访问器 也是Text依赖属性的属性名称
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        //处理属性改变事件
        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScrollableTextBlock source = (ScrollableTextBlock) d;
            string value = (string)e.NewValue;
            source.ParseText(value);
        }
        protected override void OnApplyTemplate()  //每当应用程序代码或内部进程调用ApplyTemplate
            //,都将调用此方法.简单说,就是UI元素在显示前都会调用此方法.
        {
            base.OnApplyTemplate();
            this.stackpanel = this.GetTemplateChild("StackPanel") as StackPanel;
            this.ParseText(this.Text);
        }

        private void ParseText(string value) //处理文字数据，依据空格折行
        {
            string[] textBlockTexts = value.Split(' ');
            if (this.stackpanel == null)
            {
                return;
            }
            this.stackpanel.Children.Clear();
            for (int i = 0; i < textBlockTexts.Length; i++)
            {
                TextBlock textBlock = this.GetTextBlock();
                textBlock.Text = textBlockTexts[i].ToString();
                this.stackpanel.Children.Add(textBlock);
            }

        }

        private TextBlock GetTextBlock() //生成一个 TextBolck控件
        {
            TextBlock textBlock = new TextBlock();
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.FontSize = this.FontSize;
            textBlock.FontFamily = this.FontFamily;
            textBlock.FontWeight = this.FontWeight;
            textBlock.Foreground = this.Foreground;
            textBlock.Margin = new Thickness(0, 0, 0, 0);
            return textBlock;
        }
   }
}
