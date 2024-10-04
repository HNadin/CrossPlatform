using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {
            // Перевірка наявності файлу
            if (!File.Exists("Lab3/INPUT.txt"))
            {
                Console.WriteLine("Error: INPUT.txt file not found.");
                return;
            }

            // Читання вмісту файлу
            string[] lines = File.ReadAllLines("Lab3/INPUT.txt");

            // Перевірка, чи є хоч якісь дані у файлі
            if (lines.Length < 3)
            {
                Console.WriteLine("Error: Not enough data in INPUT.txt.");
                return;
            }

            // Читання кількості вершин
            if (!int.TryParse(lines[0], out int n) || n <= 0)
            {
                Console.WriteLine("Error: Invalid number of vertices.");
                return;
            }

            int[,] adjacencyMatrix = new int[n, n]; // Матриця суміжності
            int[] distance = new int[n];  // Масив відстаней

            // Заповнення матриці суміжності
            for (int i = 0; i < n; i++)
            {
                string[] row = lines[i + 1].Split();
                if (row.Length != n)
                {
                    Console.WriteLine($"Error: Invalid row length at line {i + 2}.");
                    return;
                }

                for (int j = 0; j < n; j++)
                {
                    if (!int.TryParse(row[j], out adjacencyMatrix[i, j]))
                    {
                        Console.WriteLine($"Error: Invalid number at line {i + 2}, column {j + 1}.");
                        return;
                    }
                }
            }

            // Читання початкової та кінцевої вершин
            string[] endpoints = lines[n + 1].Split();
            if (endpoints.Length != 2 || !int.TryParse(endpoints[0], out int start) || !int.TryParse(endpoints[1], out int end))
            {
                Console.WriteLine("Error: Invalid start or end vertex.");
                return;
            }

            start--;  // Перетворюємо у 0-індекс
            end--;    // Перетворюємо у 0-індекс

            // Перевірка правильності індексів
            if (start < 0 || start >= n || end < 0 || end >= n)
            {
                Console.WriteLine("Error: Start or end vertex out of bounds.");
                return;
            }

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

            // Перевірка результату та виведення в консоль
            if (distance[end] < int.MaxValue)
            {
                Console.WriteLine($"Shortest distance from {start + 1} to {end + 1}: {distance[end]}");
            }
            else
            {
                Console.WriteLine($"No path from {start + 1} to {end + 1}");
            }

            // Спроба запису результату в файл
            try
            {
                using (StreamWriter writer = new StreamWriter("Lab3/OUTPUT.txt"))
                {
                    if (distance[end] < int.MaxValue)
                    {
                        writer.WriteLine(distance[end]);  // Виводимо коротший шлях
                    }
                    else
                    {
                        writer.WriteLine(-1);  // Якщо шляху немає
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to OUTPUT.txt: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
