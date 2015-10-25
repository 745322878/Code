using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace socket
{
    class Program1
    {
        //private  List<Socket > clientsockets = new List<Socket >();
        private Socket[] clientsockets = new Socket[40];
        private int socketNum=0;
        Socket socketServer = null;
        public Program1()
        {
     
            //先建立socketServer套接字，监听是否有客户端连接。,
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("172.20.0.229"), 6666);
            socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);        
            socketServer.Bind(ipep);
            socketServer.Listen(10);
            Console.WriteLine("等待客户端连接");
            while (true)
            {
                //和客户端连接成功，返回套接字
                Socket client  = socketServer.Accept();
                Console.WriteLine("连接成功");
                clientsockets[socketNum++] = client;
                //将客户端client加入到Socket类型的数组clientsockets里，客户端数量自加socketNum++
                Thread th = new Thread(new ParameterizedThreadStart (ReceiveData));
                //建立一个线程，和这个客户端通信(接收客户端传来的信息)
                //建立了一个ParameterizedThreadStart委托，指明线程的入口方法为ReceiveData
                th.Start(client);
                //让服务器与这客户端之间的线程跑起来，
                //提供包含执行线程方法ReceiveData要使用的数据对象client
              
            }
        }
      

         private void ReceiveData(object client)
         {
             Socket nowClient = (Socket)client;
             //不断接收客户端传来的消息，并发送给其他客户端
             while (true)
             {
                 int bytes = 0;
                 byte[] data = new byte[1024];
                 try
                 {
                     bytes = nowClient.Receive(data);//接收客户端传来的消息
                 }
                 catch (Exception e)
                 {
                     Console.WriteLine(e.ToString());
                 }
                 string message = Encoding.UTF8.GetString(data, 0, bytes);
                 data = Encoding.UTF8.GetBytes(message);
                 SendAll(data);
                 //将消息发送到其他客户端
             }
         }
         public void SendAll(byte[] data )
         {
             //使用循环向colientsockets数组中的每一个客户端发送消息
             for (int i = 0; i < socketNum ; i++)
             {
                 clientsockets[i].Send(data);   
             }
         }
         static void Main(string[] args)
         {
             new Program1();
             Console.Read();
         }
     
    }
}
