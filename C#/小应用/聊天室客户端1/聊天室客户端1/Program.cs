using System;
using System.Collections.Generic;
using System.Text;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace multithreadclient
{
    class ClientSocket
    {
        static Socket client = null;
        static string name;

        //接收服务器发来的消息
        public static void ReceiveData()
        {
            byte[] data = new byte[1024];
            int bytes = 0;
            while (true)
            {
                try
                {
                    bytes = client.Receive(data, data.Length, 0);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                string message = Encoding.UTF8.GetString(data, 0, bytes);
                Console.WriteLine(message);
            }
        }

        static void Main(string[] args)
        {

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("192.168.1.101"), 6666);
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
            Thread newThread = new Thread(new ThreadStart(ClientSocket.ReceiveData));
            newThread.Start();

            name = name + "说:";
            byte[] data1 = new byte[1024];
            while (true)
            {

                string input = Console.ReadLine();
                input = name + input;
                data1 = Encoding.UTF8.GetBytes(input);
                client.Send(data1);
                //发送客户端消息
            }
            client.Close();
            Console.ReadLine();

        }
    }
}