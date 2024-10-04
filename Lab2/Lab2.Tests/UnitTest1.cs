namespace Lab2.Tests
{
    public class UnitTest1
    {
        // Test valid input for CountWays
        [Fact]
        public void ValidInput()
        {
            int input = 7; // Valid input

            int result = Program.CountWays(input);

            int expectedOutput = 1; // Expected output for input 7
            Assert.Equal(expectedOutput, result);
        }

        [Fact]
        public void ValidInputWithMultipleValues()
        {
            int[] inputs = { 7, 10, 15, 20 }; // Multiple valid inputs
            int[] expectedOutputs = { 1, 2, 1, 4 }; // Example expected results for each input

            for (int i = 0; i < inputs.Length; i++)
            {
                int result = Program.CountWays(inputs[i]);
                Assert.Equal(expectedOutputs[i], result);
            }
        }

        // Test minimal valid input
        [Fact]
        public void MinimalValidInput()
        {
            int input = 3; // Minimal valid input

            int result = Program.CountWays(input);

            Assert.Equal(1, result); // Expected result is 1, since exactly 3 items can be chosen
        }

        // Test maximum valid input (boundary test)
        [Fact]
        public void MaxValidInput()
        {
            int input = int.MaxValue; // Large number input to test boundaries

            int result = Program.CountWays(input);

            Assert.True(result > 0); // Check that the result is valid and greater than 0
        }

        // Test negative input for CountWays (invalid input)
        [Fact]
        public void NegativeInput()
        {
            int input = -5; // Negative input (invalid case)

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Program.CountWays(input));
            Assert.Contains("between 1 and 2147483647.", exception.Message);
        }

        // Test zero input for CountWays (invalid input)
        [Fact]
        public void ZeroInput()
        {
            int input = 0; // Zero input (invalid case)

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Program.CountWays(input));
            Assert.Contains("between 1 and 2147483647.", exception.Message);
        }

    }
}