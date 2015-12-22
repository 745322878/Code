using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket__ChatRoom_Server
{
     public class Class1:INotifyPropertyChanged
    {
         private  List<Data> datas;
         public  List<Data> Datas
         {
             get
             {
                 return datas;
             }
             set
             {
                 datas = value;
                 if (PropertyChanged != null)
                 {
                     PropertyChanged(this, new PropertyChangedEventArgs("Datas"));
                 }
             }
         }
         public  event PropertyChangedEventHandler PropertyChanged;
    }
}
