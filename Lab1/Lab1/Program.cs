using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "Lab1/INPUT.txt";   // Путь к файлу ввода
            string outputFilePath = "Lab1/OUTPUT.txt"; // Путь к файлу вывода

            try
            {
                // Проверка существования INPUT.txt
                if (!File.Exists(inputFilePath))
                {
                    throw new FileNotFoundException($"File error: '{inputFilePath}' not found.");
                }

                string[] inputs;

                // Попытка чтения файла и обработка потенциальных ошибок
                try
                {
                    inputs = File.ReadAllLines(inputFilePath);
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
                    throw new IOException($"File error: Unable to read file '{inputFilePath}'.", ex);
                }

                // Проверка, что файл не пустой
                if (inputs.Length == 0)
                {
                    throw new FormatException("File is empty.");
                }

                // Запись результатов в OUTPUT.txt
                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    foreach (string input in inputs)
                    {
                        if (int.TryParse(input.Trim(), out int n))
                        {
                            // Проверка, что n в допустимом диапазоне
                            if (n < 2 || n >= 32)
                            {
                                writer.WriteLine($"Input {n} is out of range.");
                                continue;
                            }

                            BigInteger sum = 0;

                            // Подсчет различных салатов
                            for (int i = 2; i <= n; i++)
                            {
                                sum += Combinations(n, i);
                            }

                            writer.WriteLine(sum); // Запись суммы в выходной файл
                        }
                        else
                        {
                            writer.WriteLine($"Input '{input}' is not a valid integer."); // Сообщение об ошибке
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Функция для вычисления факториала
        static BigInteger Factorial(int n)
        {
            BigInteger f = 1;
            for (int i = 1; i <= n; i++)
                f *= i;
            return f;
        }

        // Функция для вычисления комбинаций C(n, r) = n! / (r! * (n-r)!)
        static BigInteger Combinations(int n, int r)
        {
            return Factorial(n) / (Factorial(r) * Factorial(n - r));
        }
    }
}
