using System;
using System.IO;
using Xunit;
using Lab1;

namespace Lab1.Test
{
    public class UnitTest1
    {
        // Test valid input for ProcessInputs
        [Fact]
        public void ValidInput()
        {
            string[] inputs = { "3", "4", "5" }; // Valid inputs

            string[] results = Program.ProcessInputs(inputs);

            string[] expectedOutput = { "4", "11", "26" }; // Expected output for inputs 3, 4, and 5
            Assert.Equal(expectedOutput, results);
        }

        // Additional valid input tests
        [Fact]
        public void ValidInputWithLargerNumbers()
        {
            string[] inputs = { "6", "7" }; // Valid inputs

            string[] results = Program.ProcessInputs(inputs);

            string[] expectedOutput = { "57", "120" }; // Expected output for inputs 6 and 7
            Assert.Equal(expectedOutput, results);
        }

        [Fact]
        public void ValidInputWithEdgeCases()
        {
            string[] inputs = { "2", "8" }; // Valid inputs

            string[] results = Program.ProcessInputs(inputs);

            string[] expectedOutput = { "1", "247" }; // Expected output for inputs 2 and 8
            Assert.Equal(expectedOutput, results);
        }

        // Test out-of-range input for ProcessInputs
        [Fact]
        public void OutOfRangeInput()
        {
            string[] inputs = { "1", "33" }; // Out-of-range inputs

            var exception = Assert.Throws<InputOutOfRangeException>(() => Program.ProcessInputs(inputs));
            Assert.Contains("Input 1 is out of range", exception.Message);
        }

        // Test invalid input for ProcessInputs
        [Fact]
        public void InvalidInput()
        {
            string[] inputs = { "abc" }; // Invalid input

            var exception = Assert.Throws<InvalidInputException>(() => Program.ProcessInputs(inputs));
            Assert.Contains("Input 'abc' is not a valid integer", exception.Message);
        }

        // Test empty input for ProcessInputs
        [Fact]
        public void EmptyInput()
        {
            string[] inputs = Array.Empty<string>(); // Empty input

            var exception = Assert.Throws<FormatException>(() => Program.ProcessInputs(inputs));
            Assert.Equal("File is empty.", exception.Message);
        }

        // Test reading non-existent file
        [Fact]
        public void FileNotFound()
        {
            string inputFilePath = "NonExistentFile.txt"; // File does not exist

            var exception = Assert.Throws<FileNotFoundException>(() => Program.ReadInputFile(inputFilePath));
            Assert.Contains($"File error: '{inputFilePath}' not found.", exception.Message);
        }
    }
}
