using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;


class AoC2024Day4
{
    static void Main()
    {
        
        Console.WriteLine("AOC 2024 Day 4");

        string filePath ="C:\\Users\\phell\\RiderProjects\\AoC2024\\AoC2024-Day4\\Inputs\\riddle.rmf";
        
        // what we need
        List<List<char>> letterMap = generateCharMatrix(filePath);
        int numberOfXmas = 0;

        
        // there are 3 steps to filter the right amount of words here: 
        // if we use a kernel for filtering, we will get horizontal entries for each kernel run
        // this means we will get more results than we need
        // what we need to do is search for every instance that is horizontal 
        // and then apply filtering kernel to get the rest.
            
        // step 1: get all horizontal kernels per line and find XMAS and SAMX
        // foreach (var lineOfChar in letterMap)
        // {
        //     string line = "";
        //     foreach (var character in lineOfChar)
        //     {
        //         line += character.ToString();
        //     }
        //     numberOfXmas += horizontalKernelFilter(line);
        // }
        
        // for vertical lines we need to shift the input around and call the same filter for horizontal
        // List<List<char>> shiftedLetterMap = transposeCharMatrix(letterMap);
        // foreach (var lineOfChar in shiftedLetterMap)
        // {
        //     string line = "";
        //     foreach (var character in lineOfChar)
        //     {
        //         line += character.ToString();
        //     }
        //     numberOfXmas += horizontalKernelFilter(line);
        // }
        
        // step 2: build a 4x4 matrix to iterate and run the filter kernels over each possible 4x4 matrix
        // try to match each possible diagonal or reversed combination
        numberOfXmas += SlidingWindowFilterMAS(letterMap);

        // collect and report all findings 
        Console.WriteLine("we found: " + numberOfXmas);
    }

    public static int horizontalKernelFilter(string line)
    {
        // use lookahead to find overlapping matches
        string pattern = "(?=(XMAS))|(?=(SAMX))";
        MatchCollection matches = Regex.Matches(line, pattern);

        return matches.Count();
    }

    public static int boxKernelFilter(List<List<char>> matrix)
    {
        
        if (matrix.Count != 4 || matrix.Exists(row => row.Count != 4))
        {
            throw new ArgumentException("The parameter must be a 4x4 matrix.");
        }
        
        // we need to filter the current kernels: 
        // X..X S..S
        // .MM. .AA.
        // .AA. .MM.
        // S..S X..X
        int numberOfXmas = 0;
        if ((matrix[0][0] == 'X') &&
            (matrix[1][1] == 'M') &&
            (matrix[2][2] == 'A') &&
            (matrix[3][3] == 'S'))
        {
            numberOfXmas++;
        }
        if ((matrix[0][0] == 'S') &&
            (matrix[1][1] == 'A') &&
            (matrix[2][2] == 'M') &&
            (matrix[3][3] == 'X'))
        {
            numberOfXmas++;
        }
        
        if ((matrix[0][3] == 'S') &&
            (matrix[1][2] == 'A') &&
            (matrix[2][1] == 'M') &&
            (matrix[3][0] == 'X'))
        {
            numberOfXmas++;
        }
        
        if ((matrix[0][3] == 'X') &&
            (matrix[1][2] == 'M') &&
            (matrix[2][1] == 'A') &&
            (matrix[3][0] == 'S'))
        {
            numberOfXmas++;
        }
        
        return numberOfXmas;
    }
    
