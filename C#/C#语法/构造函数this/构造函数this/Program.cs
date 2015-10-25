using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 构造函数this
{
    class MyClass
    {
        
        public static int value;
        public  string userName;
       
        
        public MyClass ()
       {
             value=10;
        }
        public MyClass (string firstName):this()
        {
            userName =firstName ;
           
            Console.WriteLine("{0},{1}",userName ,value );
        }
        public MyClass(string firstName, int value1)
            : this("this 构造函数" )
        {
            userName = firstName;
            
            Console.WriteLine("{0},{1}", userName, value1);
        }
       
    }
    class Class1 : MyClass
    {
 
        static string name;
        new static int value;
        public Class1()
        {
            value = 20;
         }
        //public Class1(string Name)
        //    : this()
        //{
        //    name = Name;
            
        //    Console.WriteLine("{0'",name ,value );
        //}
        public Class1(string Name):base("base 构造函数",30)

        {
            name = Name;
            Console.WriteLine("{0}",name  );
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MyClass a=new MyClass ("基类");
            MyClass b = new MyClass("基类",10);
            Class1 c = new Class1("子类");
            Console.Read();
           
        }
    }
}
