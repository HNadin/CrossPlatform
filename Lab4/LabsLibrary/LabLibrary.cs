using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LabsLibrary
{
    public static class LabLibrary
    {
        public static void RunLab(string lab, string inputFilePath, string outputFilePath)
        {
            // Копіювання вхідного файлу до заданого шляху
            string inputDestinationPath = $"../../{lab}/INPUT.txt";
            Console.WriteLine($"Input file copied to {inputDestinationPath}");
            File.Copy(inputFilePath, inputDestinationPath, overwrite: true);

            // Виконання команди Run для відповідної лабораторної роботи
            ExecuteCommands(lab, inputFilePath, outputFilePath);
        }

        public static void ExecuteCommands(string lab, string inputFilePath, string outputFilePath)
        {
            // Виконання команди Build
            BuildLab(lab);

            // Виконання команди Run
            Console.WriteLine($"Run {lab}");
            string runCommand = $"dotnet build ../../Build.proj -p:Solution={lab} -t:Run";
            RunCommand(runCommand);

            // Копіювання вихідного файлу з заданого шляху
            string outputSourcePath = $"../../{lab}/OUTPUT.txt";
            File.Copy(outputSourcePath, outputFilePath, overwrite: true);

            // Читання і обробка даних з файлу, якщо потрібно
            if (File.Exists(inputFilePath))
            {
                var inputData = File.ReadAllLines(inputFilePath);
                // Обробка даних для відповідної лабораторної (логіка може відрізнятися для кожної)
                File.WriteAllLines(outputFilePath, new[] { $"{lab} processed output data" });
                Console.WriteLine($"Data processed for {lab}. Output written to {outputFilePath}");
            }
            else
            {
                Console.WriteLine("Input file not found.");
            }

            // Виконання команди Test
            TestLab(lab);
        }

        public static void BuildLab(string lab)
        {
            // Виконання команди Build для відповідної лабораторної роботи
            Console.WriteLine($"Build {lab}");
            string buildCommand = $"dotnet build ../../Build.proj -p:Solution={lab} -t:Build";
            RunCommand(buildCommand);
        }

        public static void TestLab(string lab)
        {
            // Виконання команди Test для відповідної лабораторної роботи
            Console.WriteLine($"Test {lab}");
            string testCommand = $"dotnet build ../../Build.proj -p:Solution={lab} -t:Test";
            RunCommand(testCommand);
        }

        public static void RunCommand(string command)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ExecuteCommand("cmd.exe", $"/c {command}");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                ExecuteCommand("/bin/bash", $"-c \"{command}\"");
            }
            else
            {
                Console.WriteLine("Unsupported operating system");
            }
        }

        private static void ExecuteCommand(string shell, string arguments)
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = shell,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(processInfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                Console.WriteLine(output);

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine($"Error: {error}");
                }
            }
        }
    }

}
