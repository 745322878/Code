/*
 ============================================================================
 Name        : TTMS.c
 Author      : shu xinfeng
 Version     :
 Copyright   : Your copyright notice
 Description : Hello World in C, Ansi-style
 ============================================================================
 */

#include <stdio.h>
#include <stdlib.h>
#include <assert.h>
//#include "./View/Account_UI.h"

//定义全局变量，用于存储登陆用户信息
//account_t gl_CurUser={0, USR_ANOMY, "Anonymous",""};

int main(void) {
	setvbuf(stdout, NULL, _IONBF, 0);
	printf("Welcome to TTMS……\n");
	//链表示例
	printf("Press [Enter] key to run example for list\n");
	fflush(stdin);
	getchar();
	Example_List();

	printf("Press [Enter] key to run example for entity key service\n");
	fflush(stdin);
	getchar();
	Example_EntKeySrv();

	//主键服务示例
	return EXIT_SUCCESS;
}
