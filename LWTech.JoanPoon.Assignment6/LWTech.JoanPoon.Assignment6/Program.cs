using System;
using System.Threading;

namespace LWTech.JoanPoon.Assignment6
{
    class Program
    {
        const int maxX = 40;
        const int maxY = 60;
        const int coveragePercentage = 20;
        static Random rand = new Random();

        static void Main(string[] args)
        {
            int[,] grid = new int[maxX, maxY];
            int numMilliseconds = 50;

            PopulateGrid(grid, coveragePercentage);
            bool quit = false;

            Console.WriteLine("Joan Poon \t\t\t Assignment 6");
            Console.WriteLine("Welcome to Conway's Game of Line!");
            Console.WriteLine("_____________________________________________________________________\n\n\n");


            while (!quit)
            {
                grid = UpdateGrid(grid, maxX, maxY);
                DisplayGrid(grid);
                Console.WriteLine("_____________________________________________________________________");

                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.Q:
                            quit = true;
                            Console.WriteLine("\n\nQuitting... Good Bye!") ;
                            break;
                        case ConsoleKey.R:
                            PopulateGrid(grid, coveragePercentage);
                            DisplayGrid(grid);
                            break;
                        case ConsoleKey.F:
                            FillUpGrid(grid);
                            DisplayGrid(grid);
                            quit = true;
                            Console.WriteLine("\n\nThe grid is filled... Good Bye!");
                            break;
                        default:
                            break;
                    }
                }
                Thread.Sleep(numMilliseconds);
            }

        }

        private static int[,] PopulateGrid(int[,] grid, int coverage)
        {
            int totalLiveCells = maxX * maxY * coverage / 100;
            int placedLiveCell = 0;

            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {

                    grid[i, j] = 0;
                }
                    
            }

            while(placedLiveCell < totalLiveCells)
            {
                int x = rand.Next(maxX);
                int y = rand.Next(maxY);
                if(grid[ x, y] == 0)
                {
                    grid[x, y] = 1;
                    placedLiveCell++;
                }
            }

            return grid;
        }


        private static void DisplayGrid(int[,] grid)
        {
            string line;

            for (int x = 0; x < maxX; x++)
            {
                line = "";
                for (int y = 0; y < maxY; y++)
                {
                    line += grid[x, y] == 1 ? "*" : " ";
                }
                Console.WriteLine(line);
            }
        }


        private static int[,] UpdateGrid(int[,] currentGrid, int N, int M)
        {
            int[,] future = new int[N, M];
            for (int i = 1; i < N - 1; i++)
            {
                for (int j = 1; j < M - 1; j++)
                {
                    int neighbor = 0;

                    for (int x = -1; x < 2; x++)
                        for (int y = -1; y < 2; y++)
                            neighbor += currentGrid[i + x, j + y];

                    if (currentGrid[i, j] == 1 && neighbor < 2)
                        future[i, j] = 0;
                    else if (currentGrid[i, j] == 1 && neighbor > 3)
                        future[i, j] = 0;
                    else if (currentGrid[i, j] == 0 && neighbor == 3)
                        future[i, j] = 1;
                    else
                        future[i, j] = currentGrid[i, j];
                }
            }

            return future;
        }


        private static void FillUpGrid(int[,] grid)
        {
            for (int x = 0; x < maxX; x++)
            {
                for(int y = 0; y < maxY; y++)
                {
                    grid[x,y] = 1;
                }
            }
        }

    }
}


