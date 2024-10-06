using System;
using System.IO;
using Xunit;

public class ProgramTests
{
    private string RunTest(string[] inputLines)
    {
        // Write a temporary file with input data
        string tempInputFilePath = Path.GetTempFileName();
        File.WriteAllLines(tempInputFilePath, inputLines);

        // Read data from the temporary file
        (int[,] adjacencyMatrix, int start, int end) = Program.ReadInput(tempInputFilePath);

        // Process the graph to find the shortest path
        int result = Program.FindShortestPath(adjacencyMatrix, start, end);

        // Delete the temporary file
        File.Delete(tempInputFilePath);

        // Return the result as a string
        return result.ToString(); // Convert the integer result to a string
    }

    [Fact]
    public void Test_SuccessfulPath()
    {
        // Using a string array for input data
        string[] input = {
            "3",
            "0 1 0",
            "1 0 1",
            "0 1 0",
            "1 3"
        };

        // Act: Run the test
        string result = RunTest(input);

        Assert.Equal("2", result);  // Expect the shortest path to equal "2" as a string
    }

    [Fact]
    public void Test_NoPath()
    {
        // Using a string array for input data
        string[] input = {
            "3",
            "0 0 0",
            "0 0 0",
            "0 0 0",
            "1 3"
        };

        // Act: Run the test
        string result = RunTest(input);

        Assert.Equal("-1", result);  // Expect no path (-1) as a string
    }

    [Fact]
    public void Test_StartEqualsEnd()
    {
        // Using a string array for input data
        string[] input = {
            "3",
            "0 1 0",
            "1 0 1",
            "0 1 0",
            "2 2"
        };

        // Act: Run the test
        string result = RunTest(input);

        Assert.Equal("0", result);  // Expect the distance to itself to equal "0" as a string
    }

    [Fact]
    public void Test_InvalidVertexCount()
    {
        // Using a string array for input data
        string[] input = {
            "abc",  // Invalid number of vertices
            "0 1",
            "1 0",
            "1 2"
        };

        // Act: Run the test and capture the exception
        var exception = Assert.Throws<FormatException>(() => RunTest(input));

        // Check the exception message if necessary
        Assert.Contains("Invalid number of vertices", exception.Message);
    }

    [Fact]
    public void Test_FileNotFound()
    {
        Assert.Throws<FileNotFoundException>(() => Program.ReadInput("NonExistentFile.txt"));
    }
}
