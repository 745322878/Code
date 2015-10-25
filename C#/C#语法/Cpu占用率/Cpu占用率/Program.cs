using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cpu占用率
{
    class Program
    {
       
        static void Main(string[] args)
        {
            MakeUsage(50);
        }
        private static void SinWave(object dummy)
23     {
24         while(true)
25         {
26             for(double i = 0.0; i < 2 * Math.PI; i += 0.1)
27             {
28                 Compute(500, Math.Sin(i)/2.0 + 0.5);
29             }
30         }
31     }
    } 
}
