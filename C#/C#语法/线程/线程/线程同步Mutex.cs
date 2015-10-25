using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 线程
{
    //class 线程同步Mutex
    //{
    //    private static Mutex mut = new Mutex();
    //    private const  int numThreads = 3;
    //    private static void UseResource()
    //    {
    //        mut.WaitOne();
    //        Thread.Sleep(1000);
    //        Console.WriteLine("{0} has entered the protected area",Thread .CurrentThread .Name );
    //        Thread.Sleep(1000);
    //        Console.WriteLine("{0} is leaving the projected area",Thread .CurrentThread .Name);
    //        mut.ReleaseMutex();
    //    }
    //    static void Main(string[] args)
    //    {
    //        for (int i = 0; i < numThreads; i++)
    //        {
    //            Thread th = new Thread(new ThreadStart(UseResource));
    //            th.Name = string.Format("Thread{0}", i + 1);
    //            th.Start();
    //        }
    //        Console.ReadLine();
    //    }
    //}
}
