using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 面向对象__封装2
{
    class Student
    {
        private string name;
        private int age;
        public Student(string Name,int Age)
        {
            this.name = Name;
            this.age = Age;
        }
        public  string  GetName()
        {
            return name;
        }
        public int GetAge()
        {
            return age;
        }
        public string  ModifyName( string Name)
        {
            return Name;
        }
        public int ModifyAge(int Age)
        {
            return Age;
        }
    }
}
