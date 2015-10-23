#include <stdio.h>
#include<windows.h>
void Run();
int i;
int main(void)
{
	
	Run();
	return 0;
}
void Run()
{
	for(; ;)
	{
		for(i=0;i<1000000000;i++)
			;
		Sleep(10);
	}
}
