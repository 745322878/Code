using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace 关机服务器端
{
    class Program
    {
        static void Main(string[] args)
        {

            ///创建终结点（EndPoint）
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("172.20.0.229"),6666);
            
            ///创建socket并开始监听
            Socket server = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp );
            //创建一个socket对像，如果用udp协议，则要用SocketType.Dgram类型的套接字
            server.Bind(ipep);
            //绑定EndPoint对像
            server.Listen(1);
            //开始监听
            Console.WriteLine("正在连接");
           
            ///接受到客户端连接，为此连接建立新的socket，并接受信息
            Socket client = server.Accept();//为新建连接创建新的socket
            Console.WriteLine("连接成功");
            while (true)
            {
                string str = "";
                byte[] data = new byte[1024];
                int bytes;
                bytes = client.Receive(data, data.Length, 0);//从客户端接受信息
                str = Encoding.UTF8.GetString(data, 0, bytes  );
                //将客户端传来的信息byte转为string
                Console.WriteLine(str);
                //显示客户端传来的信息
                if (str.Contains("关机")) ;

                

                ///给客户端返回信息
                client.Send(data, bytes , 0);
               
            }
            client.Close();
            server.Close();
            Console.ReadLine();



        }
    }
}
