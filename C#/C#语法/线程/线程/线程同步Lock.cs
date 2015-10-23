using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 线程
{
    //class Account
    //{
    //    private Object thisLock = new Object();
    //    int balance;
    //    Random r = new Random();
    //    //构造函数
    //    public Account(int initial)
    //    {
    //        balance = initial;
    //    }
    //    int Withdraw(int amount)
    //    {
    //        if (balance < 0)
    //            throw new Exception("Negative Balance");
    //        lock (thisLock)
    //        {
    //            if (balance >= amount)
    //            {
    //                Console.WriteLine("Balance before Withdrawal支出前" + balance);
    //                Console.WriteLine("Amount to Withdrawl支出" + amount);
    //                balance = balance - amount;
    //                Console.WriteLine("Balance after Withdrawal支出后" + balance);
    //                return amount;
    //            }
    //            else
    //            {
    //                return 0;
    //            }
    //        }
    //    }
    //    public void DoTransactions()
    //    {
    //        for (int i = 0; i < 100; i++)
    //        {
    //            Withdraw(r.Next(1,100));
    //        }
    //    }
    //}
    //class 线程同步Lock
    //{
    //    static void Main(string[] args)
    //    {
    //        Thread[] th = new Thread[10];
    //        Account acc = new Account(1000);
    //        for (int i = 0; i < 10; i++)
    //        {
    //            Thread t=new Thread (new ThreadStart (acc.DoTransactions) );
    //            th[i]=t;
    //        }
    //        for (int i = 0; i < 10; i++)
    //        {
    //            th[i].Start();
    //        }
    //        Console.ReadLine();

    //    }
    //}
}
