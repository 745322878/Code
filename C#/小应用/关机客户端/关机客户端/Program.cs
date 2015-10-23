using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace 关机客户端
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ///创建终结点（EndPoint）   
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("172.20.0.229"), 6666);
                ///创建socket并连接到服务器
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Console.WriteLine("正在连接");
                ///连接服务器
                try
                {
                    client.Connect(ipep);
                    //因为客户端只是用来向特定的服务器发送信息，所以不需要绑定本机的IP和端口。不需要监听。
                    Console.WriteLine("连接成功");
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e.ToString());
                    return;
                }
                while (true)
                { 
                    ///向服务器发送信息
                    byte[] data = new byte[1024];
                    string input = Console.ReadLine();
                    data = Encoding.UTF8.GetBytes(input);
                    //将准备发送的消息string转成btye
                    client.Send(data, data.Length,0);
                    //发送消息

               
                   ///接受从服务器返回的信息
                    string str = "";
                    byte[] recv = new byte[1024];
                    int bytes;
                    bytes = client.Receive(recv, recv.Length, 0);
                    
                    //从服务器接受信息
                    str = Encoding.UTF8.GetString(recv, 0, bytes);
                    //将服务器传来的信息byte转为string
                    Console.WriteLine(str);
                    //显示服务器返回的信息*/
                   
                 }
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message );
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message );
            }
            
            
        }
    }
}
