using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 面向对象__多态接口
{
    public class Student  :IStudent
    {
        private string name;
        private int age;
        public Student(string Name,int Age)
        {
            this.age = Age;
            this.name = Name;
        }
        public string GetName()
        {
            return name;
        }
        public int GetAge()
        {
            return age;
        }
        public string ModifyName(string Name)
        {
            return Name;
        }
        public int ModifyAge(int Age)
        {
            return Age;
        }
        public void PrintName(string Name)
        {
            Console.WriteLine("This student'name is {0}",Name);
        }
        public void PrintAge(int Age)
        {
            Console.WriteLine("Thisstydent'name is {0}",Age);
        }
    }
}
