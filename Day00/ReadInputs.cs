﻿namespace Day00
{
    public static class ReadInputs
    {
        public static IEnumerable<int> ReadInts() =>
            Read()
                .TakeWhile(x => x != null)
                .Select(x => int.Parse(x!));

        public static IEnumerable<string?> Read()
        {
            var args = Environment.GetCommandLineArgs();
            var inputFile = args.Length > 1 ? args[1] : string.Empty;
            if (!string.IsNullOrEmpty(inputFile))
            {
                foreach (var line in File.ReadAllLines(inputFile))
                {
                    yield return line;
                }
            }
            else
            {
                while (true)
                    yield return Console.ReadLine();
            }
        }
    }
}