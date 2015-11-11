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
int count=0;	//����ܸ���
int leaf_count=0;		//Ҷ�ӽ�����
int depth=0;		//�������
BiTree b;			//ȫ�ֱ����������
DataType data[20];		//�����㵽Ҷ�ӽ��Ľ��
//������
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
//����ÿ�����
void Visit(char c,int level)
{
	printf("%c is at %d level of BiTree\n",c,level);
}
//�������,������
void PreOrdTraverse(BiTree root,int level)
{
	if(root)
	{
		Visit(root->data,level);
		PreOrdTraverse(root->Lchild,level+1);
		PreOrdTraverse(root->Rchild,level+1);
	}
}
//��������������
void  InOrderTraverse(BiTree root,int level)
{
	if(root)
	{
		InOrderTraverse(root->Lchild,level+1);
		Visit(root->data,level);
		InOrderTraverse(root->Rchild,level+1);
	}
}
//������������Ҹ�
void PostOrderTraverse(BiTree root,int level)
{
	if(root)
	{
		PostOrderTraverse(root->Lchild,level+1);
		PostOrderTraverse(root->Rchild,level+1);
		Visit(root->data,level);
	}
}
//��ʼ��ջ
SeqStack *Init_SeqStack(SeqStack *s)
{
	s=malloc(sizeof(SeqStack));
	s->top=-1;
	return s;
}
//ջ�Ƿ�Ϊ��
int IsEmpty(SeqStack *s)
{
	if(s->top==-1)
		return 1;
	else
		return 0;
}
//��ջ
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
//��ջ
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
//�õ�ջ��Ԫ��
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
//����ǵݹ����
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
//����ǵݹ����
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
//����ǵݹ����
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

//ͳ�ƶ������Ľ�����
void PreOrder_Count(BiTree root)
{
	if(root)
	{
		count++;
		PreOrder_Count(root->Lchild);
		PreOrder_Count(root->Rchild);
	}
}
//ͳ�ƶ�����Ҷ�ӽ�����
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
//�����в��ҷ��������Ľ��
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
//�����˫�׽��
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
//��ӡ����㵽Ҷ�ӽ���·��
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
//��������߶�
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
//��ӡ��״ͼ��
void PrintTree(BiTree root ,int h)
{
	int i;
	if(root==NULL)
		return ;
	PrintTree(root->Rchild,h+1);
	//�ȴ�ӡ������
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
	BiTree root=NULL;		//�����
	BiTree current=NULL;	//��ѯ�Ľ��
	BiTree parent;			//˫�׽��
//	s1=Init_SeqStack(s);
	int choice;
	printf("***************************\n");
	printf("1.�������д�����\n");
	printf("2.����ݹ����\n");
	printf("3.����ݹ����\n");
	printf("4.����ݹ����\n");
	printf("5.����ǵݹ����\n");
	printf("6.����ǵݹ����\n");
	printf("7.����ǵݹ����\n");
	printf("8.ͳ�ƶ������Ľ����\n");
	printf("9.ͳ�ƶ�����Ҷ�ӽ�㼰����\n");
	printf("10.�������ĳ����˫��\n");
	printf("11.��ӡ����㵽Ҷ�ӽ���·��\n");
	printf("12.��������߶�\n");
	printf("13.����������״��ӡ\n");
	while(1)
	{
		fflush(stdin);
		printf("��ѡ�����:\n");
		scanf("%d",&choice);
		getchar();
		switch(choice)
		{
			case 1:
					printf("����������չ�ı�������:\n");
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
					printf("�������������:\n");
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