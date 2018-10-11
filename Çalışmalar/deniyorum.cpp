#include<stdio.h>
#include<conio.h>
#include<stdlib.h>
#define boyut 6
int i,j,k,a,b,u,v,n,ne=1;
int min,mincost=0,parent[6];
int cost[6][6];
int find(int);
int uni(int,int);
int main()
{
	//clrscr();
	printf("\n\tImplementation of Kruskal's algorithm\n");
	/* printf("\nEnter the no. of vertices:");
	scanf("%d",&n);
	printf("\nEnter the cost adjacency matrix:\n");
	for(i=1;i<=n;i++)
	{
		for(j=1;j<=n;j++)
		{
			scanf("%d",&cost[i][j]);
			if(cost[i][j]==0)
				cost[i][j]=999;
		}
	}
	*/
	
	
	//int cost[boyut][boyut]
	int i,j,n,s,e;
    //FILE *p = NULL;
    FILE *p;
    p=fopen("matris.txt","r");
    //char *dosyaAdi = "matris.txt";
    n=6;
    
    if(p!= NULL)
    {
        for(i = 0; i < n; i++)
        {
            for(j = 0; j < n; j++)
            {
                fscanf(p, "%d", &cost[i][j]);
                printf("%d ", cost[i][j]);
            }
            printf("\n");
        }
    }
    else
    {
        printf("dosya bulunamadi");
    }
    
	printf("The edges of Minimum Cost Spanning Tree are\n");
	while(ne < n)
	{
		for(i=0,min=999;i<n;i++)
		{
			for(j=0;j < n;j++)
			{
				if(cost[i][j] < min)
				{
					min=cost[i][j];
					a=u=i;
					b=v=j;
				}
			}
		}
		u=find(u);
		v=find(v);
		if(uni(u,v))
		{
			printf("%d edge (%d,%d) =%d\n",ne++,a,b,min);
			mincost +=min;
		}
		cost[a][b]=cost[b][a]=999;
	}
	printf("\n\tMinimum cost = %d\n",mincost);
	getch();
}
int find(int i)
{
	while(parent[i])
	i=parent[i];
	return i;
}
int uni(int i,int j)
{
	if(i!=j)
	{
		parent[j]=i;
		return 1;
	}
	return 0;
}
