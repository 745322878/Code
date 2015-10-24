#include<stdio.h>
#include<stdlib.h>
#define Max 100


typedef struct NodeType
{
	int id;
	int password;
	struct NodeType *next;
}NodeType;

NodeType *CreatList(int n);//��������ѭ������
//void CreatList(int n);
void PrintList(NodeType *phead);//��ӡѭ������
void JosephusOperate(NodeType *phead,int m); //���С�Լɪ�򻷡�����

int main(void )
{
	int m;
	int n;
	NodeType *phead;
	do
	{
		printf("����������n(n<=100):\n");
		scanf("%d",&n);
	}while(n>100);
	phead=CreatList(n);
	PrintList(phead);
	printf("�������ʼ����m:\n");
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
		printf("�����%d���˵����룺",i);
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
	printf("��ɵ�������Ĵ���������\n");
	return phead;	
}

void PrintList(NodeType *phead)
{
	NodeType *ptemp=NULL;
	ptemp=phead;
	if(ptemp!=NULL)//����Ϊ��
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
	//�ҵ���ǰ�ڵ��ǰһ���ڵ�
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
		   printf("��%d���˳��У�����Ϊ%d\n",pDel->id,pDel->password);
		   free(pDel);

	}
	phead=NULL;
	getchar();
}