using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11章集合_比较_转换总练习
{
    public class Person 
    {
        private string name;
        private int age;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }
        public static bool operator >(Person p, Person p1)
        {
            return p.Age > p1.Age; 
        }
        public static bool operator <(Person p, Person p1)
        {
            return p.Age < p1.Age;
        }
        public static bool operator >=(Person p, Person p1)
        {
            return !(p.Age < p1.Age);
        }
        public static bool operator <=(Person p, Person p1)
        {
            return !(p > p1);
        }
        public IEnumerator GetEnumerator()
        { 
            foreach (int age in mypeopel)
        }
    }
   
}
