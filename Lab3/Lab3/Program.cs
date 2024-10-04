using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    static void Main()
    {
        string inputFilePath = "Lab3/INPUT.txt";
        string outputFilePath = "Lab3/OUTPUT.txt";

        try
        {
            (int[,] adjacencyMatrix, int start, int end) = ReadInput(inputFilePath);
            int shortestDistance = FindShortestPath(adjacencyMatrix, start, end);
            WriteOutput(outputFilePath, shortestDistance);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    public static (int[,] adjacencyMatrix, int start, int end) ReadInput(string filePath)
    {
        // Перевірка наявності файлу
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("INPUT.txt file not found.");
        }

        // Читання вмісту файлу
        string[] lines = File.ReadAllLines(filePath);

        // Перевірка, чи є достатньо даних у файлі
        if (lines.Length < 3)
        {
            throw new InvalidOperationException("Not enough data in INPUT.txt.");
        }

        // Читання кількості вершин
        if (!int.TryParse(lines[0], out int n) || n <= 0)
        {
            throw new FormatException("Invalid number of vertices.");
        }

        int[,] adjacencyMatrix = new int[n, n]; // Матриця суміжності

        // Заповнення матриці суміжності
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

        // Читання початкової та кінцевої вершин
        string[] endpoints = lines[n + 1].Split();
        if (endpoints.Length != 2 || !int.TryParse(endpoints[0], out int start) || !int.TryParse(endpoints[1], out int end))
        {
            throw new FormatException("Invalid start or end vertex.");
        }

        start--;  // Перетворюємо у 0-індекс
        end--;    // Перетворюємо у 0-індекс

        // Перевірка правильності індексів
        if (start < 0 || start >= n || end < 0 || end >= n)
        {
            throw new ArgumentOutOfRangeException("Start or end vertex out of bounds.");
        }

        return (adjacencyMatrix, start, end);
    }

    public static int FindShortestPath(int[,] adjacencyMatrix, int start, int end)
    {
        int n = adjacencyMatrix.GetLength(0);
        int[] distance = new int[n];  // Масив відстаней

        // Ініціалізація масиву відстаней
        for (int i = 0; i < n; i++)
        {
            distance[i] = int.MaxValue;
        }

        distance[start] = 0;  // Відстань до початкової вершини
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(start);  // Початок пошуку

        // Пошук в ширину (BFS)
        while (queue.Count > 0)
        {
            int i = queue.Dequeue();  // Отримуємо першу вершину з черги

            // Обхід сусідів
            for (int j = 0; j < n; j++)
            {
                if (adjacencyMatrix[i, j] != 0 && distance[j] > distance[i] + 1)
                {
                    distance[j] = distance[i] + 1;
                    queue.Enqueue(j);  // Додаємо сусіда до черги
                }
            }
        }

        // Повертаємо коротшу відстань або -1, якщо шлях не знайдено
        return distance[end] < int.MaxValue ? distance[end] : -1;
    }

    public static void WriteOutput(string filePath, int shortestDistance)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(shortestDistance);  // Виводимо коротший шлях
            }
        }
        catch (Exception ex)
        {
            throw new IOException($"Error writing to OUTPUT.txt: {ex.Message}");
        }
    }
}
