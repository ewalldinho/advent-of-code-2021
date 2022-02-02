using System.IO;

namespace AdventOfCode.MainApp.Infrastructure
{
    public static class PuzzleData
    {
        public static string GetData(AdventDays day)
        {
            var textFilePath = $"Data/day-{(int)day:00}.txt";

            using var reader = new StreamReader(File.OpenRead(textFilePath));
            var dataString = reader.ReadToEnd();
            return dataString;
        }
    }
}
