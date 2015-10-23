using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMP匹配模式
{
    class Program
    {
        static void Main(string[] args)
        {
            string ch;
            int[] next=new int[100];
            int[] nextval = new int[100];
            Console.WriteLine("请输入主串：");
            ch = Console.ReadLine().ToString();
            Get_Next(ch, next);
            Get_NextVal(ch, next, nextval);
            Console.Read();

        }
        public  static void  Get_Next(string ch,int[] next)
        {
            int j = 1, k = 0;
            next[1] = 0;
            while (j <= ch.Length)
            {
                if (k == 0 || ch[j-1] == ch[k-1])
                {
                    ++j;
                    ++k;
                    next[j] = k;
                }
                else
                {
                    k = next[k];
                }
            }
            for (int i = 1; i < ch.Length+1 ; i++)
            {
                Console.WriteLine(next[i]);
            }
            Console.WriteLine(k);
            
        }
        public static void Get_NextVal(string ch, int[] next, int[] nextval)
        {
            int j = 1, k = 0;
            Get_Next(ch, next);
            nextval[0] = 0;
            while (j < ch.Length)
            {
                k = next[j-1];
                if (ch[j] == ch[k])
                {
                    nextval[j] = nextval[k];
                }
                else
                    nextval[j] = next[j];
                j++;
            }
            for (int i = 0; i < ch.Length; i++)
            {
                Console.WriteLine(nextval[i]);
            }
            Console.ReadLine();
           
        }
    }
}
