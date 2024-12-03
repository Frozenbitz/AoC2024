using System;
using System.IO;
using System.Text.RegularExpressions;


class Program
{
    static void Main()
    {
        
        Console.WriteLine("AOC 2024 Day 3");

        string filePath ="C:\\Users\\phell\\RiderProjects\\AoC2024\\AoC2024-Day3\\Inputs\\riddle.rmf";
        if (! File.Exists(filePath))
        {
            Console.WriteLine("The file could not be found");
            Environment.Exit(-1);
        }
        
        // Open the file and read it line by line
        using (StreamReader reader = new StreamReader(filePath))
        {
            string? line;
            string pattern = @"(mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\))"; // a valid something looks like this: mul(a,b)
            int multiValue = 0;
            bool mulsEnabled = true;
            
            while ((line = reader.ReadLine()) != null)
            {

                var matches = Regex.Matches(line, pattern);
                foreach (Match match in matches)
                {
                    if (match.Value == "do()")
                    {
                        mulsEnabled = true;
                        continue;
                    }
                    
                    if (match.Value == "don't()")
                    {
                        mulsEnabled = false;
                        continue;
                    }

                    if (mulsEnabled)
                    {
                        GroupCollection groups = match.Groups;
                        multiValue += int.Parse(groups[2].Value) * int.Parse(groups[3].Value); 
                    }
                }
            }
            
            Console.WriteLine($"Uncorrupted Muls:  {multiValue}");
            
        }
    }
}

