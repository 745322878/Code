#include<stdio.h>
#include<stdlib.h>
#define Max 100


typedef struct NodeType
{
	int id;
	int password;
	struct NodeType *next;
}NodeType;

NodeType *CreatList(int n);//创建单向循环链表
//void CreatList(int n);
void PrintList(NodeType *phead);//打印循环链表
void JosephusOperate(NodeType *phead,int m); //运行“约瑟夫环”问题

int main(void )
{
	int m;
	int n;
	NodeType *phead;
	do
	{
		printf("请输入人数n(n<=100):\n");
		scanf("%d",&n);
	}while(n>100);
	phead=CreatList(n);
	PrintList(phead);
	printf("请输入初始密码m:\n");
	scanf("%d",&m);
	JosephusOperate(phead,m);
	return 0;
}
//void CreatList(int n)
NodeType *CreatList(int n)
{
	int i;
	int password;
	NodeType *phead=NULL;
	//int m=6;
	NodeType *pNew=NULL;
	NodeType *pCur=NULL;


	
	for( i=1;i<n+1;i++)
	{
		printf("输入第%d个人的密码：",i);
		scanf("%d",&password);
		pNew=(NodeType *)malloc(sizeof(struct NodeType));
		pNew->id=i;
		pNew->password=password;
		pNew->next=NULL;
		if(phead==NULL)
		{
			phead=pCur=pNew;
			pCur->next=phead;
		}
		else
		{
			pNew->next=pCur->next;
			pCur->next=pNew;
			pCur=pNew;
		
		}
	}
	printf("完成单项链表的创建！！！\n");
	return phead;	
}

void PrintList(NodeType *phead)
{
	NodeType *ptemp=NULL;
	ptemp=phead;
	if(ptemp!=NULL)//链表为空
	{
		printf("--ID---Password\n");
		do
		{
			printf("%3d %7d\n",ptemp->id,ptemp->password);
			ptemp=ptemp->next;
		}while(ptemp!=phead);
	}
}
void JosephusOperate(NodeType *phead,int m)
{
	int count;
	
	int Flag=1;
	NodeType *pPrv=NULL;
	NodeType *pNew1=NULL;
	NodeType *pDel=NULL; 

		pPrv=pNew1=phead;
	//找到当前节点的前一个节点
	while(pPrv->next!=phead)
	{
		pPrv=pPrv->next;
	}
	while(Flag==1)
	{
		   for(count=1;count<m;count++)
		   {
				pPrv=pNew1;
				pNew1=pNew1->next;
		   }
		   if(pNew1==pPrv)
			   Flag=0;
		   pDel=pNew1;
		   pPrv->next=pNew1->next;
		   pNew1=pNew1->next;
		   m=pDel->password;
		   printf("第%d个人出列，密码为%d\n",pDel->id,pDel->password);
		   free(pDel);

	}
	phead=NULL;
	getchar();
}