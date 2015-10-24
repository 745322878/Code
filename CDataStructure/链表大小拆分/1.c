#include<stdio.h>
#include<stdlib.h>
typedef struct node
{
	int  data;
	struct node *next;
}*LinkList,NodeType;
LinkList CreatList(int iCount); //����������
void PrintList(NodeType *pHead);//��ӡ������
void SplitList(NodeType *pHead);  //��ֵ�����
/*
ͷ�巨
LinkList CreatList(int iCount)
{
	int i;
	int data;
	LinkList pHead=(LinkList)malloc(sizeof(NodeType));
	NodeType *pNew=NULL;
	pHead->next=NULL;
	for(i=1;i<=iCount;i++)
	{
		printf("�������%d��data:\n",i);
		scanf("%d",&data);
		pNew=(LinkList)malloc(sizeof( NodeType));
		pNew->data=data;
		pNew->next=pHead->next;
		pHead->next=pNew;

	}
	return pHead;
}
*/
//β�巨
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
			printf("�������%d��data:\n",i);
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
		printf("��%d��Ԫ������:%d\n",i,pTemp->data);
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
	printf("ԭ����\n");
	PrintList(pHead);
	printf("\n");
	SplitList(pHead);
	printf("��ֵ�����\n");
	PrintList(pHead);
	return 0;

}
