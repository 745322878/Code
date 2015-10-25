using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ查询
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = { "Alonso", "Zheng", "Smith", "Jone", "Small" };
            //var name=
            //    from n in names 
            //    where n.StartsWith ("S")
            //    select n;
            var name = names.Where(n => n.StartsWith("S"));
            foreach (var nam in name )
                Console.WriteLine(nam);
            Console.Read();
        }
    }
}
