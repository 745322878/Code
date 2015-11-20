using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding
{
    class Person:INotifyPropertyChanged
    {
        private string name;
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
        private  int age;
        public int Age
        {
            set
            {
                age=value ;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this,new PropertyChangedEventArgs ("Age"));
                }
            }
            get
            {
                return  age;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
