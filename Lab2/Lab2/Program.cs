using System;
using System.IO;

namespace Lab2
{
    public class Program
    {
        // Custom exception for invalid input
        public class InvalidInputException : Exception
        {
            public InvalidInputException(string message) : base(message) { }
        }

        public static int CountWays(int n)
        {
            // Check that n is within the range [1, 2147483647]
            if (n < 1 || n > 2147483647)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "Input must be between 1 and 2147483647.");
            }

            if (n < 3)
                return 0;
            if (n == 3)
                return 1;

            int i = 1, j = 0;

            while (n > 3)
            {
                if (n % 2 == 0)
                {
                    i = 2 * i + j;
                }
                else
                {
                    j = i + 2 * j;
                }
                n /= 2;
            }

            return n == 3 ? i : j;
        }

        static void Main(string[] args)
        {
            // Read input data
            string inputFilePath = Path.Combine("Lab2", "INPUT.txt");   // Path to the input file
            string outputFilePath = Path.Combine("Lab2", "OUTPUT.txt"); // Path to the output file

            try
            {
                string[] lines = ReadInputFile(inputFilePath); // Read input data
                string[] results = ProcessInputs(lines); // Process inputs
                WriteOutputFile(outputFilePath, results); // Write results to output

                // Display success message in console
                Console.WriteLine($"Results successfully written to '{outputFilePath}':");
                foreach (var result in results)
                {
                    Console.WriteLine(result); // Output each result
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Method to read input from file
        public static string[] ReadInputFile(string inputFilePath)
        {
            if (!File.Exists(inputFilePath))
            {
                throw new FileNotFoundException($"File error: '{inputFilePath}' not found.");
            }

            string[] lines = File.ReadAllLines(inputFilePath);
            if (lines.Length == 0)
            {
                throw new FormatException("File is empty.");
            }

            return lines; // Return the lines from the input file
        }

        // Method to process the inputs and calculate the results
        public static string[] ProcessInputs(string[] inputs)
        {
            string[] results = new string[inputs.Length];

            for (int lineNumber = 0; lineNumber < inputs.Length; lineNumber++)
            {
                string input = inputs[lineNumber];
                if (int.TryParse(input.Trim(), out int n))
                {
                    // Count the number of ways
                    int result = CountWays(n);
                    results[lineNumber] = result.ToString(); // Store the result
                }
                else
                {
                    throw new InvalidInputException($"Input '{input}' is not a valid integer. Error at line {lineNumber + 1}.");
                }
            }

            return results; // Return the results for each line of input
        }

        // Method to write the results to a file
        public static void WriteOutputFile(string outputFilePath, string[] results)
        {
            File.WriteAllLines(outputFilePath, results);
        }
    }
}