using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using LabsLibrary;
using System.Runtime.InteropServices;

namespace Lab4
{
    [Command(Name = "Lab4", Description = "Console app for labs")]
    [Subcommand(typeof(VersionCommand), typeof(RunCommand), typeof(SetPathCommand))]
    class Program
    {
        static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            Console.WriteLine("Specify a command to execute. Available commands:");
            Console.WriteLine("  version - Displays app version and author information.");
            Console.WriteLine("  run     - Run a specified lab. Requires a lab name (lab1, lab2, or lab3).");
            Console.WriteLine("           Optional parameters:");
            Console.WriteLine("             -I | --input   Input file path.");
            Console.WriteLine("             -o | --output  Output file path.");
            Console.WriteLine("  set-path - Set the input/output path for files.");
            Console.WriteLine("             Requires the path parameter (-p | --path).");
        }
    }

    [Command(Name = "version", Description = "Displays app version and author")]
    class VersionCommand
    {
        private void OnExecute()
        {
            Console.WriteLine("Author: Nadiia Chaban");
            Console.WriteLine("Version: 1.0.3");
        }
    }

    [Command(Name = "run", Description = "Run specified lab.")]
    class RunCommand
    {
        [Argument(0, Description = "Lab to run (lab1, lab2, lab3)")]
        [Required]
        public string Lab { get; set; } = string.Empty;

        [Option("-I|--input", "Input file path", CommandOptionType.SingleValue)]
        public string InputFile { get; set; } = string.Empty;

        [Option("-o|--output", "Output file path", CommandOptionType.SingleValue)]
        public string OutputFile { get; set; } = string.Empty;

        private void OnExecute()
        {
            if (string.IsNullOrEmpty(Lab))
            {
                Console.WriteLine("Error: Lab project not specified.");
                return;
            }

            string labPath = GetLabDirectory(Lab);

            if (string.IsNullOrEmpty(labPath))
            {
                Console.WriteLine($"Error: Lab project '{Lab}' not found in project directory.");
                return;
            }

            string inputFilePath;

            // Assign the input file path with a priority to InputFile
            if (string.IsNullOrEmpty(InputFile))
                inputFilePath = Path.Combine(Environment.GetEnvironmentVariable("LAB_PATH"), "INPUT.txt") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "INPUT.txt");
            else
                inputFilePath = InputFile;

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Input file not found.");
                return;
            }

            string outputFilePath = !string.IsNullOrEmpty(OutputFile) ? OutputFile : Path.Combine(Environment.GetEnvironmentVariable("LAB_PATH"), "OUTPUT.txt");

            Console.WriteLine($"Running {Lab} with input file: {inputFilePath}, output file: {outputFilePath}");

            switch (Lab.ToLower())
            {
                case "lab1":
                    LabLibrary.RunLab("Lab1", inputFilePath, outputFilePath);
                    break;
                case "lab2":
                    LabLibrary.RunLab("Lab2", inputFilePath, outputFilePath);
                    break;
                case "lab3":
                    LabLibrary.RunLab("Lab3", inputFilePath, outputFilePath);
                    break;
                default:
                    Console.WriteLine("Unknown lab specified. Use 'lab1', 'lab2', or 'lab3'.");
                    break;
            }
        }

        private string? GetLabDirectory(string labName)
        {
            string projectRoot = Directory.GetCurrentDirectory();
            return labName.ToLower() switch
            {
                "lab1" => Path.Combine(projectRoot, "Lab1"),
                "lab2" => Path.Combine(projectRoot, "Lab2"),
                "lab3" => Path.Combine(projectRoot, "Lab3"),
                _ => null,
            };
        }
    }

    [Command(Name = "set-path", Description = "Set input/output path")]
    class SetPathCommand
    {
        [Option("-p|--path", "Path to input/output files", CommandOptionType.SingleValue)]
        [Required]
        public string Path { get; set; } = string.Empty;

        private void OnExecute()
        {
            if (string.IsNullOrEmpty(Path))
            {
                Console.WriteLine("Please specify a path.");
                return;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("Setting LAB_PATH environment variable for Windows.");
                Environment.SetEnvironmentVariable("LAB_PATH", Path, EnvironmentVariableTarget.User);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                SetPathForLinux();
            }
            else
            {
                Console.WriteLine("Unsupported operating system");
            }
        }

        private void SetPathForLinux()
        {
            string shellConfigFile = GetShellConfigFile();
            if (string.IsNullOrEmpty(shellConfigFile))
            {
                Console.WriteLine("Could not determine the shell configuration file.");
                return;
            }

            try
            {
                Console.WriteLine($"Adding LAB_PATH to {shellConfigFile}.");
                string exportCommand = $"export LAB_PATH=\"{Path}\"";

                // Append export command to shell configuration file
                File.AppendAllText(shellConfigFile, $"{Environment.NewLine}{exportCommand}{Environment.NewLine}");

                Console.WriteLine("Path successfully added. Please restart your terminal or run 'source ~/.bashrc' (or equivalent) to apply changes.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update {shellConfigFile}: {ex.Message}");
            }
        }

        private string GetShellConfigFile()
        {
            string shell = Environment.GetEnvironmentVariable("SHELL");
            if (string.IsNullOrEmpty(shell))
            {
                return string.Empty;
            }

            if (shell.EndsWith("bash"))
            {
                return (Environment.GetEnvironmentVariable("HOME") ?? "~") + "/.bashrc";
            }
            else if (shell.EndsWith("zsh"))
            {
                return (Environment.GetEnvironmentVariable("HOME") ?? "~") + ".zshrc";
            }
            else
            {
                Console.WriteLine("Unsupported shell. Supported shells are Bash and Zsh.");
                return string.Empty;
            }
        }
    }
}
