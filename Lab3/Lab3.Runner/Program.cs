using Lab3;

namespace Lab3.Runner
{
    public class Program
    {
        static void Main()
        {
            string inputFilePath = Path.Combine("Lab3","../INPUT.txt");   // Path to the input file
            string outputFilePath = Path.Combine("Lab3","../OUTPUT.txt"); // Path to the output file

            try
            {
                // Виклик методів із бібліотеки Lab3
                (int[,] adjacencyMatrix, int start, int end) = Lab3.Program.ReadInput(inputFilePath);
                int shortestDistance = Lab3.Program.FindShortestPath(adjacencyMatrix, start, end);
                Lab3.Program.WriteOutput(outputFilePath, shortestDistance);

                // Виведення результату в консоль
                Console.WriteLine($"Shortest distance from vertex {start + 1} to vertex {end + 1} has been successfully written to '{outputFilePath}': {shortestDistance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}