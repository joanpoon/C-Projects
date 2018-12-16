using System;

namespace LWTech.JoanPoon.AssignmentOne
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputOne = GetInteger("Please enter the first integer. ");
            int inputTwo = GetInteger("Please enter the second integer. ");
            int inputThree = GetInteger("Please enter the third integer. ");

            Console.WriteLine("The sum of {0} and {1} and {2} is {3}", inputOne,
                              inputTwo, inputThree, CalculateSum(inputOne, inputTwo, inputThree));

            int polynomialInput = GetInteger("Please enter an integer for calculating the polynomial.");
            Console.WriteLine("The value of the polynomial with x = {0} is {1} ",
                              polynomialInput, CalculatePolynomial(polynomialInput));

            int seconds = GetInteger("Please enter an integer representing the number of seconds.");
            Console.WriteLine(ConvertSeconds(seconds));

            int numbersInSequence = GetPositiveInteger("How many numbers are in the sequence?");
            Console.WriteLine(AnalyzeSequence(numbersInSequence));

            bool moreNumbers = true;
            int numbersSum = 0;
            int numberCount = 0;
            while (moreNumbers)
            {
                int userInput = GetInteger("Please enter a non-negative integer for calculating the average," +
                           "or enter a negative integer to end.");

                if (userInput < 0 && numberCount == 0)
                    moreNumbers = false;
                else if (userInput < 0 && numberCount > 0)
                {
                    Console.WriteLine("The average of intergers you entered is " +
                                      $"{CalculateAverage(numbersSum, numberCount)}");
                    moreNumbers = false;
                }
                else
                {
                    numbersSum += userInput;
                    numberCount++;
                }

            }

            Console.WriteLine("Display all odd integers between 100 and 150 by a for-loop");
            for (int i = 100; i < 151; i++)
                if (i % 2 != 0)
                    Console.Write("{0} ", i);

            Console.WriteLine("\nDisplay all even integers between 150 and 200 by a while-loop");
            int j = 150;
            while (j < 201)
            {
                if (j % 2 == 0)
                    Console.Write("{0} ", j);
                j++;
            }

            Console.WriteLine("\nDisplay all even integers between 100 and 0 by a do-loop");
            int k = 100;
            do
            {
                if (k % 2 == 0)
                    Console.Write("{0} ", k);
                k--;
            } 
            while (k > -1);


            bool goOn = true;
            while (goOn)
            {
                Console.WriteLine("\nPlease enter your test score or enter quit to end the program.");
                string scoreInput = Console.ReadLine();

                if (scoreInput.ToLower() == "quit")
                {
                    Console.WriteLine("Good bye!");
                    goOn = false;
                } 
                else
                {
                    Console.WriteLine(ConvertScore(scoreInput));
                }
            }

            Console.ReadLine();
        }

        static int GetInteger(string prompt)
        {
            int input = 0;
            string response = "";
            bool itWorked = false;

            Console.WriteLine(prompt);

            while (!itWorked)
            {
                response = Console.ReadLine();
                itWorked = int.TryParse(response, out input);

                if(!itWorked)
                {
                    Console.WriteLine("Sorry, I could not understand what you entered." +
                                      " Please enter a valid integer.");
                }
            }
            return input;
        }

        static int CalculateSum(int firstInput, int secondInput, int thirdInput)
        {
            return firstInput + secondInput + thirdInput;
        }

        static double CalculatePolynomial(int input)
        {
            double convert = (double)input;
            return (2 * Math.Pow(convert, 3) + 5 * convert - 1);
        }

        static string ConvertSeconds (int secondsInput)
        {
            int hours = secondsInput / ( 60 * 60 );
            int minutes = secondsInput % (60 * 60) / 60 ;
            int seconds = secondsInput % 60;

            return($"{secondsInput} seconds is equivalent of {hours} hours, {minutes} minutes, {seconds} seconds");
        }

        static int GetPositiveInteger (string prompt)
        {
            int input = 0;
            string response = "";
            bool itWorked = false;

            Console.WriteLine(prompt);

            while (!itWorked)
            {
                response = Console.ReadLine();
                itWorked = int.TryParse(response, out input);

                if (!itWorked)
                {
                    Console.WriteLine("Sorry, I could not understand what you entered." +
                                      " Please enter a valid positive integer.");
                } else if ( input < 0)
                {
                    Console.WriteLine("Sorry, the integer must be a positive number.");
                    itWorked = false;
                }
            }
            return input;
        }

        static string AnalyzeSequence (int numbersInSequence)
        {
            if (numbersInSequence == 0)
                return "There is no minimum and maximum value in the sequence.";
            else if (numbersInSequence == 1)
            {
                int min = GetPositiveInteger("Please enter a non-negative number.");
                return$"The mininum and maximum value in the sequence is {min}.";
            }
            else
            {
                int min, max;
                min = max = GetPositiveInteger("Please enter a non-negative number.");

                for (int i = 1; i < numbersInSequence; i++)
                {
                    int currentInput = GetPositiveInteger("Please enter a non-negative number again.");

                    if (currentInput > max)
                        max = currentInput;
                    else if (currentInput < min)
                        min = currentInput;
                }
                return$"The minimum value is {min}, maximum value is {max}";
            }

        }

        static int CalculateAverage(int sum, int count)
        {
            return sum / count;
        }


        static string ConvertScore(string score)
        {
            bool isValid = int.TryParse(score, out int testScore);
            if (!isValid)
                return ("Sorry, I could not understand what you entered." +
                              " Please enter your test score or enter quit to end.");
            else if (testScore > 100 || testScore < 0)
                return ("Sorry, test score should be between 0 and 100.");
            else
            {
                switch (testScore)
                {
                    case int n when (n >= 91):
                        return ($"{testScore} is a letter grade A.");
                    case int n when (n >= 81):
                        return ($"{testScore} is a letter grade B.");
                    case int n when (n >= 71):
                        return ($"{testScore} is a letter grade C.");
                    case int n when (n >= 61):
                        return ($"{testScore} is a letter grade D.");
                    default:
                        return($"{testScore} is a letter grade F.");
                }
            }

        }
    }
}
