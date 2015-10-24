#include<stdio.h>
#include<stdlib.h>
typedef struct node
{
	int  data;
	struct node *next;
}*LinkList,NodeType;
LinkList CreatList(int iCount); //创建单链表
void PrintList(NodeType *pHead);//打印单链表
void SplitList(NodeType *pHead);  //拆分单链表
/*
头插法
LinkList CreatList(int iCount)
{
	int i;
	int data;
	LinkList pHead=(LinkList)malloc(sizeof(NodeType));
	NodeType *pNew=NULL;
	pHead->next=NULL;
	for(i=1;i<=iCount;i++)
	{
		printf("请输入第%d个data:\n",i);
		scanf("%d",&data);
		pNew=(LinkList)malloc(sizeof( NodeType));
		pNew->data=data;
		pNew->next=pHead->next;
		pHead->next=pNew;

	}
	return pHead;
}
*/
//尾插法
LinkList CreatList(int iCount)
{
	int i;
	int data;
	LinkList pHead=(LinkList)malloc(sizeof(NodeType));
	NodeType *pNew=NULL;
	NodeType *pTemp=NULL;
	pHead->next=NULL;
	pNew=pTemp=pHead;
	for(i=1;i<=iCount;i++)
	{
			printf("请输入第%d个data:\n",i);
		scanf("%d",&data);
		pNew=(LinkList)malloc(sizeof(NodeType));
		pNew->data=data;
		pNew->next=pTemp->next;
		pTemp->next=pNew;
		pTemp=pNew;
	}
	return pHead;
				
	
}
void PrintList(NodeType *pHead)
{
	int i=1;
	NodeType *pTemp=pHead->next;
	while(pTemp!=NULL)
	{
		printf("第%d个元素数据:%d\n",i,pTemp->data);
		i++;
		pTemp=pTemp->next;
	}

} 
void SplitList(NodeType *pHead)
{
	NodeType *pTemp,*q,*r;
	NodeType *pPrv=NULL;
	r=pTemp=pHead->next;
	pPrv=pHead;       
	while(pTemp->next!=NULL)
	{ 
		                                                           
		q=pTemp->next ;
		if(q->data<r->data)
		{
			pTemp->next=q->next;
			pPrv->next=q;
			q->next=r;
			
			
			pPrv=q;
	
			                                                                                                                                                                                                			
		}
		else
		{
					pTemp=pTemp->next;                                                                                                                
		}
	}
	
	
}
int main(void)
{
	LinkList pHead=NULL;
	pHead=CreatList(5);
	printf("原链表：\n");
	PrintList(pHead);
	printf("\n");
	SplitList(pHead);
	printf("拆分单链表\n");
	PrintList(pHead);
	return 0;

}
