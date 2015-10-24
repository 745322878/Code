#include<stdio.h>
#include<stdlib.h>

typedef struct Node
{
	float coef; //系数
	int expn;   //指数
	struct  Node *next;		//指针域
}NodeType;
NodeType *CreatList();
void PrintList(NodeType *pHead,char str[]);
NodeType *AddList(NodeType *aHead,NodeType *bHead);
NodeType *Delete_Zero(NodeType *cHead);
NodeType *SubList(NodeType *aHead,NodeType *bHead);
int main(void)
{
	NodeType *aHead,*bHead,*cHead;
	char operation;
	aHead=CreatList();
	bHead=CreatList();
	PrintList(aHead,"pa");
	PrintList(bHead,"pb");
	getchar();
	printf("请输入操作符（'+'或'-'）\n");
	scanf("%c",&operation);
	if(operation=='+')
	{
		printf("加法\npa+pb=") ;
		cHead=AddList(aHead,bHead);
	
	}
	else if(operation =='-')
	{
		printf("减法\npa-pb=");
		cHead=SubList(aHead,bHead);
	}
	PrintList(cHead,"pc");
	printf("\n");

	return 0;
}
NodeType *CreatList()
{
	NodeType *pHead,*pNew,*pTemp;
	float coef;
	int expn;
	pHead=(NodeType *)malloc(sizeof(struct Node));
	pHead->next=NULL;
	pTemp=pHead;
	printf("请输入一元多项式的系数(输入0结束):\n");
	scanf("%f%d",&coef,&expn);
	getchar();
	while(coef!=0)
	{
		pNew=(NodeType *)malloc(sizeof(struct Node));
		pNew->coef=coef;
		pNew->expn=expn;
		pTemp->next=pNew;
		pTemp=pNew;
		scanf("%f%d",&coef,&expn);
	}
	pTemp->next=NULL;
	return pHead;
}
void PrintList(NodeType *pHead,char str[])
{
	NodeType *pTemp;
	pTemp=pHead->next ;
	printf("%s=",str);
	while(pTemp!=NULL)
	{
		if(pTemp->expn==0)
		{
			printf("%2.2f+",pTemp->coef);
		}
		else
		{
				printf("%2.2fX^%d",pTemp->coef,pTemp->expn);
				if(pTemp->next!=NULL)
				{
					printf("+");
				}
		}
		//printf("%f %d\n",pTemp->coef,pTemp->expn);
		pTemp=pTemp->next;
	}
	printf("\n");

}
NodeType *AddList(NodeType *aHead,NodeType *bHead)
{
	NodeType *aTemp,*bTemp,*cTemp,*cHead;
	cHead=(NodeType *)malloc(sizeof(struct Node));

	cTemp=cHead;
	aTemp=aHead->next;
	bTemp=bHead->next ;
	while(aTemp!=NULL&&bTemp!=NULL)
	{
		if(aTemp->expn < bTemp->expn)
		{
			cTemp->next=aTemp;
			cTemp=aTemp;
			aTemp=aTemp->next ;
			
		}
		else if(aTemp->expn > bTemp->expn)
		{
			cTemp->next=bTemp;
			cTemp=bTemp;
			bTemp=bTemp->next ;
		}
		else if(aTemp->expn==bTemp->expn)
		{
			aTemp->coef=aTemp->coef+bTemp->coef;
			cTemp->next=aTemp;
			cTemp=aTemp;
			aTemp=aTemp->next ;
			bTemp=bTemp->next ;
		}
	}
	if(aTemp==NULL)
	{
		cTemp->next=bTemp;
	}
	else if(bTemp==NULL)
	{
		cTemp->next=aTemp;
	}	
	return cHead;
}
NodeType *Delete_Zero(NodeType *cHead)
{
	NodeType *cTemp,*cPre;
	cPre=cHead;
	cTemp=cHead->next ;
	while(cTemp!=NULL)
	{
		if(cTemp->coef==0)
		{
			cPre->next=cTemp->next;
			free(cTemp);
			cTemp=cPre->next ;
		}
		else
		{
			cPre=cTemp;
			cTemp=cTemp->next;
		}
	}
	return cHead;
}
NodeType *SubList(NodeType *aHead,NodeType *bHead)
{
	NodeType *bTemp,*cHead;
	bTemp=bHead->next;
	cHead=(NodeType *)malloc (sizeof(struct Node));
	while(bTemp!=NULL)
	{
		bTemp->coef=bTemp->coef*(-1);
		bTemp=bTemp->next ;
	}
	cHead=AddList(aHead,bHead);
	return cHead;
}