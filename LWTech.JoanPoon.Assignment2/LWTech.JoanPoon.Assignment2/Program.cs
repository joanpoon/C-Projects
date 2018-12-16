using System;

namespace LWTech.JoanPoon.Assignment2
{
    class Program
    {
        static Random rng = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Assignment 2: Connect Four \t\t Joan Poon");
            Console.WriteLine("Pseudocode reviewed by Douglas Chan");
            Console.WriteLine("===========================================================\n");
            Console.WriteLine("Welcome to the game! You will be player X, the computer will be player O.");
            Console.WriteLine("This is the view of the grid: \n");

            int round = 1;
            char[,] myGrid = new char[8, 8];
            bool gameOver = false;

            InitializeGrid(myGrid);
            DisplayGrid(myGrid);


            while (!gameOver)
            {
                Console.WriteLine($"\nRound #: {round++}");

                DropPlayerChecker(myGrid, ValidatePlayerChoice(myGrid));
                DisplayGrid(myGrid);

                DropComputerChecker(myGrid);
                DisplayGrid(myGrid);

                if(round > 3)
                {
                    if (CheckVerticalWinner(myGrid, 'X') || CheckHorizontalWinner(myGrid, 'X'))
                    {
                        gameOver = true;
                        Console.WriteLine("Congratulations! You won the game!");
                    }

                    if (CheckVerticalWinner(myGrid, 'O') || CheckHorizontalWinner(myGrid, 'O'))
                    {
                        gameOver = true;
                        Console.WriteLine("Sorry, the computer won, AI will take over soon... ");
                    }

                    if (round == 33)
                    {
                        gameOver = true;
                        Console.WriteLine("It is a tie game!");
                    }
                }
            }


        }


        static void InitializeGrid(char[,] grid){
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    grid[row, col] = ' ';
                }
            }
        }


        static void DisplayGrid(char[,] grid)
        {
            Console.WriteLine("_________________________________");
            for (int row = 0; row < 8; row++)
            {
                Console.Write("| ");
                for (int col = 0; col < 8; col++)
                {
                    Console.Write(grid[row, col] + " | ");
                }
                Console.WriteLine("\n_________________________________");
            }
        }

        static int ValidatePlayerChoice(char[,] grid)
        {
            int userChoice = 0;
            String response = "";
            bool itWorked = false;

            while (!itWorked)
            {
                Console.Write("Which column would you like to drop a checker into (1-8)? ");
                response = Console.ReadLine().Trim();

                if (!int.TryParse(response, out userChoice))
                    Console.WriteLine("Sorry, I could not understand what you entered." +
                                      " Please enter a valid choice.");
                else if (userChoice < 1 || userChoice > 8)
                    Console.WriteLine("Sorry, I could not understand what you entered." +
                                          " Please enter a valid choice within 1-8.");
                else if (isFull(grid, --userChoice))
                    Console.WriteLine("Sorry, the column you chose is already full.");
                else
                    itWorked = true;

            }

            return userChoice;

        }

        static bool isFull(char[,] grid, int col)
        {
            for (int row = 7; row > -1; row--)
            {
                if (grid[row, col] == ' ')
                    return false;
            }
            return true;
        }


        static void DropPlayerChecker(char[,] grid, int playersChoice)
        {
            for (int row = 7; row > -1; row--)
            {
                if(grid[row, playersChoice] == ' ')
                {
                    grid[row, playersChoice] = 'X';
                    break;
                }
            }
        }

        static void DropComputerChecker(char[,] grid)
        {
            bool isCompleted = false;

            while (!isCompleted)
            {
                int compChoice = rng.Next(8);

                if (!isFull(grid, compChoice))
                {
                    Console.WriteLine("\n\nIt's the computer's turn.");
                    Console.WriteLine($"The computer drops a checker in column # {compChoice+1}");

                    for (int row = 7; row > -1; row--)
                    {
                        if (grid[row, compChoice] == ' ')
                        {
                            grid[row, compChoice] = 'O';
                            isCompleted = true;
                            break;
                        }
                    }
                }
            }
        }


        static bool CheckHorizontalWinner(char[,] grid, char player)
        {
            for (int col = 0; col < 5; col++)
            {
                for (int row = 0; row < 8; row++)
                {
                    if(grid[row,col] == player && grid[row, col+1] == player &&
                       grid[row, col+2] == player && grid[row, col+3] == player)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        static bool CheckVerticalWinner(char[,] grid, char player)
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (grid[row, col] == player && grid[row + 1, col] == player &&
                       grid[row + 2, col] == player && grid[row + 3, col] == player)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
