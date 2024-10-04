using System;
using System.IO;
using Xunit;

public class ProgramTests
{
    [Fact]
    public void Test_SuccessfulPath()
    {
        // Using a string array for input
        string[] input = {
            "3",
            "0 1 0",
            "1 0 1",
            "0 1 0",
            "1 3"
        };

        string directoryPath = "Lab3";
        string inputFilePath = Path.Combine(directoryPath, "INPUT.txt");

        // Ensure the directory exists
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Write input data to the file
        File.WriteAllLines(inputFilePath, input);

        // Read data from the file and find the shortest path
        (int[,] adjacencyMatrix, int start, int end) = Program.ReadInput(inputFilePath);
        int result = Program.FindShortestPath(adjacencyMatrix, start, end);

        Assert.Equal(2, result);  // Expect the shortest path to equal 2
    }

    [Fact]
    public void Test_NoPath()
    {
        // Using a string array for input
        string[] input = {
            "3",
            "0 0 0",
            "0 0 0",
            "0 0 0",
            "1 3"
        };

        string directoryPath = "Lab3";
        string inputFilePath = Path.Combine(directoryPath, "INPUT.txt");

        // Ensure the directory exists
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Write input data to the file
        File.WriteAllLines(inputFilePath, input);

        // Read data from the file and find the shortest path
        (int[,] adjacencyMatrix, int start, int end) = Program.ReadInput(inputFilePath);
        int result = Program.FindShortestPath(adjacencyMatrix, start, end);

        Assert.Equal(-1, result);  // Expect no path (-1)
    }

    [Fact]
    public void Test_StartEqualsEnd()
    {
        // Using a string array for input
        string[] input = {
            "3",
            "0 1 0",
            "1 0 1",
            "0 1 0",
            "2 2"
        };

        string directoryPath = "Lab3";
        string inputFilePath = Path.Combine(directoryPath, "INPUT.txt");

        // Ensure the directory exists
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Write input data to the file
        File.WriteAllLines(inputFilePath, input);

        // Read data from the file and find the shortest path
        (int[,] adjacencyMatrix, int start, int end) = Program.ReadInput(inputFilePath);
        int result = Program.FindShortestPath(adjacencyMatrix, start, end);

        Assert.Equal(0, result);  // Expect the distance to itself to equal 0
    }

    [Fact]
    public void Test_InvalidVertexCount()
    {
        // Using a string array for input
        string[] input = {
            "abc",  // Invalid number of vertices
            "0 1",
            "1 0",
            "1 2"
        };

        string directoryPath = "Lab3"; // Ensure the path to the directory is correct
        string inputFilePath = Path.Combine(directoryPath, "INPUT.txt");

        // Ensure the directory exists
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Write input data to the file
        File.WriteAllLines(inputFilePath, input);

        // Ensure an exception is thrown when reading the input file
        var exception = Assert.Throws<FormatException>(() => Program.ReadInput(inputFilePath));

        // Optionally check the exception message if necessary
        Assert.Contains("Invalid number of vertices", exception.Message);
    }

    [Fact]
    public void Test_FileNotFound()
    {
        Assert.Throws<FileNotFoundException>(() => Program.ReadInput("NonExistentFile.txt"));
    }
}
