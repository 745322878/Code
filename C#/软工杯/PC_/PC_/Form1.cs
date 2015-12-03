using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace PC_
{
    public partial class Form1 : Form
    {
        const int MOUSEEVENTF_MOVE = 0x0001;      //移动鼠标 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;// 模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;// 模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040; //模拟鼠标中键抬起 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000; //标示是否采用绝对坐标
        const int MOUSEEVENTF_WHEEL = 0x800;
        const int VK_CONTROL = 0x11;
        const int VK_ADD = 0x6B;
        const int VK_SUBTRACT = 0x6D;
        [System.Runtime.InteropServices.DllImport("user32")]
        //[DllImport("user32.dll")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
         [System.Runtime.InteropServices.DllImport("user32")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        private int D_valueX, D_valueY;//发送过来的X Y 坐标差
        private int count;
        private string mouseEvent;
        private Socket phoneClient = null;
        int setptX, setptY;
        Socket pcServer;
        private object lockObject = new object();
        public Form1()
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            //Thread mth = new Thread(new ThreadStart(InitializeComponent));
            //mth.Start();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread mth = new Thread(new ThreadStart(Connect));
            mth.Start();
        }

        private void Connect()
        {
            byte[] buffer;
            string getData;
            string[] x_y_MouseEvent;
            int dataSize = 0;
            pcServer = null;//
            button1.Enabled = false;
            button2.Enabled = true;
            //IPAddress Ip = new IPAddress(new byte[] {172,0,0,76});
            IPAddress Ip = IPAddress.Any;
            IPEndPoint endPoint = new IPEndPoint(Ip, 4567);
            pcServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//
            pcServer.Bind(endPoint);
            pcServer.Listen(10);
            phoneClient = pcServer.Accept();
           // if (phoneClient != null)
            //{ MessageBox.Show("连接成功！"); }
            while (true)
            {
                buffer = new byte[2014];
                try
                {
                    dataSize = phoneClient.Receive(buffer, buffer.Length, 0);
                    getData = Encoding.UTF8.GetString(buffer, 0, dataSize);
                    x_y_MouseEvent = getData.Split(',');
                    D_valueX = Convert.ToInt32(Convert.ToDouble( x_y_MouseEvent[0]));
                    D_valueY = Convert.ToInt32(Convert.ToDouble( x_y_MouseEvent[1]));
                    mouseEvent = x_y_MouseEvent[2];
                    //Thread th = new Thread(new ThreadStart(DoMouseEvent));
                    DoMouseEvent();
                    //th.Start();
                }
                catch
                {
                    MessageBox.Show("手机已断开！");
                    button1.Enabled = true;
                    button2.Enabled = false;
                    pcServer.Dispose();
                    phoneClient.Dispose();
                    break;
                }
                /*getData = Encoding.UTF8.GetString(buffer, 0, dataSize);
                x_y_MouseEvent = getData.Split(',');
                D_valueX = Convert.ToInt32(x_y_MouseEvent[0]);
                D_valueY = Convert.ToInt32(x_y_MouseEvent[1]);
                mouseEvent = x_y_MouseEvent[2];
                count = 50;
                //Thread th = new Thread(new ThreadStart(timer1.Start));
                //th.Start();
                //timer1.Start();
                Thread th = new Thread(new ThreadStart(DoMouseEvent));
                th.Start();*/
            }
        }
        private void DoMouseEvent()
        {
            //lock (lockObject)
            //{
                switch (mouseEvent)
                {
                    case "WHEELUP":
                        int count_w = 20;
                        while (true)
                        {
                            Thread.Sleep(1);
                            if (count_w == 0)
                            {
                                return;
                            }
                            setptY = -50 / count_w;
                            count_w--;
                            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, setptY, 0);//setptY 为负代表向前，为正代表向后
                            //移动端只用发过来其Y坐标差
                            if (checkBox1.Checked)
                            {
                                textBox1.Text = String.Format("{0},{1}", 0, 0);
                            }
                        }
                    case "WHEELDOWN":
                        int count_d = 20;
                        while (true)
                        {
                            Thread.Sleep(1);
                            if (count_d == 0)
                            {
                                return;
                            }
                            setptY = 50 / count_d;
                            count_d--;
                            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, setptY, 0);//setptY 为负代表向前，为正代表向后
                            //移动端只用发过来其Y坐标差
                            if (checkBox1.Checked)
                            {
                                textBox1.Text = String.Format("{0},{1}", 0, 0);
                            }
                        }
                    case "MOVE":
                        int count = 10;
                        while (true)
                        {
                            //Thread.Sleep(1);
                            if (count == 0)
                            {
                                return;
                            }
                            setptX = D_valueX / count;
                            setptY = D_valueY / count;
                            count--;
                           // if (setptX < 1 && setptY < 1)
                              //  return;
                            mouse_event(MOUSEEVENTF_MOVE, setptX, setptY, 0, 0);
                            /*if (count == 0)
                            {
                                return;
                            }*/
                            if (checkBox1.Checked)
                            {
                                textBox1.Text = String.Format("{0},{1}", MousePosition.X, MousePosition.Y);
                            }
                        }
                        //break;
                    case "LEFT":
                        if (checkBox1.Checked)
                        {
                            textBox1.Text = String.Format("{0},{1}", MousePosition.X, MousePosition.Y);
                        }
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        break;
                    case "RIGHT":
                        if (checkBox1.Checked)
                        {
                            textBox1.Text = String.Format("{0},{1}", MousePosition.X, MousePosition.Y);
                        }
                        mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                        break;
                    case "BIG":
                        if (checkBox1.Checked)
                        {
                            textBox1.Text = String.Format("{0},{1}", MousePosition.X, MousePosition.Y);
                        }
                        keybd_event((byte)VK_CONTROL, 0, 0, 0);
                        keybd_event((byte)VK_ADD, 0, 0, 0);
                        keybd_event((byte)VK_ADD, 0, 2, 0);
                        keybd_event((byte)VK_CONTROL, 0, 2, 0);
                       
                        break;
                    case "SMALL":
                        if (checkBox1.Checked)
                        {
                            textBox1.Text = String.Format("{0},{1}", MousePosition.X, MousePosition.Y);
                        }
                        keybd_event((byte)VK_CONTROL, 0, 0, 0);
                        keybd_event((byte)VK_SUBTRACT, 0, 0, 0);
                        keybd_event((byte)VK_SUBTRACT, 0, 2, 0);
                        keybd_event((byte)VK_CONTROL, 0, 2, 0);
                       
                        break;
                    /*case "DOUBLE":
                        if (checkBox1.Checked)
                        {
                            textBox1.Text = String.Format("{0},{1}", MousePosition.X, MousePosition.Y);
                        }
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        break;*/
                //}
            }
        }


        /* private void button2_Click(object sender, EventArgs e)
         {
             if (phoneClient == null)
             {
                 MessageBox.Show("无手机连接");
                 return;
             }
             phoneClient.Close();
             MessageBox.Show("已经和手机断开连接！");
             button1.Enabled = true;
             button2.Enabled = false;
         }*/

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (phoneClient == null)
            {
                MessageBox.Show("无手机连接");
                button1.Enabled = true;
                button2.Enabled = true;
                return;
            }
            else
            {
                phoneClient.Close();
                MessageBox.Show("已经和手机断开连接！");
                button1.Enabled = true;
                button2.Enabled = false;
                pcServer.Dispose();
                phoneClient.Dispose();
            }
        }
    }
}
