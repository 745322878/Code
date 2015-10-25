using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace 处理事件
{
    class Program
    {
        static int counter = 0;
        static string displayString = "This String will appear one letter at a time .";
        static void Main(string[] args)
        {
            Timer myTimer = new Timer(100);
            myTimer.Elapsed += new ElapsedEventHandler(WriteChar);           //给事件添加一个处理程序
            myTimer.Start();
            System.Threading.Thread.Sleep(200);
            Console.Read();
        }
        static void WriteChar(object source, ElapsedEventArgs e)                
        {
            Console.Write(displayString [counter++% displayString .Length ]);  
        }
    }
}
