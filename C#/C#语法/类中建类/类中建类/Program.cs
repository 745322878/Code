using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 类中建类
{
    class Program
    {
        public class A
        { 
            public  void Print()
            {
                Console.WriteLine("This is A!");
            }
        }
        public void Print()
        {
            Console.WriteLine("This is Program!");
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            A a = new A();
            program.Print ();
            a.Print();

            Console.Read();
        }
    }
}
