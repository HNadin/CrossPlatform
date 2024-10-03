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

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string inputFilePath = "Lab1/INPUT.txt";   // Path to the input file
            string outputFilePath = "Lab1/OUTPUT.txt"; // Path to the output file

            try
            {
                // Check if INPUT.txt exists
                if (!File.Exists(inputFilePath))
                {
                    throw new FileNotFoundException($"File error: '{inputFilePath}' not found.");
                }

                string[] inputs;

                // Attempt to read the file and handle potential errors
                try
                {
                    inputs = File.ReadAllLines(inputFilePath, Encoding.UTF8); // Change to Encoding.GetEncoding(1251) if necessary
                }
                catch (UnauthorizedAccessException ex)
                {
                    throw new UnauthorizedAccessException($"File error: Access to the file '{inputFilePath}' is denied.", ex);
                }
                catch (PathTooLongException ex)
                {
                    throw new PathTooLongException($"File error: The file path '{inputFilePath}' is too long.", ex);
                }
                catch (IOException ex)
                {
                    throw new IOException($"File error: Unable to read the file '{inputFilePath}'.", ex);
                }

                // Check if the file is not empty
                if (inputs.Length == 0)
                {
                    throw new FormatException("File is empty.");
                }

                // Write the results to OUTPUT.txt
                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    for (int lineNumber = 0; lineNumber < inputs.Length; lineNumber++)
                    {
                        string input = inputs[lineNumber];
                        if (int.TryParse(input.Trim(), out int n))
                        {
                            // Check if n is within the allowed range
                            if (n < 2 || n >= 32)
                            {
                                // Throw an exception for out-of-range values with line number
                                throw new InputOutOfRangeException($"Input {n} is out of range. Error at line {lineNumber + 1}.");
                            }

                            BigInteger sum = 0;

                            // Calculate the different combinations
                            for (int i = 2; i <= n; i++)
                            {
                                sum += Combinations(n, i);
                            }

                            writer.WriteLine(sum); // Write the sum to the output file
                        }
                        else
                        {
                            // Throw an exception for invalid input with line number
                            throw new InvalidInputException($"Input '{input}' is not a valid integer. Error at line {lineNumber + 1}.");
                        }
                    }
                }
            }
            catch (InputOutOfRangeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
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
