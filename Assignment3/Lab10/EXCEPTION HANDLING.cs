using System;

class Calculator
{
    private int number1;
    private int number2;

    public void GetInput()
    {
        Console.Write("Enter the first number: ");
        while (true)
        {
            try
            {
                number1 = int.Parse(Console.ReadLine());
                break; // Exit loop if input is valid
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Invalid input! Please enter an integer.");
                Console.Write("Enter the first number: ");
            }
        }

        Console.Write("Enter the second number: ");
        while (true)
        {
            try
            {
                number2 = int.Parse(Console.ReadLine());
                break; // Exit loop if input is valid
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Invalid input! Please enter an integer.");
                Console.Write("Enter the second number: ");
            }
        }
    }

    public void PerformOperations()
    {
        int sum = number1 + number2;
        int difference = number1 - number2;
        int product = number1 * number2;

        try
        {
            // Handle division by zero using try-catch
            double division = (double)number1 / number2;
            Console.WriteLine($"Division: {division}");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Error: Division by zero is not allowed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }

        Console.WriteLine($"Sum: {sum}");
        Console.WriteLine($"Difference: {difference}");
        Console.WriteLine($"Product: {product}");

        // Check if the sum is even or odd
        Console.WriteLine(sum % 2 == 0 ? "The sum is even." : "The sum is odd.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Calculator calc = new Calculator();
            calc.GetInput();
            calc.PerformOperations();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}