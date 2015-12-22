using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Socket__ChatRoom_Server
{
    /// <summary>
    /// SocketData.xaml 的交互逻辑
    /// </summary>
    public partial class SocketData : Window
    {
        Thread th;
      
        private  List<Data> datas = new List<Data>();
        public List<Data> datas1;
        string message;
        public SocketData()
        {
            InitializeComponent();
            
            //开启一个线程，新建一个委托，让委托入口指向ReceiveData()
            th = new Thread(new ParameterizedThreadStart(ReceiveData));
            th.Start(App.socketServer);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SocketData_Closed();
        }

        private void SocketData_Closed()
        {
            App.socketServer.Dispose();
            MainWindow.server.Dispose();
            th.Abort();
            this.Close();
        }
        //接收数据
        private void ReceiveData(object client)
        {

            Socket nowsocket = (Socket)client;
            while (true)
            {
                byte[] data = new byte[1024];               
                int bytes = 0;

                try
                {
                    bytes = nowsocket.Receive(data);
                    message = Encoding.UTF8.GetString(data, 0, bytes);
                    //datas.Add(new Data { Message=message});
                    //两线程可以同步执行                 
                    Dispatcher.Invoke((Action)(() =>
                    {

                        listbox.Items.Add(new Data { Message = message });
                       
                    }));
                    data = Encoding.UTF8.GetBytes(message);
                    SendAll(data);
                }
                catch (ArgumentNullException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                catch (SocketException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                catch (ObjectDisposedException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

            }
        }

        private void SendAll(byte[] data)
        {
            for (int i = 0; i < MainWindow.socketnum; i++)
            {
                Socket s;
                s = MainWindow.clients[i];
                
                //IPEndPoint ipep=new IPEndPoint(IPAddress.Parse("10.0.0.4"),11111);
               // s.SendTo(data,ipep);
              s.Send(data);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
