#include<stdio.h>
void move(char x,int n,char y);
void hanoi(int n,char x,char y,char z);
int i=0;
int main(void)
{
	char x='X',y='Y',z='Z';
	int n;
	
	printf("ÇëÊäÈëËþÊý(n):\n");
	scanf("%d",&n);
	hanoi(n,x,y,z);
	printf("%d",i);
	return 0;
}
//n¸öÅÌ×Ó£¬´ÓxÅÌ°áµ½zÅÌ£¬½èÖúyÅÌ
void hanoi(int n,char x,char y,char z)
{
	if(n==1)
	{
		move(x,1,z);
		i++;
	}
	else
	{
		hanoi(n-1,x,z,y);
		move(x,n,z);
		i++;
		hanoi(n-1,y,x,z);
	}
}
void move(char x,int n,char y)
{
	printf("%c->%c\n",x,y);
}