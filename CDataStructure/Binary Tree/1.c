#include<stdio.h>
#include<stdlib.h>
#define MAXSIZE 50
typedef char DataType;
typedef struct Node
{
	DataType data;
	struct Node *Lchild;
	struct Node *Rchild;
}BiTNode,*BiTree;
typedef struct 
{
	BiTree data[MAXSIZE];
	int top;
}SeqStack;
SeqStack *s1;
int count=0;	//结点总个数
int leaf_count=0;		//叶子结点个数
int depth=0;		//树的深度
BiTree b;			//全局变量，根结点
DataType data[20];		//存根结点到叶子结点的结点
//创建树
//ABD#G###CE#H##F##
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
		CreateBiTree(&((*root)->Lchild));
		CreateBiTree(&((*root)->Rchild));
	}

}
//访问每个结点
void Visit(char c,int level)
{
	printf("%c is at %d level of BiTree\n",c,level);
}
//先序遍历,根左右
void PreOrdTraverse(BiTree root,int level)
{
	if(root)
	{
		Visit(root->data,level);
		PreOrdTraverse(root->Lchild,level+1);
		PreOrdTraverse(root->Rchild,level+1);
	}
}
//中序遍历，左根右
void  InOrderTraverse(BiTree root,int level)
{
	if(root)
	{
		InOrderTraverse(root->Lchild,level+1);
		Visit(root->data,level);
		InOrderTraverse(root->Rchild,level+1);
	}
}
//后序遍历，左右根
void PostOrderTraverse(BiTree root,int level)
{
	if(root)
	{
		PostOrderTraverse(root->Lchild,level+1);
		PostOrderTraverse(root->Rchild,level+1);
		Visit(root->data,level);
	}
}
//初始化栈
SeqStack *Init_SeqStack(SeqStack *s)
{
	s=malloc(sizeof(SeqStack));
	s->top=-1;
	return s;
}
//栈是否为空
int IsEmpty(SeqStack *s)
{
	if(s->top==-1)
		return 1;
	else
		return 0;
}
//入栈
int Push_SeqStack(SeqStack *s,BiTree root)
{
	if(s->top==MAXSIZE-1)
		return 0;
	else
	{
		s->data[++s->top]=root;
		return 1;
	}
}
//出栈
int Pop_SeqStack(SeqStack *s,BiTree *root)
{
	if(IsEmpty(s))
		return 0;
	else
	{
		*root=s->data[s->top--];
		return 1;
	}
}
//得到栈顶元素
int Top_SeqStack(SeqStack *s,BiTree *root)
{
	if(IsEmpty(s))
		return 0 ;
	else
	{
		*root=s->data[s->top];
		return 1;
	}
}
//先序非递归遍历
void PreOrder(BiTree root)
{
	
	BiTree p;
	s1=Init_SeqStack(s1);
	p=root;
	while(p!=NULL||!IsEmpty(s1))
	{
		while(p!=NULL)
		{
			printf("%c",p->data);
			Push_SeqStack(s1,p);
			p=p->Lchild;
		}
		if(!IsEmpty(s1))
		{
			Pop_SeqStack(s1,&p);
			p=p->Rchild;
		}
	}
	printf("\n");
}
//中序非递归遍历
void InOrder(BiTree root)
{
	BiTree p;
	s1=Init_SeqStack(s1);
	p=root;
	while(p!=NULL||!IsEmpty(s1))
	{
		while(p!=NULL)
		{
			
			Push_SeqStack(s1,p);
			p=p->Lchild;
		}
		if(!IsEmpty(s1))
		{
			Pop_SeqStack(s1,&p);
			printf("%c",p->data);
			p=p->Rchild;
		}
	}
	printf("\n");
}
//后序非递归遍历
void PostOrder(BiTree root)
{
	BiTree p,q;
	s1=Init_SeqStack(s1);
	p=root;
	q=NULL;
	while(p!=NULL||!IsEmpty(s1))
	{
		while(p!=NULL)
		{
			
			Push_SeqStack(s1,p);
			p=p->Lchild;
		}
		
		if(!IsEmpty(s1))
		{
			Top_SeqStack(s1,&p);
			if(p->Rchild==NULL||p->Rchild==q)
			{
				Pop_SeqStack(s1,&p);
				printf("%c",p->data);
				q=p;
				p=NULL;
			}
			else
			{
				p=p->Rchild;	
			}
		
		}
	}
	printf("\n");
}

