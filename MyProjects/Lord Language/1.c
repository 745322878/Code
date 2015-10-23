#include<stdio.h>
#include<stdlib.h>
#include<string.h>
#define MAXSIZE 50
typedef struct 
{
	char data[MAXSIZE];
	int top;
}SeqStack;
typedef struct 
{
	char data[MAXSIZE];
	int rear,front;
}Queue;
SeqStack *s1;
SeqStack *sAB;
Queue *q1;
SeqStack *Init_Stack(SeqStack *s);				//��ʼ��ջ
int Empty_Stack(SeqStack *s);					//��ջ
int Push_Stack(SeqStack *s,char ch);		//��ջ
Pop_Stack(SeqStack *s,char *ch);			//��ջ
void Get_emptyStack(SeqStack *s);			//�ÿ�ջ
Queue *Init_Queue(Queue *q);						//��ʼ������
int Empty_Queue(Queue *q);					//�ն�
int Push_Queue(Queue *q,char ch);			//���
int Pop_Queue(Queue *q,char *ch);				//����
void Get(char ch[]);					//�õ��ַ���
void Translation_A();					//����A
void Translation_B();					//����B
void Translation_Barcket(SeqStack *s);		//������������
int main(void)
{
	char ch[50];
	char letter;
	s1=Init_Stack(s1);
	//sAB=Init_Stack(sAB);
	q1=Init_Queue(q1);
	printf("������ħ�����ԣ�\n");
	gets(ch);
	Get(ch);
	/*while(Empty_Stack(s1)==0)
	{
		Pop_Stack(s1,&letter);
		printf("%c\n",letter);
	}*/
	while(Empty_Queue(q1)==0)
	{
		Pop_Queue(q1,&letter);
		printf("%2c",letter);
	}
	printf("\n");
	return 0;
}
SeqStack *Init_Stack(SeqStack *s)
{
	 s=malloc(sizeof(SeqStack));
	 s->top=-1;
	 return s;
}
int Empty_Stack(SeqStack *s)
{
	if(s->top==-1)
		return 1;
	else 
		return 0;
}
int Push_Stack(SeqStack *s,char ch)
{
	s->data[++s->top]=ch;
	return 1;
}
int Pop_Stack(SeqStack *s,char *ch)
{
	if(Empty_Stack(s)==1)//��ջ
		return 0;
	else
	{
		*ch=s->data[s->top--];
		return 1;
	}
}
void Get_emptyStack(SeqStack *s)
{
	int i;
	char letter;
	for(i=s->top;i>=0;i--)
	{
		Pop_Stack(s,&letter);
	}
}
Queue *Init_Queue(Queue *q)
{
	q=malloc(sizeof(Queue));
	q->front=q->rear=-1;
	return q;
}
int Empty_Queue(Queue *q)				//�ն�
{
	if(q->front==q->rear)
		return 1;
	else 
		return 0;
}
 
int Push_Queue(Queue *q,char ch)			//���
{
	q->data[++q->rear]=ch;
	return 1;
}
int Pop_Queue(Queue *q,char *ch)
{
	if(Empty_Queue(q)) //�ն�
		return 0;
	q->front++;
	*ch=q->data[q->front];
	return 1;
}
void Get(char ch[])
{
	int i;
	int flag=0;
	for(i=0;ch[i]!='\0';i++)
	{
		if(ch[i]=='(')
		{
			do
			{
				if(ch[i]=='(')
					flag++;
				else if(ch[i]==')')
					flag--;
				Push_Stack(s1,ch[i]);
				i++;
				
				
			}while(flag!=0);
			i--;
			Translation_Barcket(s1);
		}
		else if(ch[i]=='A'||ch[i]=='B') 
		{
			if(ch[i]=='A')
				Translation_A();
			else
				Translation_B();
			//Push_Stack(sAB,ch[i]);
		}
		else if(ch[i]!='A'&&ch[i]!='B'&&ch[i]!='(')
		{
			Push_Queue(q1,ch[i]);
		//	Push_Stack(s1,ch[i]);
		}
	}
}    
void Translation_A()
{
	char ch[3]="sae";
	int i;
	for(i=0;i<3;i++)
	{
		Push_Queue(q1,ch[i]);
	}
}
void Translation_B()
{
	char ch[4]="tAdA";
	int i;
	for(i=0;i<4;i++)
	{
		if(ch[i]=='A')
		{
			Translation_A();
		}
		else 
			Push_Queue(q1,ch[i]);
	}
}

void Translation_Barcket(SeqStack *s)
{
	char letter;
	char ch;
	ch=s->data[1];
	Pop_Stack(s,&letter);
	while(s1->top!=1)
	{
		Push_Queue(q1,ch);
		Pop_Stack(s,&letter);
		Push_Queue(q1,letter);
	}
	Get_emptyStack(s);
	Push_Queue(q1,ch);
}


