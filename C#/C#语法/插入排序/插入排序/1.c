#include<stdio.h>
int main(void)
{
	int a[10] = { 50, 60, 20, 30, 10, 80, 90, 70, 40, 100 };
	int i, j, t;
	for ( i = 1; i < 10; i++)
	{
		 t = a[i];
		 j = i - 1;
		while (j >= 0 && a[j]>t)
		{
			a[j + 1] = a[j];
			j--;
		}
		a[j+1] = t;

	}
	for (i = 0; i < 10; i++)
		printf("%d ", a[i]);
	getchar();
	getchar();
	return 0;

}