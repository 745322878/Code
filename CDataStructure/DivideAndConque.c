#include<stdio.h>
#include<stdlib.h>
#include<time.h>


#define MaxNum 100  // �����������ֵ

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
	//��ʱ������������ӣ�ÿ�β������ֲ�һ��
//	srand(unsigned)time(NULL));
//	printf("����������ĳ��ȣ����Ȳ�����100��\n");
//	scanf("%d",&arrayLength);
	
//	arrayStop = arrayLength - 1;

//	for(int i = 0; i < arryLength; i++)
//	{
		//����0-100�������
//		m_Array[i] = rand() % 101;
//	}

	Max_Min(m_Array,arrayStart,arrayStop,&max,&min);

	printf("%d %d\n",max,min);
	return 0;
}