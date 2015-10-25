using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 正则表达式
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Matches1();
            Console.ReadLine();
        }
        private static void Match()
        {
            string input = @"c:\windows\testb.txt";
            //string pattern = @"^.+\\(.+)$";
            string pattern = @"^.+:\\.+\\(.+)$";
            //string input = @"age=30";
            //string pattern = @"^.+=(.+)$";
            Match match = Regex.Match(input, pattern);
            while (match.Success)
            {
                Console.WriteLine(match.Groups[1]);
                match = match.NextMatch();
            }

        }
        private static void Matches()
        {
            MatchCollection matches;
            List<int> number = new List<int>();
            string input = "大家好，我是hebe ，我22岁了，身高180,我们团队有3个女生";
            string pattern = @"\d+";
            Regex r = new Regex(pattern);
            matches = r.Matches(input);
            foreach (Match match in matches)
            {
                Console.WriteLine("{0} at {1}", match.Value, match.Index);
                number.Add(Convert.ToInt32(match.Value));
            }
            foreach (int i in number)
                Console.WriteLine(i);
        }
        public static void Replace()
        {
            string input = @"我的生日是05/21/2010耶";
            string replacement = "$3/$1/$2";
            string pattern = @"(\d+)/(\d+)/(\d+)";
            Console.WriteLine(Regex.Replace(input, pattern, replacement));
        }
        private static void Replace1()
        {
            string input =@"我我我我爱爱爱爱你你你你";
            string pattern = @"(.)\1+";
            string replacement = "$1";
            Console.WriteLine(Regex.Replace(input,pattern ,replacement));
            
        }

        private static void Matches1()
        {
            MatchCollection matches;
            List<string> result = new List<string>();
            List<string> result1 = new List<string>();
            string input = @"Hi,how are you?Welcome to our country!";
            string pattern = @"\b\W+(\w{3})\W+\b";
            Regex r = new Regex(pattern );
            matches = r.Matches(input);
            Console.WriteLine("All 3 words:");
            foreach (Match match in matches)
            {
               // Console.WriteLine("{0} at {1}",match.Value ,match.Index );
                result.Add(match.Value);
            }
            foreach (string str in result)
            {
                
                result1.Add(str.Substring(1, 3));
            }
            foreach (string output in result1)
            {
                Console.WriteLine(output);
            
            }
        }
    }
}
