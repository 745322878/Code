using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 定义事件
{
    class Program
    {
        static void Main(string[] args)
        {
            Connection myConnection = new Connection();
            Display myDisplay = new Display();
            myConnection.MessageArrived +=new MessageHanfler (myDisplay .DisplayMessage);
            myConnection .Connect();
            Console.Read();
        }
    }
}
