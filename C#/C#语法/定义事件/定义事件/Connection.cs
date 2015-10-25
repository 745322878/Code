using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace 定义事件
{
    public  delegate void MessageHanfler(string messageText);
    public class Connection
    {

        public event MessageHanfler MessageArrived;
        private Timer pollTimer;
        public Connection()
        {
            pollTimer = new Timer(100);
            pollTimer.Elapsed += new ElapsedEventHandler(CheckForMessage);

        }
        public void Connect()
        {
            pollTimer.Start();

        }
        public void Disconnect()
        {
            pollTimer.Stop();
        }
        private static Random random = new Random();
        public void CheckForMessage(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Checking for new message .");
            if ((random.Next(9) == 0) && (MessageArrived != null))
            {
                MessageArrived("Hello Mum!");
            }
        }
    }
}
