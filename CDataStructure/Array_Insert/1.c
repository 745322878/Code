#include<stdio.h>
#include<string.h>
#define MAXSIZE 5
typedef int ElemType;
/*typedef struct 
{
	ElemType elem[MAXSIZE];
	int length;
}SeqList;
SeqList L;
//顺序表初始化
void init_SeqList(SeqList L1)
{
	L1.length=0;
	
}
//顺序表插入元素
int  Insert_SeqList(SeqList L1,int d,ElemType x)
{
	int j;
	if(L1.length==MAXSIZE-1)
	{
		printf("表满\n");
		return 0;
	}
	if(d<1||d>L1.length+1)
	{
		printf("位置错误\n");
		return 0;
	}
	else 
	{

		for(j=L1.length-1;j>=3;j--)
		{
			L1.elem[j+1]=L1.elem[j];
		}
		L1.elem[3]=x;
		L1.length=MAXSIZE+1;
		return 1;
	}	
}
int main()
{
	int i;

	init_SeqList(L);
	
	for(i=0;i<MAXSIZE;i++)
	{
		printf("请输入顺序表中第%d个值:\n",i+1);
		scanf("%d",&(L.elem[i]));
		getchar();
		L.length++;
	}
	//L.length=MAXSIZE;
	for(i=0;i<L.length;i++)
	{
		printf("%3d",L.elem[i]);
	}
	printf("\n");
	Insert_SeqList(L,2,6);

	for(i=0;i<L.length;i++)
	{
		printf("%3d",L.elem[i]);
	}
	printf("\n");
	return 0;
}*/
int main()
{
	int i;
	int a[5]={1,2,3,4,5};
	Insert(a,3,6);
	for(i=0;i<6;i++)
	{
		printf("%3d",a[i]);
	}
	printf("\n");
	return 0;


}
int Insert(int a[],int d,ElemType x)
{
		
		int j;
		for(j=5-1;j>=d;j--)
		{         
			a[j+1]=a[j];
		}
		a[d]=x;
		return 1;
		//l++;	
}