    public static int masKernelFilter(List<List<char>> matrix)
    {
        
        if (matrix.Count != 3 || matrix.Exists(row => row.Count != 3))
        {
            throw new ArgumentException("The parameter must be a 3x3 matrix.");
        }
        
        int numberOfXmas = 0;
        if ((matrix[0][0] == 'M') &&
            (matrix[1][1] == 'A') &&
            (matrix[2][2] == 'S') &&
            (matrix[2][0] == 'M') &&
            (matrix[0][2] == 'S') )
        {
            numberOfXmas++;
        }
        if ((matrix[0][0] == 'M') &&
            (matrix[1][1] == 'A') &&
            (matrix[2][2] == 'S') &&
            (matrix[0][2] == 'M') &&
            (matrix[2][0] == 'S') )
        {
            numberOfXmas++;
        }
        
        if ((matrix[0][2] == 'M') &&
            (matrix[1][1] == 'A') &&
            (matrix[2][0] == 'S') &&
            (matrix[2][2] == 'M') &&
            (matrix[0][0] == 'S') )
        {
            numberOfXmas++;
        }
        
        if ((matrix[2][2] == 'M') &&
            (matrix[1][1] == 'A') &&
            (matrix[0][0] == 'S') &&
            (matrix[2][0] == 'M') &&
            (matrix[0][2] == 'S'))
        {
            numberOfXmas++;
        }
        
        return numberOfXmas;
    }

    public static List<List<char>> generateCharMatrix(string filePath)
    {
        if (! File.Exists(filePath))
        {
            Console.WriteLine("The file could not be found");
            Environment.Exit(-1);
        }
        
        List<List<char>> letterMap = new List<List<char>>();
        
        // Open the file and read it line by line into a list type 
        using (StreamReader reader = new StreamReader(filePath))
        {
            // create a list of lines containing all the current input
            string? line;
            int currentLine = 0;
            while ((line = reader.ReadLine()) != null)
            {
                // add an empty list of chars for the next run ...
                letterMap.Add(new List<char>());
                foreach (var character in line)
                {
                    letterMap[currentLine].Add(character);    
                }
                currentLine++;
            }
        }
        
        return letterMap;
    }
    
    public static List<List<char>> transposeCharMatrix(List<List<char>> input)
    {
        List<List<char>> letterMap = input;
        List<List<char>> output = new List<List<char>>();

        // keep the dimensions from lettermap and create a new container 
        foreach (var line in letterMap)
        {
            output.Add(new List<char>(new char[line.Count]));
        }
        
        // Open the file and read it line by line into a list type 
        foreach (var (line, lineIndex) in letterMap.Select((item, index) => (item, index)))
        {
            foreach (var (token, index) in line.Select((item, index) => (item, index)))
            {
                // mix up the indices here to create transpose
                output[index][lineIndex] = token;
            }
        }
        
        return output;
    }
    
    public static int SlidingWindowFilter(List<List<char>> matrix, int windowSize)
    {
        // get the current dimensions
        int rows = matrix.Count;
        int cols = matrix[0].Count;
        int numberOfXmas = 0;

        for (int i = 0; i <= rows - windowSize; i++)
        {
            for (int j = 0; j <= cols - windowSize; j++)
            {
                
                // create a new window 4x4
                var window = new List<List<char>>();
                for (int c = 0; c < windowSize; c++)
                {
                    window.Add(new List<char>(new char[windowSize]));
                }
                
                for (int wi = 0; wi < windowSize; wi++)
                {
                    for (int wj = 0; wj < windowSize; wj++)
                    {
                        window[wi][wj] = matrix[i + wi][j + wj];
                    }
                }
                
                numberOfXmas += boxKernelFilter(window);
            }
        }
        
        return numberOfXmas;
    }
    
    public static int SlidingWindowFilterMAS(List<List<char>> matrix, int windowSize = 3)
    {
        // get the current dimensions
        int rows = matrix.Count;
        int cols = matrix[0].Count;
        int numberOfXmas = 0;

        for (int i = 0; i <= rows - windowSize; i++)
        {
            for (int j = 0; j <= cols - windowSize; j++)
            {
                
                // create a new window 4x4
                var window = new List<List<char>>();
                for (int c = 0; c < windowSize; c++)
                {
                    window.Add(new List<char>(new char[windowSize]));
                }
                
                for (int wi = 0; wi < windowSize; wi++)
                {
                    for (int wj = 0; wj < windowSize; wj++)
                    {
                        window[wi][wj] = matrix[i + wi][j + wj];
                    }
                }
                
                numberOfXmas += masKernelFilter(window);
            }
        }
        
        return numberOfXmas;
    }
}
