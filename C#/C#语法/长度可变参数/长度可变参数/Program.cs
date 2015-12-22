using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 长度可变参数
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = Sum("aaaa",new int[] { 3, 4, 5, 6 });
            int j = Sum("bbbb",4, 5, 6, 7);
            Console.WriteLine(i);
            Console.WriteLine(j);
            Console.Read();
        }
        //长度可变参数为params类型，params类型参数必须放在最后面
        static int Sum(string str,params int[] num)
        {
            int result = 0;
            foreach (int i in num)
            {
                result += i;
            }
            return result;
        }
    }
}
