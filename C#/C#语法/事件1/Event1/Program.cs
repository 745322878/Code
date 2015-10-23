using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event1
{
    class Program
    {
        public delegate void ChangeEventHandler(object sender, EventArgs e);
        static void Main(string[] args)
        {
           
            I i =new MyClass ();
            i.MyEvent +=new MyDelegate(f);  //给事件绑定一个委托
            i.FireAway();
            Console.Read();
        }
        private static void f()
        {
            Console.WriteLine("This is called when the event fired");
        }
    }
    public  delegate void MyDelegate();

    public interface  I
    {
        event MyDelegate MyEvent;
        event EventHandler MyGuidlineEvent;
        void FireAway();
    }
    public class MyClass : I
    {
        public event MyDelegate MyEvent;
        public event EventHandler MyGuidlineEvent;
        public void FireAway()
        {
            if (MyEvent != null)
                MyEvent();
        }
    }
 }
