using System;
using System.IO;
using Xunit;
using Lab1;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Lab1.Test
{
    public class UnitTest1
    {
        // Test valid input for ProcessInputs
        [Fact]
        public void ValidInput()
        {
            // Arrange
            string[] inputs = { "3", "4", "5" }; // Valid inputs

            // Act
            string[] results = Program.ProcessInputs(inputs);

            // Assert
            string[] expectedOutput = { "4", "11", "26" }; // Expected output for inputs 3, 4, and 5
            Assert.Equal(expectedOutput, results);
        }

        // Test out-of-range input for ProcessInputs
        [Fact]
        public void OutOfRangeInput()
        {
            // Arrange
            string[] inputs = { "1", "33" }; // Out-of-range inputs

            // Act & Assert
            var exception = Assert.Throws<InputOutOfRangeException>(() => Program.ProcessInputs(inputs));
            Assert.Contains("Input 1 is out of range", exception.Message);
        }

        // Test invalid input for ProcessInputs
        [Fact]
        public void InvalidInput()
        {
            // Arrange
            string[] inputs = { "abc" }; // Invalid input

            // Act & Assert
            var exception = Assert.Throws<InvalidInputException>(() => Program.ProcessInputs(inputs));
            Assert.Contains("Input 'abc' is not a valid integer", exception.Message);
        }

        // Test empty input for ProcessInputs
        [Fact]
        public void EmptyInput()
        {
            // Arrange
            string[] inputs = Array.Empty<string>(); // Empty input

            // Act & Assert
            var exception = Assert.Throws<FormatException>(() => Program.ProcessInputs(inputs));
            Assert.Equal("File is empty.", exception.Message);
        }

        // Test reading non-existent file
        [Fact]
        public void FileNotFound()
        {
            // Arrange
            string inputFilePath = "NonExistentFile.txt"; // File does not exist

            // Act & Assert
            var exception = Assert.Throws<FileNotFoundException>(() => Program.ReadInputFile(inputFilePath));
            Assert.Contains($"File error: '{inputFilePath}' not found.", exception.Message);
        }
    }
}
