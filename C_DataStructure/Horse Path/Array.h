#define MAXSIZE 100
#define N 8
int weight[N][N];
int Board [N][N][8];

typedef struct		//位置
{
	int x;		//横坐标
	int y;		//纵坐标
}PosType;
typedef struct		//栈的元素
{
	int ord;			//定义马走的步数
	PosType seat;		//点
	int di;				//马的方向
}ElemType;
typedef struct				//定义栈
{
	ElemType  *base;		//栈底指针
	ElemType  *top;			//栈顶指针
}SeqStack;
int InitStack();//初始化栈
ElemType GetTop();	//得到栈顶元素
void Push(ElemType elem);//入栈
//void Pop(ElemType *elem);//出栈
void Pop();
int StackEmpty();//判断栈是否为空
int Pass(PosType curpos); //判断当前位置是否合法
PosType NextPos(PosType curpos,int direction);//8个候选方向
void setweight();  //求各点权值
void setmap();//各点的8个方向按权值递增排列
int  HorsePath(PosType start); //马走过的路径
void OutputPath(); //输出马走过的路径
SeqStack s;

//初始化
int InitStack()
{
	s.base=malloc(sizeof(ElemType));
	if(s.base==0)
		return 0;
	s.top=s.base;
	return 1;
}

//得到栈顶元素
ElemType GetTop()
{
	if(s.top==s.base)
		exit(0);
	return *(s.top-1);
}
//入栈
void Push(ElemType elem)
{
	*s.top++=elem;
	//s->top++;
	//s->Elem[s->top]=elem;
}
//出栈
void Pop()
{
	//空栈
	if(s.base==s.top) ;
	else
		--s.top;
}
//判断是否为空栈
int StackEmpty()
{
	if(s.top==s.base  )
		return 1;
	else
		return 0;
}

//判断当前位置是否合法
int Pass(PosType curpos)	
{
	SeqStack s1=s;
	//出界
	if(curpos.x<0||curpos.x>(N-1)||curpos.y<0||curpos.y>(N-1))
		return 0;
	for(;s1.top!=s1.base;)
	{
		s1.top--;
		//已走过
		if(curpos.x==(*s1.top).seat.x&&curpos.y==(*s1.top).seat.y)
			return 0;
	}
	return 1;
}
//8个候选方向，返回一个位置
PosType NextPos(PosType curpos,int direction)
{
	switch(direction)
	{
		case 1:curpos.x-=1;curpos.y+=2;break;
		case 2:curpos.x+=1;curpos.y+=2;break;
		case 3:curpos.x-=1;curpos.y-=2;break;
		case 4:curpos.x+=1;curpos.y-=2;break;
		case 5:curpos.x-=2;curpos.y+=1;break;
		case 6:curpos.x+=2;curpos.y+=1;break;
		case 7:curpos.x-=2;curpos.y-=1;break;
		case 8:curpos.x+=2;curpos.y-=1;break;
	}
	return curpos;
}
//求各点权值
void setweight()
{
	int i,j,k;
	PosType m;
	//该点
	ElemType elem;
	//栈的元素
	for(i=0;i<N;i++)
	{
		for(j=0;j<N;j++)
		{
		
			elem.seat.x=i;
			elem.seat.y=j;
			weight[i][j]=0;
			for(k=0;k<8;k++)
			{
				m=NextPos(elem.seat,k+1);
				if(m.x>=0&&m.x<N && m.y>=0&&m.y<N)
				{
					weight[i][j]++;
					//(i,j)点有几个方向可以移动
				}
			}
		}
	}
}
//将各点的8个方向按权值递增排列
void setmap()
{
	int a[8];
	int i,j,k,m,min,s,h;
	PosType n1,n2;
	//n2当前位置，n1下一个位置
	for(i=0;i<N;i++)
	{
		for(j=0;j<N;j++)
		{
			for(h=0;h<8;h++) //用数组a[8]记录当前位置的下一个位置的可行路径的条数
			{
				n2.x=i;
				n2.y=j;
				//左上，右上，左下，右下，8个方向，一次循环一个方向
				n1=NextPos(n2,h+1);
					//下一位置可行
				if(n1.x>=0&&n1.x<N && n1.y>=0&&n1.y<N)
				{
					a[h]=weight[n1.x][n1.y];	
				}
				else 
					a[h]=0;
			}
			//对每个方向索引 权值 升序排列存入Board[N][N][8],不能到达的方向排在最后
			for(m=0;m<8;m++)
			{
				min=9;
				for(k=0;k<8;k++)
				{
					if(min>a[k])
					{
						min=a[k];
						Board[i][j][m]=k;
						s=k;
					}
				
				}
				a[s]=9;
			}
		}
	}
}
int  HorsePath(PosType start)
{
	PosType curpos;
	int horsestep=0,off;
	ElemType elem;
	curpos=start;
	do
	{
		//当前位置合法
		if(Pass(curpos))
		{
			horsestep++;
			elem.di=0;
			elem.ord=horsestep;
			elem.seat=curpos;
			Push(elem);
			if(N*N ==horsestep)
				return 1;
			off=Board[elem.seat.x][elem.seat.y][elem.di]+1;
			curpos=NextPos(elem.seat,off);//取得下一个坐标点
			
		}
		//位置不合法
		else {
            if(StackEmpty()==0){
                while (!StackEmpty() && elem.di == 8) {
                    Pop();
                    if (!StackEmpty()) {
                        elem = GetTop();
                        horsestep = elem.ord;
                    }
                }
                if (!StackEmpty() && elem.di < 8) {
                    Pop();
                    off = Board[elem.seat.x][elem.seat.y][++elem.di];
                    curpos = NextPos(elem.seat, off + 1);
                    Push(elem);
                }
            }
        }
    } while (StackEmpty()==0);
	


	printf("走不通");
	return 0;

}
void OutputPath()
{
	int i,j,k,horsestep=1;
	SeqStack s1=s;
	int path[N][N];
	for(i=0;s1.top!=s1.base;i++)
	{
		path[(*s1.base ).seat.x][(*s1.base ).seat.y]=i+1;
	
		++s1.base;
		
	}
	printf("\n");
	/*while(horsestep!=64)
	{*/
		for( j=0;j<N;j++)
		{
			printf("\n");
			for(k=0;k<N;k++)
			{
				/*if(path[j][k]==horsestep)
				{
					printf("\t%d",path[j][k]);
					horsestep++;
					Sleep(1);
				}*/
					printf("\t%d",path[j][k]);
			}
		
		}
//	}
}

