using System;
using System.IO;

class Program
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

    static int CountWays(int n)
    {
        // Base cases
        if (n < 3)
            return 0;
        if (n == 3)
            return 1;

        // Initial values of coefficients
        int i = 1, j = 0;

        while (n > 3)
        {
            // If n is even
            if (n % 2 == 0)
            {
                i = 2 * i + j;
            }
            else
            {
                j = i + 2 * j;
            }

            // Move to the smaller value
            n /= 2;
        }

        // Return the result based on the parity of the final value
        return n == 3 ? i : j;
    }

    static void Main(string[] args)
    {
        // Read input data
        string inputFilePath = "Lab2/INPUT.TXT"; // Path to the input file
        string outputFilePath = "Lab2/OUTPUT.TXT"; // Path to the output file

        try
        {
            string[] lines = ReadInputFile(inputFilePath); // Read input data
            WriteOutputFile(outputFilePath, ProcessInputs(lines)); // Process inputs and write to output
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
                // Check if n is within the allowed range
                if (n < 3 || n >= 32) // You can adjust the upper limit based on your specific needs
                {
                    throw new InputOutOfRangeException($"Input {n} is out of range. Error at line {lineNumber + 1}.");
                }

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
