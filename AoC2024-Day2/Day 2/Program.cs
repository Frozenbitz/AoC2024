using System;
using System.IO;

class Program
{
    static void Main()
    {
        
        Console.WriteLine("AOC 2024 Day 1");

        string filePath ="C:\\Users\\phell\\RiderProjects\\AoC2024\\AoC2024-Day1\\Inputs\\sample.txt";
        if (! File.Exists(filePath))
        {
            Console.WriteLine("The file could not be found");
            Environment.Exit(-1);
        }
        
        // Open the file and read it line by line
        using (StreamReader reader = new StreamReader(filePath))
        {
            string? line;
            List<int> leftList = new List<int>();
            List<int> rightList = new List<int>();

            while ((line = reader.ReadLine()) != null)
            {
                // opening the provided buffer for testing 
                string[] values = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                leftList.Add(int.Parse(values[0]));
                rightList.Add(int.Parse(values[1]));
            }

            // sorting both lists and comparing their input
            leftList.Sort();
            rightList.Sort();
            
            List<int> diff = new List<int>();

            for (int i = 0; i < Math.Min(leftList.Count, rightList.Count); i++)
            {
                diff.Add(Math.Abs(leftList[i] - rightList[i])); 
            }

            // here we need to get the similarity score 
            int similarityScore = 0;
            foreach (var candidate in leftList)
            {
                similarityScore += candidate * rightList.Count(n => n == candidate);
            }
            Console.WriteLine($"Similarity Score: {similarityScore}");   

        }
    }
    
    
}

