#include<stdio.h>
#define M 30
void  RubikCube_Init(int RC[M][M],int m); //��ʼ��ħ����
void RubikCube(int RC[M][M],int m);    //ħ��������
void RubikCube_Print(int RC[M][M],int m);  //���ħ����
int main()
{
	int RC[M][M];
	int m;
	printf("������ħ����Ľ���(m),mΪ����\n");
	scanf("%d",&m);
	if(m%2==0)
	{
		printf("��������\n");
		return 0;
	}
	else 
	{
		RubikCube_Init(RC,m);
		RubikCube(RC,m);
		RubikCube_Print(RC,m);
	}
	return 0;
}
void  RubikCube_Init(int RC[M][M],int m) //��ʼ��ħ����
{
	int i,j;
	for(i=0;i<m;i++)
		for(j=0;j<m;j++)
		{
			RC[i][j]=0;
		}
}
void RubikCube(int RC[M][M],int m)    //ħ��������
{
	int x,y,i;  //x,y��ʾ���꣬i��ʾ����1-m*m
	i=1;
	x=0;
	y=m/2;
	RC[x][y]=i;
	for(i=2;i<=m*m;i++)
	{
		x=(x-1+m)%m;
		y=(y-1+m)%m;
		if(RC[x][y]!=0)
		{
			x=(x+2)%m;
			y=(y+1)%m;
		}
		RC[x][y]=i;
	}

}
void RubikCube_Print(int RC[M][M],int m)  //���ħ����
{
	int i,j;
	for(i=0;i<m;i++)
	{
		for(j=0;j<m;j++)
		{
			printf("%3d",RC[i][j]);
		}
		printf("\n");
	}
}