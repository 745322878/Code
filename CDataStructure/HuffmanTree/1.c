#include<stdio.h>
#include<stdlib.h>
#include<string.h>
#define N 30
#define M 2*N-1
#define MAXWEIGHT 1000
typedef char *  HuffmanCode[N];
typedef struct 
{
	int weight;
	int parent;
	int Lchild;
	int Rchild;
}HTNode,HuffmanTree[M+1];
HuffmanTree ht;
HuffmanCode hc;
void select(HuffmanTree ht,int n, int* s1 ,int* s2);		//��ht��ǰi-1��ѡ˫��Ϊ0��Ȩֵ��С�������
void CreateHuffmanTree(HuffmanTree ht,int w[], int n);		//����һ��HuffmanTree
void CreateHuffmanCode1(HuffmanTree ht,HuffmanCode hc,int n);	//Huffman ����
void CreateHuffmanDeCode(HuffmanTree ht,char ch[],int n);		//Huffman ����
//����HuffmanTree,����Ϊ��������Ҷ�ӽ������飬Ҷ�ӽ��ĸ���
void CreateHuffmanTree(HuffmanTree ht,int w[], int n)
{
	int i;
	int s1,s2;
	int m;
	m=2*n-1;
	//��ʼ��ǰn��Ԫ�س�Ϊ�����
	for(i=1;i<=n;i++)
	{
		ht[i].weight=w[i];
		ht[i].Lchild=0;
		ht[i].parent=0;
		ht[i].Rchild=0;
	}	
	//��ʼ����n-1��Ԫ��Ϊ��
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
//����һ��HuffmanTree
void select(HuffmanTree ht,int n, int* s1 ,int* s2)
{
	int j;
	int m1,m2;
	m1=m2=MAXWEIGHT;
	*s1=*s2=1;
	for(j=1;j<=n;j++)
	{
		if(ht[j].parent==0&&ht[j].weight<m1)
		{
			m2=m1;
			*s2=*s1;
			m1=ht[j].weight;
			*s1=j;
		}
		else if(ht[j].parent==0&&ht[j].weight<m2)
		{
			m2=ht[j].weight ;
			*s2=j;
		}
	}
	
}
//Huffman ����,nΪҶ�ӽ�����
void CreateHuffmanCode1(HuffmanTree ht,HuffmanCode hc,int n)	
{
	char *cd;
	int start;
	int c,p,i;
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
			p=ht[c].parent;
		}
		hc[i]=(char *)malloc((n-start)*sizeof(char));
		strcpy(hc[i],&cd[start]);
	}
	free(cd);
}
//Huffman ����
void CreateHuffmanDeCode(HuffmanTree ht,char ch[],int n)
{
	int i=0,j,c,l;
	HTNode p;

	l=strlen(ch);
	
	for(j=0;j<l;j=i)
	{
		p=ht[2*n-1];
		while(p.Lchild!=0&&p.Rchild!=0)
		{
			if(ch[i]=='0')
				c=p.Lchild;
			else 
				c=p.Rchild;
			p=ht[c];
			i++;
		}
		printf("%5c",p.weight+'0');
	}
	printf("\n");
}
//Huffman ����
/*void CreateHuffmanDeCode(HuffmanTree ht,HuffmanCode hc,int n)	
{
	int i,l,j;
	int c;
	HTNode p;
	printf("\nHuffman����Ϊ:\n");
	for(i=1;i<=n;i++)
	{
		p=ht[2*n-1];
		l=strlen(hc[i]);
		for(j=0;j<l;j++)
		{
			if(hc[i][j]=='0')
			{
				c=p.Lchild;
				
			}
			else
			{
				c=p.Rchild;
			}
			p=ht[c];
		}
		printf("%3c",p.weight+48);
	}
	printf("\n");
}*/
int main()
{
	int num;
	int w[N];
	int i;
	char ch[N];
	printf("������Ҷ�ӽ��ĸ���:\n");
	scanf("%d",&num);
	getchar();
	printf("������ÿ��Ҷ�ӽ���Ȩֵ:\n");
	for(i=1;i<=num;i++)
	{
		scanf("%d",&w[i]);
		fflush(stdin);
	}
	CreateHuffmanTree(ht,w,num);
	printf("��������ʾ��ͼ:\n");
	for(i=1;i<=2*num-1;i++)
	{
		printf("%5d%5d%5d%5d\n",ht[i].weight,ht[i].parent,ht[i].Lchild,ht[i].Rchild);
	}
	
	CreateHuffmanCode1(ht,hc,num);
	printf("Huffman����:\n");
	for(i=1;i<=num;i++)
	{
		printf("%c: ",ht[i].weight+48);
		printf("%s\n",hc[i]);
	}
	//CreateHuffmanDeCode( ht, hc, num);
	printf("����������չʾ��ѹ������������룺\n");
	gets(ch);
	CreateHuffmanDeCode(ht,ch,num);
	return 0;
}
