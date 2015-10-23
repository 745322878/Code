using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 引用类型
{
    class Program
    {
        static void Main(string[] args)
        {
            string a="piapia";
            string a1 = a;
            Console.WriteLine(a);
            Console.WriteLine(a1);

            a = "bibi";
            a1 = a;
            Console.WriteLine(a);
            Console.WriteLine(a1);

            int b1 = 5;
            int b=b1;
            b1 = 6;
           
            Console.WriteLine(b);
            Console.WriteLine(b1);
            Console.ReadLine();
        }
    }
}
