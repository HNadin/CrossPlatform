using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    static void Main()
    {
        string inputFilePath = Path.Combine("Lab3", "INPUT.txt");   // Path to the input file
        string outputFilePath = Path.Combine("Lab3", "OUTPUT.txt"); // Path to the output file

        try
        {
            (int[,] adjacencyMatrix, int start, int end) = ReadInput(inputFilePath);
            int shortestDistance = FindShortestPath(adjacencyMatrix, start, end);
            WriteOutput(outputFilePath, shortestDistance);

            // Display success message in console
            Console.WriteLine($"Shortest distance from vertex {start + 1} to vertex {end + 1} has been successfully written to '{outputFilePath}': {shortestDistance}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    public static (int[,] adjacencyMatrix, int start, int end) ReadInput(string filePath)
    {
        // Check for file existence
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("INPUT.txt file not found.");
        }

        // Read file contents
        string[] lines = File.ReadAllLines(filePath);

        // Check for sufficient data in the file
        if (lines.Length < 3)
        {
            throw new InvalidOperationException("Not enough data in INPUT.txt.");
        }

        // Read the number of vertices
        if (!int.TryParse(lines[0], out int n) || n <= 0)
        {
            throw new FormatException("Invalid number of vertices.");
        }

        int[,] adjacencyMatrix = new int[n, n]; // Adjacency matrix

        // Fill the adjacency matrix
        for (int i = 0; i < n; i++)
        {
            string[] row = lines[i + 1].Split();
            if (row.Length != n)
            {
                throw new FormatException($"Invalid row length at line {i + 2}.");
            }

            for (int j = 0; j < n; j++)
            {
                if (!int.TryParse(row[j], out adjacencyMatrix[i, j]))
                {
                    throw new FormatException($"Invalid number at line {i + 2}, column {j + 1}.");
                }
            }
        }

        // Read start and end vertices
        string[] endpoints = lines[n + 1].Split();
        if (endpoints.Length != 2 || !int.TryParse(endpoints[0], out int start) || !int.TryParse(endpoints[1], out int end))
        {
            throw new FormatException("Invalid start or end vertex.");
        }

        start--;  // Convert to 0-index
        end--;    // Convert to 0-index

        // Validate index correctness
        if (start < 0 || start >= n || end < 0 || end >= n)
        {
            throw new ArgumentOutOfRangeException("Start or end vertex out of bounds.");
        }

        return (adjacencyMatrix, start, end);
    }

    public static int FindShortestPath(int[,] adjacencyMatrix, int start, int end)
    {
        int n = adjacencyMatrix.GetLength(0);
        int[] distance = new int[n];  // Distance array

        // Initialize the distance array
        for (int i = 0; i < n; i++)
        {
            distance[i] = int.MaxValue;
        }

        distance[start] = 0;  // Distance to start vertex
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(start);  // Begin search

        // Breadth-First Search (BFS)
        while (queue.Count > 0)
        {
            int i = queue.Dequeue();  // Get the first vertex from the queue

            // Visit neighbors
            for (int j = 0; j < n; j++)
            {
                if (adjacencyMatrix[i, j] != 0 && distance[j] > distance[i] + 1)
                {
                    distance[j] = distance[i] + 1;
                    queue.Enqueue(j);  // Add neighbor to the queue
                }
            }
        }

        // Return the shortest distance or -1 if no path found
        return distance[end] < int.MaxValue ? distance[end] : -1;
    }

    public static void WriteOutput(string filePath, int shortestDistance)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(shortestDistance);  // Write the shortest path
            }
        }
        catch (Exception ex)
        {
            throw new IOException($"Error writing to OUTPUT.txt: {ex.Message}");
        }
    }
}
