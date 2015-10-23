using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace socket1
{
    class Program
    {
        static  Socket client=null;
        static string name;
        static void Main(string[] args)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("172.20.0.229"), 6666);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(ipep);
                //连接服务器成功

            }
            catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("连接服务器成功，请输入昵称");
            name = Console.ReadLine();
            //建立一个接收服务器消息的线程
            Thread newthread = new Thread(new ThreadStart(ReceiveData));
            newthread.Start();

            name = name + "说：";
            byte[] data1 = new byte[1024];
            //发消息给服务器端
            while (true)
            {
                string input = Console.ReadLine();
                input = name + input;
                data1 = Encoding.UTF8.GetBytes(input);
                client.Send(data1);
            }
            client.Close ();
            Console.ReadLine ();
        }
        //接收服务器给客户端发来的信息，并显示在屏幕上
        internal static void ReceiveData()
        {
            byte [] data =new byte[1024];
            int bytes=0;
            while (true)
            {
                try
                {
                    bytes=client.Receive (data );
                }
                catch(SocketException e)
                {
                    Console.WriteLine(e.ToString ());
                    break;
                }
                string message=Encoding .UTF8.GetString (data,0,bytes );
                Console.WriteLine(message );
            }
        }
    }
}
