using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        // Read input from file
        string[] lines = File.ReadAllLines("Lab3/INPUT.txt");
        int n = int.Parse(lines[0]);  // Number of vertices in the graph
        int[,] adjacencyMatrix = new int[n, n]; // Adjacency matrix
        int[] distance = new int[n];  // Distance array

        // Fill the adjacency matrix
        for (int i = 0; i < n; i++)
        {
            string[] row = lines[i + 1].Split();
            for (int j = 0; j < n; j++)
            {
                adjacencyMatrix[i, j] = int.Parse(row[j]);
            }
        }

        string[] endpoints = lines[n + 1].Split();
        int start = int.Parse(endpoints[0]) - 1;  // Convert to zero-based index
        int end = int.Parse(endpoints[1]) - 1;    // Convert to zero-based index

        // Initialize the distance array with a large number (like infinity)
        for (int i = 0; i < n; i++)
        {
            distance[i] = int.MaxValue;
        }

        distance[start] = 0;  // Starting point has distance 0

        Queue<int> queue = new Queue<int>();
        queue.Enqueue(start);  // Start traversal from the start node

        // Breadth-first search (BFS)
        while (queue.Count > 0)
        {
            int i = queue.Dequeue();  // Get the first vertex from the queue

            // Explore neighbors
            for (int j = 0; j < n; j++)
            {
                if (adjacencyMatrix[i, j] != 0 && distance[j] > distance[i] + 1)
                {
                    distance[j] = distance[i] + 1;
                    queue.Enqueue(j);  // Add neighbor to the queue
                }
            }
        }

        // Check if the path was found and print to console
        if (distance[end] < int.MaxValue)
        {
            Console.WriteLine($"Shortest distance from {start + 1} to {end + 1}: {distance[end]}");
        }
        else
        {
            Console.WriteLine($"No path from {start + 1} to {end + 1}");
        }

        // Write result to output file
        using (StreamWriter writer = new StreamWriter("Lab3/OUTPUT.txt"))
        {
            if (distance[end] < int.MaxValue)
            {
                writer.WriteLine(distance[end]);  // Print shortest distance to the target vertex
            }
            else
            {
                writer.WriteLine(-1);  // If there's no path
            }
        }
    }
}
