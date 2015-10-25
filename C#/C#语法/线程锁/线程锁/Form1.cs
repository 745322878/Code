using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace 线程锁
{
    public partial class Form1 : Form
    {
        private object lockObject = new object();
        int n = 50;
        public Form1()
        {
            InitializeComponent();
        }

        private void 开始售票_Click(object sender, EventArgs e)
        {
            Thread[] ths = new Thread[15];
            for (int i = 0; i < 15; i++)
            {
                ths[i] = new Thread(new ThreadStart(Sell));
            }
            foreach(Thread a in ths)
            { a.Start(); }
        }
        private void Sell()
        {
            string myString;
            lock (lockObject)//加锁
            {
                while (n > 0)
                {
                    Thread.Sleep(300);
                    n--;
                    this.Invoke(new Action(() =>
                        {
                            myString = string.Format("当前还剩{0}张票！", n.ToString());
                            listBox1.Items.Add(myString);
                        }
                        ));
                }
           }
        }
    }
}
