using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 舞伴问题
{
    class Program
    {
          public  struct Person
         {
            public char[] name;
             public  char sex;
          };
        static void Main(string[] args)
        {
           
        }
        public void DancePartner(Person[] dancer, int num)
        { 
            //结构体数组dancer中存放跳舞的男女，num是跳舞的人数
            int i;
            Person p;
            Queue Mdancers, Fdancers;
            InitQueue(Mdancers);

        }

        private void InitQueue(Queue Mdancers)
        {
            throw new NotImplementedException();
        }
    }
}
