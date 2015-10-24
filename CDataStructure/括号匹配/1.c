#include<stdio.h>
#include<stdlib.h>
#define MAXSIZE 50
typedef struct 
{
	char data[MAXSIZE];
	int top;
}SeqStack;

//³õÊ¼»¯Õ»
SeqStack *Inint_SeqStack();
//ÈëÕ»
void Push_SeqStack(SeqStack *s,char ch);
//³öÕ»
char Pop_SeqStack(SeqStack *s);
//À¨ºÅÆ¥Åä
int  Matching(SeqStack *s,char str[]);
int main(void)
{
	char str[MAXSIZE];
	int result;
	SeqStack *s;
	s=Inint_SeqStack();
	printf("ÇëÊäÈëÆ¥ÅäµÄ×Ö·û´®:\n");
	gets(str);
	result=Matching(s,str);
	if(result==1)
		printf("À¨ºÅÆ¥Åä£¡£¡£¡\n");
	else 
		printf("À¨ºÅ²»Æ¥Åä£¡£¡£¡\n");
	return 0;
}
//³õÊ¼»¯Õ»
SeqStack *Inint_SeqStack()
{
	SeqStack *s;
	s=malloc(sizeof(SeqStack));
	s->top=-1;
	return s;
}
//ÈëÕ»
void Push_SeqStack(SeqStack *s,char ch)
{
	if(s->top==MAXSIZE-1)
		 ;
	else
	{
		s->top++;
		s->data[s->top]=ch;
	}
}
//³öÕ»
char Pop_SeqStack(SeqStack *s)
{
	char ch;
	//¿ÕÕ»
	if(s->top==-1)
		;
	else
	{
		ch=s->data[s->top];
		s->top--;
		return ch;
	}
	
}
int  Matching(SeqStack *s,char str[])
{
	int i=0;
	char ch;
	while(str[i]!='\0')
	{
		if(str[i]=='('||str[i]=='[')
		{
			Push_SeqStack(s,str[i]);
			i++;
		}
		else if(str[i]==')'||str[i]==']')
		{
			//¿ÕÕ»
			if(s->top==-1)
				return 0;
			ch=Pop_SeqStack(s);
			if(ch=='('&&str[i]==')'  || ch=='['&&str[i]==']')
			{
				i++;
			}
			else
				return 0;
		}
	}
	if(s->top==-1)
		return 1;
}