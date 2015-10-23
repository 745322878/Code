#include<stdio.h>
#include<stdlib.h>
#include<string.h>
typedef struct
{
	char name[20];
	char sex;

}Person;  
typedef struct
{
	Person *front;
	Person *rear;
	int _number;
}Queue;                     
Person dancer[50];
int num;
int Mnumber;
int Fnumber;
Queue Mdancers,Fdancers;

int EmptyQueue(int number);
//判断是否为空队
int  InitQueue();
//队的初始化
int InQueue(Queue *q,char name[],char sex);
//入队
Person  OutQueue(Queue *q,int number);
//出队
void DancePartner(Person dancer[] ,int num);
//舞伴配对
int main(void )
{
	InitQueue();
	Mnumber=0;
	Fnumber=0;
	printf("请输入总人数num:\n");
	scanf("%d",&num);
	getchar();
	DancePartner(dancer,num);
	return 0;

}
int EmptyQueue(int  number)
{
	
	if(number==0)
		return 0;
	return 1;
}
int  InitQueue()
{

/*	q.rear=(Person *)malloc (sizeof(Person)*50);

	if(q.rear==0)
		return 0;
	q.front=q.rear;
	return 1;*/
	Mdancers.rear=(Person *) malloc (sizeof(Person)*50);
	Fdancers.rear=(Person *) malloc (sizeof(Person)*50);
	Mdancers.front=Mdancers.rear;
	Fdancers.front=Fdancers.rear;
	Mdancers._number=0;
	Fdancers._number=0;
	return 1;
}
//入队
int InQueue(Queue *q,char name[],char sex)
{
	
//	if(p->rear-p->front==0)
//		printf("队满");

	strcpy((*(*q).rear).name,name);


	(*(*q).rear).sex= sex;
	*(*q).rear++;
	return 1;
}
Person  OutQueue(Queue *q,int number)
{
	Person p;
	if(number==0)
	//if(*(*q)._number==0)
		exit(0);
	p=*(*q).front++;
	

	return  p;
	
}

void DancePartner(Person dancer[] ,int num)
{
	int i;

	Person p;
	Person q;

	//InitQueue(Fdancers);
	for(i=0;i<num;i++)
	{
		printf("请输入第%d个舞者的性别:\n",i+1);
		scanf("%c",&dancer[i].sex);
		getchar();
		printf("请输入第%d个舞者的姓名:\n",i+1);
		scanf("%s",dancer[i].name);
		getchar();
		p=dancer[i];

		if(p.sex=='F')
		{
			InQueue(&Fdancers,dancer[i].name,dancer[i].sex);
			Fnumber++;
		}
		else
		{
			InQueue(&Mdancers,dancer[i].name,dancer[i].sex);
			Mnumber++;
		}
	}
	while(EmptyQueue(Fnumber)==1 &&EmptyQueue(Mnumber)==1)
	{
		q=OutQueue(&Fdancers,Fnumber);
		Fnumber--;
		printf("%s\t",q.name);
		q=OutQueue(&Mdancers,Mnumber);
		Mnumber--;
		printf("%s\n",q.name);
	}
	if(EmptyQueue(Fnumber)==1)
	{
		q=OutQueue(&Fdancers,Fnumber);
		printf("%s will be the first to get a partner.\n",q.name );
	}
	else if(EmptyQueue(Mnumber)==1)
	{
		q=OutQueue(&Mdancers,Mnumber);
		printf("%s will be the first to get a partner.\n",q.name );
	}

}