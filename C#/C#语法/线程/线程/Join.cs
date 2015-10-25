using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 线程
{
    class Join
    {
        //static void Main(string[] args)
        //{
        //    Thread th = new Thread(new ThreadStart(ShowInfo));
        //    for (int j = 0; j < 20; j++)
        //    {
        //        if (j == 10)
        //        {
        //            th.Start();
        //            th.Join ();
                    
        //        }
        //        else
        //        {
        //            Console.WriteLine("主线程"+j);
        //        }
        //    }
     
        //    Console.ReadLine();
        //}
        private static void ShowInfo()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("子进程:"+i);
               
            }
        }
    }
}
