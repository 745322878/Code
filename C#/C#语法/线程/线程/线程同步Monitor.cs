using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 线程
{
    //class Program1
    //{
    //    private Queue input;
    //    public Program1()
    //    {
    //        input= new Queue();
    //    }
    //    public void Add(object qValue)
    //    {
    //        input.Enqueue(qValue);
    //    }
    //    public  void Print()
    //    {
    //        IEnumerator  eenum= input.GetEnumerator();
    //        while (eenum.MoveNext() )
    //        {
    //            Console.WriteLine(eenum.Current.ToString ());
    //        }
    //    }
    //    public void DoWork(object data)
    //    {
    //        Monitor.Enter(input);
    //        Console.WriteLine("队列已被线程{0}锁定",data );
    //        for (int i = 0; i < 20; i++)
    //        {
    //            Add(string .Format ("{0},{1}",data,i ));
    //        }
    //        Print();
    //       Monitor.Exit(input );
    //        Console.WriteLine("队列已被线程{0}释放",data );
    //    }
    //    static void Main(string[] args)
    //    {
    //        Program1 sample = new Program1();
    //        Thread th = new Thread(sample.DoWork);
    //        Thread th1 = new Thread(sample.DoWork);
    //        th.Start("T1");
    //        th1.Start("T2");
    //        Console.ReadLine();
    //    }
    //}
}
