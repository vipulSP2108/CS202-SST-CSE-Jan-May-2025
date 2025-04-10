// FIRSTLY MY CODE WAS NOT HANDLEING INTIGER THE I UPDATED CODE

using System;

class Calculator
{
    private int number1;
    private int number2;

    public void GetInput()
    {
        Console.Write("Enter the first number: ");
        while (!int.TryParse(Console.ReadLine(), out number1))
        {
            Console.WriteLine("Error: Invalid input! Please enter an integer.");
            Console.Write("Enter the first number: ");
        }

        Console.Write("Enter the second number: ");
        while (!int.TryParse(Console.ReadLine(), out number2))
        {
            Console.WriteLine("Error: Invalid input! Please enter an integer.");
            Console.Write("Enter the second number: ");
        }
    }

    public void PerformOperations()
    {
        int sum = number1 + number2;
        int difference = number1 - number2;
        int product = number1 * number2;

        // Handle division by zero
        if (number2 != 0)
        {
            double division = (double)number1 / number2;
            Console.WriteLine($"Division: {division}");
        }
        else
        {
            Console.WriteLine("Error: Division by zero is not allowed.");
        }

        Console.WriteLine($"Sum: {sum}");
        Console.WriteLine($"Difference: {difference}");
        Console.WriteLine($"Product: {product}");

        // Check if the sum is even or odd
        if (sum % 2 == 0)
            Console.WriteLine("The sum is even.");
        else
            Console.WriteLine("The sum is odd.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Calculator calc = new Calculator();
        calc.GetInput();
        calc.PerformOperations();
    }
}