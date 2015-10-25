using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 聊天室终版
{
    class Program
    {
        Socket server = null;
        private int socketNum = 0;//连接服务器的客户端数
        private Socket[] socketUser = new Socket[40];//将客户端存入数组里
        //构造函数
        public Program()
        {
            //建立scoket
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("172.20.0.229"), 6666);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipep);
            server.Listen(10);
            //打开监听
            Console.WriteLine("等待客户端连接");
            AccpetUser();
        }

        //监听客户端,并使用线程连接客户端
        private void AccpetUser()
        {
            while (true)
            {
                Socket nowClient = server.Accept();
                Console.WriteLine("一个客户端连接成功");
                socketUser[socketNum++] = nowClient;
                //将套接字（通道存入数组）
                Thread nowThread = new Thread(new ParameterizedThreadStart(ReceiveData));
                //开一个线程， 接受 nowClient 发来的信息。
                nowThread.Start(nowClient);
            }
        }

        //接收客户端的信息
        private void ReceiveData(object client)
        {
            Socket nowClient = (Socket)client;

            while (true)
            {
                int bytes = 0;
                byte[] data = new byte[1024];
                try
                {
                    bytes = nowClient.Receive(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                string message = Encoding.UTF8.GetString(data, 0, bytes);
                data = Encoding.UTF8.GetBytes(message);
                SendAllUser(data);
                //将消息发送到其他客户端
            }
        }
        //将消息发送到其他客户端
        private void SendAllUser(byte[] data)
        {
            for (int i = 0; i < socketNum; i++)
            {

                socketUser[i].Send(data);
            }
        }
        static void Main(string[] args)
        {
            new Program();
            Console.Read();
        }
    }
}