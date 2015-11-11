#include<stdio.h> 
#include<stdlib.h>
#include<string.h>
#define N 30
#define M 2*N-1
#define MAXWEIGHT 1000
typedef char * HuffmanCode[N];
typedef struct 
{
	int weight;
	int parent;
	int Lchild;
	int Rchild;
}HTNode,HuffmanTree[M+1];
HuffmanTree ht;
HuffmanCode hc;
void select(HuffmanTree ht,int n,int* s1,int* s2);
void CreateHuffmanTree(HuffmanTree ht,int w[],int n);
void CreateHuffmanCode1(HuffmanTree ht ,HuffmanCode hc,int n);

int main()
{
	char ch[N];
	char ch1[N]="";
	int w[N];
	int l,i,l1,j,flag=0;
	for(i=1;i<N;i++)
	{
		w[i]=0;
	}
	printf("请输入传输的数据:\n");
	gets(ch);
	l=strlen(ch);
	for(i=0;i<l;i++)
	{
		l1=strlen(ch1);
		for(j=0;j<l1;j++)
		{
			if(ch1[j]==ch[i])
			{
				flag=1;
				break;
			}
			else 
				flag=0;
		}
		if(flag==0)
			ch1[j]=ch[i];
		w[j+1]++;
	}
	l1=strlen(ch1);
	CreateHuffmanTree(ht,w,l1);
	printf("三叉链表示意图：\n");
	for(i=1;i<2*l1;i++)
	{
		printf("%5d%5d%5d%5d\n",ht[i].weight,ht[i].parent,ht[i].Lchild,ht[i].Rchild);
	}
	printf("\n");
	CreateHuffmanCode1(ht,hc,l1);
	printf("Huffman编码:\n");
	for(i=1;i<=l1;i++)
	{
		printf("%c: ",ch1[i-1]);
		printf("%s\n",hc[i]);
	}
	for(i=0;i<l;i++)
		printf("%c",ch[i]);
	printf("压缩为:\n");
	for(i=0;i<l;i++)
	{
		for(j=0;j<l1;j++)
		{
			if(ch[i]==ch1[j])
			{
				printf("%s",hc[j+1]);
				break;
			}
		}
	}
	printf("\n");
	return 0;
}
void select(HuffmanTree ht,int n,int* s1,int* s2)
{
	int j;
	int m1,m2;
	m1=m2=MAXWEIGHT;
	*s1=*s2=1;
	for(j=1;j<=n;j++)
	{
		if(ht[j].parent==0 &&ht[j].weight<m1)
		{
			m2=m1;
			*s2=*s1;
			m1=ht[j].weight;
			*s1=j;
		}
		else
		{
			m2=ht[j].weight;
			*s2=j;
		}
	}
}
void CreateHuffmanTree(HuffmanTree ht,int w[],int n)
{
	int i;
	int m;
	int s1,s2;
	m=2*n-1;
	for(i=1;i<=n;i++)
	{
		ht[i].weight=w[i];
		ht[i].Lchild=0;
		ht[i].parent=0;
		ht[i].Rchild=0;
	}
	for(i=n+1;i<=m;i++)
	{
		ht[i].weight=0;
		ht[i].Lchild=0;
		ht[i].parent=0;
		ht[i].Rchild=0;
	}
	for(i=n+1;i<=m;i++)
	{
		select(ht,i-1,&s1,&s2);
		ht[i].weight=ht[s1].weight+ht[s2].weight;
		ht[i].Lchild=s1;
		ht[i].Rchild=s2;
		ht[s1].parent=i;
		ht[s2].parent=i;
	}
} 
void CreateHuffmanCode1(HuffmanTree ht,HuffmanCode hc, int n)
{
	//c为孩子，p为c的双亲结点
	char *cd;
	int c,p,i;
	int start;
	cd=(char *)malloc(n*sizeof(char));
	cd[n-1]='\0';
	for(i=1;i<=n;i++)
	{
		start=n-1;
		c=i;
		p=ht[i].parent;
		while(p!=0)
		{
			--start;
			if(ht[p].Lchild==c)
				cd[start]='0';
			else
				cd[start]='1';
			c=p;
			p=ht[p].parent;
		}
		hc[i]=(char *)malloc((n-start)*sizeof(char));
		strcpy(hc[i],&cd[start]);
	}
	free(cd);
}