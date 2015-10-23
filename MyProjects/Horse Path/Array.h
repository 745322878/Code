#define MAXSIZE 100
#define N 8
int weight[N][N];
int Board [N][N][8];

typedef struct		//λ��
{
	int x;		//������
	int y;		//������
}PosType;
typedef struct		//ջ��Ԫ��
{
	int ord;			//�������ߵĲ���
	PosType seat;		//��
	int di;				//��ķ���
}ElemType;
typedef struct				//����ջ
{
	ElemType  *base;		//ջ��ָ��
	ElemType  *top;			//ջ��ָ��
}SeqStack;
int InitStack();//��ʼ��ջ
ElemType GetTop();	//�õ�ջ��Ԫ��
void Push(ElemType elem);//��ջ
//void Pop(ElemType *elem);//��ջ
void Pop();
int StackEmpty();//�ж�ջ�Ƿ�Ϊ��
int Pass(PosType curpos); //�жϵ�ǰλ���Ƿ�Ϸ�
PosType NextPos(PosType curpos,int direction);//8����ѡ����
void setweight();  //�����Ȩֵ
void setmap();//�����8������Ȩֵ��������
int  HorsePath(PosType start); //���߹���·��
void OutputPath(); //������߹���·��
SeqStack s;

//��ʼ��
int InitStack()
{
	s.base=malloc(sizeof(ElemType));
	if(s.base==0)
		return 0;
	s.top=s.base;
	return 1;
}

//�õ�ջ��Ԫ��
ElemType GetTop()
{
	if(s.top==s.base)
		exit(0);
	return *(s.top-1);
}
//��ջ
void Push(ElemType elem)
{
	*s.top++=elem;
	//s->top++;
	//s->Elem[s->top]=elem;
}
//��ջ
void Pop()
{
	//��ջ
	if(s.base==s.top) ;
	else
		--s.top;
}
//�ж��Ƿ�Ϊ��ջ
int StackEmpty()
{
	if(s.top==s.base  )
		return 1;
	else
		return 0;
}

//�жϵ�ǰλ���Ƿ�Ϸ�
int Pass(PosType curpos)	
{
	SeqStack s1=s;
	//����
	if(curpos.x<0||curpos.x>(N-1)||curpos.y<0||curpos.y>(N-1))
		return 0;
	for(;s1.top!=s1.base;)
	{
		s1.top--;
		//���߹�
		if(curpos.x==(*s1.top).seat.x&&curpos.y==(*s1.top).seat.y)
			return 0;
	}
	return 1;
}
//8����ѡ���򣬷���һ��λ��
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
//�����Ȩֵ
void setweight()
{
	int i,j,k;
	PosType m;
	//�õ�
	ElemType elem;
	//ջ��Ԫ��
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
					//(i,j)���м�����������ƶ�
				}
			}
		}
	}
}
//�������8������Ȩֵ��������
void setmap()
{
	int a[8];
	int i,j,k,m,min,s,h;
	PosType n1,n2;
	//n2��ǰλ�ã�n1��һ��λ��
	for(i=0;i<N;i++)
	{
		for(j=0;j<N;j++)
		{
			for(h=0;h<8;h++) //������a[8]��¼��ǰλ�õ���һ��λ�õĿ���·��������
			{
				n2.x=i;
				n2.y=j;
				//���ϣ����ϣ����£����£�8������һ��ѭ��һ������
				n1=NextPos(n2,h+1);
					//��һλ�ÿ���
				if(n1.x>=0&&n1.x<N && n1.y>=0&&n1.y<N)
				{
					a[h]=weight[n1.x][n1.y];	
				}
				else 
					a[h]=0;
			}
			//��ÿ���������� Ȩֵ �������д���Board[N][N][8],���ܵ���ķ����������
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
		//��ǰλ�úϷ�
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
			curpos=NextPos(elem.seat,off);//ȡ����һ�������
			
		}
		//λ�ò��Ϸ�
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
	


	printf("�߲�ͨ");
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

