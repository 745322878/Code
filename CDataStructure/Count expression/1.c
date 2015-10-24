#include<stdio.h>
#include<stdlib.h>
typedef struct 
{
	char data[50];
	int top;
}Stack;
Stack OPRD;//������ջ
Stack OPTR;//�����ջ
char Opset[7]={'+','-','*','/','(',')','#'};
int Init_Stack();//��ʼ��ջ
int Push_Stack(Stack *s,char ch);//��ջ
int Empty_Stack(Stack *s);
int Pop_Stack(Stack *s,char *ch);//��ջ
char GetTop_Stack(Stack *s);//�õ�ջ��Ԫ��
int IsOpset(char ch);//�Ƿ��������
char Compare(char ch1,char ch2);//�Ƚ���������ȼ�,��ջ��Ԫ�أ��ٶ�ȡ�������
char Calculation();//����

int main(void)
{
	char val;
	val=Calculation();
	printf("\n%d\n",val);
	return 0;

}
int Init_Stack(Stack *s)
{
	s=(Stack *)malloc(sizeof(Stack));
	s->top=-1;
	return 1;
}
int Push_Stack(Stack *s,char ch)
{
	s->data[++s->top]=ch;
	return 1;
}
int Empty_Stack(Stack *s)
{
	if(s->top==-1)
	{
		return 0;
	}
	return 1;
}
int  Pop_Stack(Stack *s,char *ch)
{

	*ch=s->data[s->top--];
	return 1;
}
char GetTop_Stack(Stack *s)
{
	return s->data[s->top];
}
int IsOpset(char ch)
{
	int i=0;
	for(i=0;i<7;i++)
	{
		if(ch==Opset[i])
			return 1;
	}
	return 0;
}
//�Ƚ���������ȼ�,��ջ��Ԫ�أ��ٶ�ȡ�������
char  Compare(char ch1,char ch2)
{
	if(ch1=='+' ||ch1=='-')
	{
		if(ch2=='+'||ch2=='-'||ch2==')'||ch2=='#')
			return '>';
		return '<';
	}
	else if(ch1=='*'||ch1=='/')
	{
		if(ch2=='(')
			return '<';
		return '>';
	}
	else if(ch1=='(')
	{
		if(ch2==')')
			return '=';
		return '<';
	}
	else if(ch1==')')
	{
		return '>';
	}
	else if(ch1=='#')
	{
		if(ch2=='#')
			return '=';
		return '<';
	}

}
char Calculation()
{
	char ch;
	char op;
	char num1,num2;
	char data;
	char val;
	Init_Stack(&OPRD);
	Init_Stack(&OPTR);
	Push_Stack(&OPTR,'#');
	printf("\nPlease input an expression:");
	ch=getchar();
	while(ch!='#'||GetTop_Stack(&OPRD)!='#')
	{
		if(IsOpset(ch)==0 && ch>='0'&&ch<='9') //ch�ǲ�����
		{
			data=ch-'0';
			//ch-0��ASCII����Ǹ�����
			ch=getchar();
			while(IsOpset(ch)==0 && ch>='0'&&ch<='9')	//ch�ǲ�����,��ֹ�س��ȷ������Ҳ�����ֵķ�������
			{
				data=data*10+ch-'0';
				ch=getchar();
			}
			Push_Stack(&OPRD,data);
		}
		else if(IsOpset(ch)==1)
		{
			switch(Compare(GetTop_Stack(&OPTR),ch))
			{
				case '<':
						Push_Stack(&OPTR,ch);
						ch=getchar();
						break;
				case'=':
						Pop_Stack(&OPTR,&op);
						if(op=='#')
							goto loop;	
						ch=getchar();
						break;
				case '>':
						Pop_Stack(&OPTR,&op);
						Pop_Stack(&OPRD,&num1);
						Pop_Stack(&OPRD,&num2);
						switch (op)
						{
							case '*': val=num1*num2; break;
							case '/': val=num1/num2; break;
							case '+': val=num1+num2; break;
							case '-': val=num1-num2; break;
						}
						Push_Stack(&OPRD,val);
						break;
			}
		}
		else
		{
			ch=getchar();
		}
	}
	loop:val=GetTop_Stack(&OPRD);
	return val;


}
