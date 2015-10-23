using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace static实例测试
{
   public class Class
    {
        internal int num = 5;
        public static int getValue(int value)
        {
            return value;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Class a = new Class();
            
            Console.WriteLine(Class.getValue(10));
            Console.ReadLine();
        }
    }
}
