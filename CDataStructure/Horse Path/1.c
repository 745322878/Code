#include<stdio.h>
#include<stdlib.h>
#include<windows.h>
#include"Array.h"


int main(void)
{
	PosType start;
	InitStack();
	printf("请输入起始位置：(0-7)\nX:");
	scanf("%d",&start.x);
	printf("\nY:");
	scanf("%d",&start.y );
	setweight();
	setmap();
	HorsePath(start);
	OutputPath();
	printf("\n");
	return 0;
}


