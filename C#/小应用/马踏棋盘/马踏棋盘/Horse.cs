using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace 马踏棋盘
{


    public struct PosType	//位置
    {
        int x;		//横坐标
        int y;		//纵坐标
    }
    public struct ElemType		//栈的元素
    {
        int ord;			//定义马走的步数
        PosType seat;		//点
        int di;				//马的方向
    }
    public struct SeqStack		//定义栈
    {
      public ElemType bas;			//底指针
     public    ElemType top;			//栈顶指针
    }
    class Horse
    {
        SeqStack s;
       public int  InitStack()
        {
            s = new SeqStack();
            s.bas = new ElemType();
            if (s.bas) 
                return ;
           else
            //if(!s.base)
            //     ;
            //s.top=s.base ;

        }

    }
}
