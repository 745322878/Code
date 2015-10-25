using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 面向对象__多态接口
{
    class Program  
    {
       
        static void Main(string[] args)
        {
            string name;
            int age;
            IStudent stu = new Student("张三", 18);
            name=stu.GetName();
            age=stu.GetAge();
            stu.PrintName(name);
            stu.PrintAge(age);
            name = stu.ModifyName("李四");
            age=stu.ModifyAge(19);
            stu.PrintName(name);
            stu.PrintAge(age);
            Console.ReadLine();
            
        }
    }
}
