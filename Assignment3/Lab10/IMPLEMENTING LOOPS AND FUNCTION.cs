using System;

class NumberOperations
{
    // Method to print numbers from 1 to 10 using a for loop
    public void PrintNumbers()
    {
        Console.WriteLine("Numbers from 1 to 10:");
        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine(i);
        }
    }

    // Method to calculate the factorial of a number
    public long CalculateFactorial(int number)
    {
        long factorial = 1;
        for (int i = 1; i <= number; i++)
        {
            factorial *= i;
        }
        return factorial;
    }

    // Method to handle user input until they enter 'exit'
    public void KeepAskingForInput()
    {
        string input;
        Console.WriteLine("Enter a number to calculate its factorial or type 'exit' to quit:");

        while (true)
        {
            Console.Write("Input: ");
            input = Console.ReadLine();

            if (input.ToLower() == "exit")
            {
                Console.WriteLine("Exiting the program. Goodbye!");
                break;
            }

            if (int.TryParse(input, out int number) && number >= 0)
            {
                long result = CalculateFactorial(number);
                Console.WriteLine($"Factorial of {number} is {result}");
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a non-negative integer or 'exit' to quit.");
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        NumberOperations operations = new NumberOperations();

        // Calling the methods
        operations.PrintNumbers();
        operations.KeepAskingForInput();
    }
}