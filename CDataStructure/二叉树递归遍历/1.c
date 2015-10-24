#include<stdio.h>
#define DataType int
typedef struct Node
{
	DataType data;
	Struct Node *Lchild
	Struct Node *Rchild;
}BiTNode,*BiTree
int main()
{

	return 0;
}
//先序遍历二叉树，root为根节点的指针
void PreOrder(BiTree root)
{
	if(root)
	{
		Visit(root->data );
		PreOrder(root->data);
		PreOrder(root->data);
	}
}
Visit(DataType BiTree root->data)