#include<stdio.h>
#include<stdlib.h>
typedef char DataType;
typedef struct Node 
{
	DataType data;
	struct Node *Lchild;
	struct Node *Rchild;
}BiTNode,*BiTree;
BiTree b;
DataType data[20];

/*void Init_BiTree(BiTree *root)
{
	*root=malloc(sizeof(BiTNode));
}*/
void CreateBiTree(BiTree *root)
{

	char ch;
	ch=getchar();
	if(ch=='#')
		*root=NULL;
	else
	{
		
		*root=(BiTNode *)malloc(sizeof(BiTNode));
		(*root)->data=ch;
		CreateBiTree(&(*root)->Lchild);
		CreateBiTree(&(*root)->Rchild);
	}
}
void PreOrder(BiTree root)
{
	if(root)
	{
		printf("%c",root->data );
		PreOrder(root->Lchild);
		PreOrder(root->Rchild);
	}
}
/*
//求结点的双亲结点
BiTree Parent(BiTree root,BiTree current)
{
	BiTree p;
	if(root==NULL)
		return NULL;
	else
	{
		if(root->Lchild==current||root->Rchild==current)
		{
			return root;
		}
		p=Parent(root->Lchild,current);
		if(p!=NULL)
		{
			return p;
		}
		else
		{
			return Parent(root->Rchild,current);
		}
	}
}
void PreOrder(BiTree root)
{
	BiTree p,q;
	int i=0,j;
	p=root;
	if(root)
	{
		if(root->Lchild==NULL&&root->Rchild==NULL)
		{
		
			printf("%c:",root->data);
			while(p->data!=b->data)
			{
				q=Parent(b,p);
				//printf("%2c",q->data);
				data[i]=q->data;
				i++;
				p=q;
			}
			for(j=i-1;j>=0;j--)
			{
				printf("%2c",data[j]);
			}
			printf("\n");
		}
		PreOrder(root->Lchild);
		PreOrder(root->Rchild);
	}
}*/
int main()
{
	BiTree root=NULL;
	CreateBiTree(&root);
	b=root;
	PreOrder(root);
	return 0;
}
