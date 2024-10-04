using System;
using System.IO;
using System.Numerics;
using System.Text;

namespace Lab1
{
    // Custom exception for out-of-range values
    public class InputOutOfRangeException : Exception
    {
        public InputOutOfRangeException(string message) : base(message) { }
    }

    // Custom exception for invalid input
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message) { }
    }

    public class Program
    {
        // Main entry point
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            string inputFilePath = "Lab1/INPUT.txt";   // Path to the input file
            string outputFilePath = "Lab1/OUTPUT.txt"; // Path to the output file

            try
            {
                // Reading input from file and processing it
                string[] inputs = ReadInputFile(inputFilePath);
                string[] results = ProcessInputs(inputs);
                WriteOutputFile(outputFilePath, results);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Method to process the inputs and calculate the results
        public static string[] ProcessInputs(string[] inputs)
        {
            if (inputs.Length == 0)
            {
                throw new FormatException("File is empty.");
            }

            string[] results = new string[inputs.Length];

            for (int lineNumber = 0; lineNumber < inputs.Length; lineNumber++)
            {
                string input = inputs[lineNumber];
                if (int.TryParse(input.Trim(), out int n))
                {
                    // Check if n is within the allowed range
                    if (n < 2 || n >= 32)
                    {
                        throw new InputOutOfRangeException($"Input {n} is out of range. Error at line {lineNumber + 1}.");
                    }

                    BigInteger sum = 0;

                    // Calculate the different combinations
                    for (int i = 2; i <= n; i++)
                    {
                        sum += Combinations(n, i);
                    }

                    results[lineNumber] = sum.ToString(); // Store the sum in the results array
                }
                else
                {
                    throw new InvalidInputException($"Input '{input}' is not a valid integer. Error at line {lineNumber + 1}.");
                }
            }

            return results; // Return the results for each line of input
        }

        // Method to read input from file
        public static string[] ReadInputFile(string inputFilePath)
        {
            if (!File.Exists(inputFilePath))
            {
                throw new FileNotFoundException($"File error: '{inputFilePath}' not found.");
            }

            return File.ReadAllLines(inputFilePath, Encoding.UTF8);
        }

        // Method to write the results to a file
        public static void WriteOutputFile(string outputFilePath, string[] results)
        {
            File.WriteAllLines(outputFilePath, results);
        }

        // Function to calculate the factorial
        static BigInteger Factorial(int n)
        {
            BigInteger f = 1;
            for (int i = 1; i <= n; i++)
                f *= i;
            return f;
        }

        // Function to calculate combinations C(n, r) = n! / (r! * (n-r)!)
        static BigInteger Combinations(int n, int r)
        {
            return Factorial(n) / (Factorial(r) * Factorial(n - r));
        }
    }
}
