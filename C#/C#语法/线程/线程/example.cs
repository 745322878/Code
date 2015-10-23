using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 线程
{
    class example
    {
       
        static void Main(string[] args)
        {
            Thread[] th = new Thread[4];
            Ticket  t=new Ticket ();
            for (int i = 0; i < 4; i++)
            {
                th[i] = new Thread(new ThreadStart(t.randomSell));
                th[i].Name = "窗口" + i;
                th[i].Start();
                
            }
            Console.Read();
        }
    }
    public class Ticket
    { 
        private static Object thisLock = new Object();
        static int number=0;
        

        public void randomSell()
        {
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(1000);
                this.Sale();
               
            }
        }
       
        public  void Sale()
        {
            
            lock (thisLock)
            {
                if (number <= 66)
                {
                    number++;
                   
                    Console.WriteLine("{0}:售出{1}号车票.", Thread.CurrentThread.Name, number );
                }
                else
                {
                    Console.WriteLine("{0}:已售完.",Thread .CurrentThread .Name );
                }
            }
            
        }
    }
}