//统计二叉树的结点个数
void PreOrder_Count(BiTree root)
{
	if(root)
	{
		count++;
		PreOrder_Count(root->Lchild);
		PreOrder_Count(root->Rchild);
	}
}
//统计二叉树叶子结点个数
void Sum_Tree(BiTree root,int level)
{
	if(root)
	{
		if(root->Lchild==NULL&&root->Rchild==NULL)
		{
			printf("LeafNode is %c at %d level\n",root->data,level);
			leaf_count++;
		}
		Sum_Tree(root->Lchild,level+1);
		Sum_Tree(root->Rchild,level+1);
	}

}
//在树中查找符合条件的结点
BiTree Search_Node(BiTree root,DataType data)
{
	BiTree p;
	if(root==NULL)
	{
		return NULL;
	}
	if(root->data==data)
	{
		return root;
	}
	p=Search_Node(root->Lchild,data);
	if(p!=NULL)
	{
		return p;
	}
	else
	{
		return Search_Node(root->Rchild,data);
	}
}
//求结点的双亲结点
BiTree Parent(BiTree root,BiTree current)
{
	BiTree p;
	//p=(BiTree)malloc(sizeof(BiTNode));
	if(root==NULL)
		return NULL;
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
//打印根结点到叶子结点的路径
void Print_rootToleaf_Route(BiTree root)
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
		Print_rootToleaf_Route(root->Lchild);
		Print_rootToleaf_Route(root->Rchild);
	}
}
//求二叉树高度
void TreeDepth(BiTree root,int h)
{
	if(root)
	{
		if(h>depth)
		{
			depth=h;
		}
		TreeDepth(root->Lchild, h+1);
		TreeDepth(root->Rchild,h+1);
	}

}
//打印树状图形
void PrintTree(BiTree root ,int h)
{
	int i;
	if(root==NULL)
		return ;
	PrintTree(root->Rchild,h+1);
	//先打印右子树
	for(i=1;i<h;i++)
	{
		printf("\t");
	}
	printf("%c\n",root->data);
	PrintTree(root->Lchild,h+1);
}

int main()
{
	int level=1,h=1;
	DataType data;
	BiTree root=NULL;		//根结点
	BiTree current=NULL;	//查询的结点
	BiTree parent;			//双亲结点
//	s1=Init_SeqStack(s);
	int choice;
	printf("***************************\n");
	printf("1.先序序列创建树\n");
	printf("2.先序递归遍历\n");
	printf("3.中序递归遍历\n");
	printf("4.后序递归遍历\n");
	printf("5.先序非递归遍历\n");
	printf("6.中序非递归遍历\n");
	printf("7.后序非递归遍历\n");
	printf("8.统计二叉树的结点数\n");
	printf("9.统计二叉树叶子结点及个数\n");
	printf("10.求二叉树某结点的双亲\n");
	printf("11.打印根结点到叶子结点的路径\n");
	printf("12.求二叉树高度\n");
	printf("13.二叉树按树状打印\n");
	while(1)
	{
		fflush(stdin);
		printf("请选择操作:\n");
		scanf("%d",&choice);
		getchar();
		switch(choice)
		{
			case 1:
					printf("请输入树扩展的遍历序列:\n");
					CreateBiTree(&root); 
					b=root;
					break;
			case 2: PreOrdTraverse(root,level); break;
			case 3:InOrderTraverse(root,level);break;
			case 4:PostOrderTraverse(root,level);break;
			case 5:	PreOrder(root); break;
			case 6:InOrder(root);break;
			case 7:PostOrder(root);break;
			case 8:PreOrder_Count( root); 	
					printf("This tree have %d node\n",count);
					break;
			case 9:Sum_Tree( root,level);
					printf("LeafNode has %d\n",leaf_count);
					break;
			case 10:
					printf("请输入结点的内容:\n");
					scanf("%c",&data);
					getchar();
					/*current = (BiTree)malloc(sizeof(BiTNode));
					current->data=data;*/
					current=Search_Node(root,data);
					parent=Parent(root,current);
					printf("%c's parent is %c\n",current->data,parent->data);
					break;
			case 11: Print_rootToleaf_Route(root);
					 break;
			case 12:TreeDepth(root,h); 
					 printf("This tree's depth is %d\n",depth);
					 break;
			case 13:PrintTree(root,level);
					break;
		}
	}
	return 0;
}