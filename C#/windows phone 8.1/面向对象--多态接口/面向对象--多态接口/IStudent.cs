using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 面向对象__多态接口
{
    public interface IStudent
    {
        string   GetName();
        int  GetAge();
        string ModifyName(string Name);
        int ModifyAge(int Age);
        void PrintName(string Name);
        void PrintAge(int Age);
    }
}
