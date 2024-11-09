namespace LabsLibrary
{
    public static class Lab1Helper
    {
        public static string[] ProcessLab1Inputs(string[] inputData)
        {
            // Call the method from Lab1
            return Lab1.Program.ProcessInputs(inputData);
        }
    }

    public static class Lab2Helper
    {
        public static string CountLab2Ways(int input)
        {
            // Call the method from Lab2
            var result = Lab2.Program.CountWays(input);
            return result.ToString();
        }
    }

    public static class Lab3Helper
    {
        public static string FindLab3ShortestPath(string[] inputData)
        {
            // Write the contents to a temporary file
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllLines(tempFilePath, inputData);

            // Call the method that expects a file path
            (int[,] adjacencyMatrix, int start, int end) = Lab3.Program.ReadInput(tempFilePath);
            var result = Lab3.Program.FindShortestPath(adjacencyMatrix, start, end);
            return result.ToString();
        }
    }
}