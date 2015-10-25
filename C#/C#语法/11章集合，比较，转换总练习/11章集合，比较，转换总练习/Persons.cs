using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11章集合_比较_转换总练习
{
    public class Persons:DictionaryBase         //键值集合
    {
        //
        public void Add(Person  newPerson)
        {
            Dictionary.Add(newPerson.Name ,newPerson );
        }
       /* public void Add(string name, Person newPerson)
        {
            Dictionary.Add(name, newPerson);
        }*/
        public void Remove(string newName)
        {
            Dictionary .Remove (newName);
        }
        public Person this[string newName]      //索引器，通过一个字符串索引符来访问，该字符索引符为人名，
                                                    //与Person.Name属性相同。
        {
            get 
            {
                return (Person)Dictionary [newName ];
            }
            set
            {
                Dictionary [newName] = value;
            }
        }
    }

   
    
}
