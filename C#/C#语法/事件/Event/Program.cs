using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
    class Program
    {
        
       
        static void Main(string[] args)
        {
            var e = new EventTest(5);
            e.SetValue(100);
            e.ChangeNum += new EventTest.NumManipulationHandler(EventTest.EventFired);
            e.SetValue(200);
            Console.ReadLine();
        }
    }
    class EventTest
    {
        private int value;
        public delegate void NumManipulationHandler();      //声明委托
        public event NumManipulationHandler ChangeNum;      //将事件与委托绑定
        public EventTest(int n)                             //构造函数，每次都要先设值
        {
            SetValue(n);
        }
        public static void EventFired()
        {
            Console.WriteLine("Binded Event fired");
        }


        protected virtual void OnNumChanged()
        {
            if (ChangeNum != null)
            {
                ChangeNum();
            }
            else
            {
                Console.WriteLine("Event fired!");
            }
        }
        public void SetValue(int n)     //设值时触发事件
        {
            if (value != n)
            {
                value = n;
                OnNumChanged();
            }
        }
    }
    
}
