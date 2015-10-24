#include<stdio.h>
#include<stdlib.h>
#include<time.h>


#define MaxNum 100  // 定义数组最大值

void Max_Min(int * array,int arrayStart, int arrayStop, int *max,int *min)
{
	int maxRight,minRight,maxLeft,minLeft,mid;
	
	if(arrayStart == arrayStop)
	{
		*max = *min = array[arrayStart];
	}
	else
	{
		mid =( arrayStart + arrayStop ) / 2;
		Max_Min(array,arrayStart,mid,&maxLeft,&minLeft);
		Max_Min(array,mid+1,arrayStop,&maxRight,&minRight);		
		*max = maxLeft > maxRight ? maxLeft : maxRight;
		*min = minLeft < minRight ? minLeft : minRight;
	}
}


int main(void)
{
	int arrayLength,arrayStart = 0,arrayStop = 9;
	int max = -1,min = -1;
	int m_Array[10] = {0,4,5,9,7,8,3,2,1,6};
	//用时间做随机数种子，每次产生数字不一样
//	srand(unsigned)time(NULL));
//	printf("请输入数组的长度（长度不大于100）\n");
//	scanf("%d",&arrayLength);
	
//	arrayStop = arrayLength - 1;

//	for(int i = 0; i < arryLength; i++)
//	{
		//产生0-100随机数字
//		m_Array[i] = rand() % 101;
//	}

	Max_Min(m_Array,arrayStart,arrayStop,&max,&min);

	printf("%d %d\n",max,min);
	return 0;
}