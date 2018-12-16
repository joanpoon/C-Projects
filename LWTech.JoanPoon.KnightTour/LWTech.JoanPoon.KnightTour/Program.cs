using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LWTech.JoanPoon.KnightTour
{
    class Program
    {
        static bool PlaceAllKnights(int[,] board, int N, int i, int j, int k)
        {

            if (board[i, j] != 0)
                return false;


            board[i, j] = k;


            if (k == N * N )
                return true;


            if (isSafe(N, i + 2, j + 1) && PlaceAllKnights(board, N, i + 2, j + 1, k + 1))
                return true;

            if (isSafe(N, i + 1, j + 2) && PlaceAllKnights(board, N, i + 1, j + 2, k + 1))
                return true;

            if (isSafe(N, i - 1, j + 2) && PlaceAllKnights(board, N, i - 1, j + 2, k + 1))
                return true;
            
            if (isSafe(N, i - 2, j + 1) && PlaceAllKnights(board, N, i - 2, j + 1, k + 1))
                return true;

            if (isSafe(N, i + 1, j - 2) && PlaceAllKnights(board, N, i + 1, j - 2, k + 1))
                return true;

            if (isSafe(N, i + 2, j - 1) && PlaceAllKnights(board, N, i + 2, j - 1, k + 1))
                return true;

            if (isSafe(N, i - 1, j - 2) && PlaceAllKnights(board, N, i - 1, j - 2, k + 1) )
                return true;
              

            if (isSafe(N, i - 2, j - 1) && PlaceAllKnights(board, N, i - 2, j - 1, k + 1) )
                return true;

            board[i, j] = 0;
            k--;
            return false;
        }

        static void PrintBoard(int N, int[,] board)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    Console.Write(" " + board[i, j].ToString("D2") + " ");
                Console.WriteLine();
            }
        }

        static bool isSafe(int N, int i, int j)
        {
            return (i < N && j < N && i > -1 && j > -1);
        }

        static void Main(string[] args)
        {
            int n = 5;
            int[,] board = new int[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    board[i, j] = 0;

            if (PlaceAllKnights(board, n, 0, 0, 1))
            {
                Console.WriteLine($"Success! Knight's tour on a {n} x {n} board:\n");
                PrintBoard(n, board);
            }
            else
                Console.WriteLine($"Knight's tour cannot be solved on a {n} x {n} board.");


        }
    }
}