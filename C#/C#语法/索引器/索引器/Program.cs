using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 索引器
{
    class Class1
    {
        int Temp0=5;
        int Temp1=6;
        
        public int this[int index]
        {
            get 
            {
                return (0 == index) ? Temp0 : Temp1;
            }
            set
            {
                if (index == 0)
                    Temp0 = value;
                else
                    Temp1 = value;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            Class1 a = new Class1();
            Console.WriteLine("Values -- To:{0},T1:{1}",a[0],a[1]);
            a[0] = 15;
            a[1] = 20;
            Console.WriteLine("Values -- To:{0},T1:{1}", a[0], a[1]);
            Console.ReadLine();

        }
    }
}
