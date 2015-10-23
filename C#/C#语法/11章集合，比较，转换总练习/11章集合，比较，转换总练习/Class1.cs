using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11章集合_比较_转换总练习
{
    public class People : CollectionBase, ICloneable
    {
        public object Clone()
        {
            People clonePeople = new People();
            Person currentPerson, newPerson; 
            foreach (DictionaryEntry p in Dictionary )
            {
                currentPerson = p.Value as Person;
                newPerson =new Person ():
                newPerson.Name=currentPerson .Name;
                newPerson.Age=currentPerson .Age;
                clonePeople .Add(newPerson);
            }
            return clonePeople ;
        }
    }
}
