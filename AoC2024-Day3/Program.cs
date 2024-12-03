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

                int tmpCheck = CheckLineIsSafe(intList);
                if (tmpCheck == -1)
                {
                    safeLines++;
                }
                else
                {
                    List<int> tmpList = intList;
                    List<int> tmpList2 = intList;
                    List<int> tmpList3 = intList;

                    // remove first found issue and retry using the returned index and the next one
                    tmpList.RemoveAt(tmpCheck);
                    if (tmpCheck+1 < tmpList2.Count)
                    {
                        tmpList2.RemoveAt(tmpCheck + 1);
                    }
                    if (tmpCheck-1 < tmpList3.Count && tmpCheck-1 > 0)
                    {
                        tmpList3.RemoveAt(tmpCheck -1);
                    }
                    
                    if ((CheckLineIsSafe(intList) == -1) ||
                        (CheckLineIsSafe(tmpList2) == -1) || 
                        CheckLineIsSafe(tmpList3) == -1)
                    {
                        safeLines++;
                    }
                }
            }
            
            Console.WriteLine($"The number of safe lines is {safeLines}");
            
        }
    }

    static int CheckLineIsSafe(List<int> values)
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
        
        // get the initial sign
        int initialSign = Math.Sign(diffs[0]);

        // check bounds
        foreach (var (difference, index) in diffs.Select((value, index) => (value, index)))
        {
            // check if the signedness changes
            if (Math.Sign(difference) != initialSign) return index;
            if (Math.Abs(difference) > 3) return index;
            if (Math.Abs(difference) == 0) return index;
        }

        if (diffs.All(n => n > 0)) return -1;
        if (diffs.All(n => n < 0)) return -1;

        // is 0 a good value for fucked input?
        return 0;
    }
}

