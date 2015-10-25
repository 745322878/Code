#include<stdio.h>
void count(int value)
{
	if (value == 0)
		return;
	count(value - 1);
	printf("%d\n", value);
}
int main(void)
{
	count(3);
	getchar();
	return 0 ;
}