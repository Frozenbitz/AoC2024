using System;
using System.IO;

class Program
{
    static void Main()
    {
        
        Console.WriteLine("AOC 2024 Day 2");

        string filePath ="C:\\Users\\phell\\RiderProjects\\AoC2024\\AoC2024-Day2\\Inputs\\riddle.rmf";
        if (! File.Exists(filePath))
        {
            Console.WriteLine("The file could not be found");
            Environment.Exit(-1);
        }
        
        // Open the file and read it line by line
        using (StreamReader reader = new StreamReader(filePath))
        {
            string? line;
            int safeLines = 0;

            while ((line = reader.ReadLine()) != null)
            {
                // opening the provided buffer for testing 
                string[] splitLine = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                 
                // we need to cast from strings to int, for processing
                List<int> intList = splitLine.Select(int.Parse).ToList();
                
                if (CheckLineIsSafe(intList))
                {
                    safeLines++;
                }
            }
            
            Console.WriteLine($"The number of safe lines is {safeLines}");
            
        }
    }

    static bool CheckLineIsSafe(List<int> values)
    {
        // create adjacent pairs using array.zip 
        // creates touples? of values where we should be able to iterate over  the pairs and compare both
        var adjacentPairs = values.Zip(values.Skip(1), (current, next) => (current, next));
        
        // collect the diffs 
        List<int> diffs = new List<int>();
        foreach (var value in adjacentPairs)
        {
            diffs.Add(value.current - value.next);
        }

        // check bounds
        foreach (var difference in diffs)
        {
            if (Math.Abs(difference) > 3) return false;
            if (Math.Abs(difference) == 0) return false;
        }

        if (diffs.All(n => n > 0)) return true;
        if (diffs.All(n => n < 0)) return true;

        return false;
    }
}

