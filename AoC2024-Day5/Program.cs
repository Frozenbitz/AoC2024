using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;


class AoC2024Day5
{
    static void Main()
    {
        
        Console.WriteLine("AOC 2024 Day 5");

        string filePath ="C:\\Users\\phell\\RiderProjects\\AoC2024\\AoC2024-Day5\\Inputs\\riddle.rmf";
        
        // generate the rulesets and the updates
        // the ruleset needs to be in a single list as well as the rules 
        // we can make a function to check each rule on a list of updates
        var inputs = createListsOfInputs(filePath);

        var rules = inputs.Item1;
        var updates = inputs.Item2;
        
        int finalSum = 0;

        foreach (var update in updates)
        {
            bool updateGood = checkSingleUpdate(update, rules);

            if (updateGood == false)
            {
                // we leave and do not add to the final sum
                continue;
            }

            // get the middle element of uneven lists and add up 
            finalSum += update[(update.Count / 2)];
            
        }
        
        
        // collect and report all findings 
        Console.WriteLine("we found: " + finalSum);
    }
    
    
    public static (string, string) ruleFilter(string line)
    {
        // use lookahead to find overlapping matches
        string pattern = "(?<page>\\d+)\\|(?<before>\\d+)";
        Match rules = Regex.Match(line, pattern);

        return (rules.Groups["page"].Value, rules.Groups["before"].Value);
    }
    
    public static List<int> updateFilter(string line)
    {
        string pattern = "(\\d+)";
        List<int> collectionOfUpdates = new List<int>();
        
        MatchCollection updates = Regex.Matches(line, pattern);
        foreach (Match update in updates)
        {
            collectionOfUpdates.Add(int.Parse(update.Value));
        }

        return collectionOfUpdates;
    }

    public static (List<List<int>>, List<List<int>>) createListsOfInputs(string filePath)
    {
        if (! File.Exists(filePath))
        {
            Console.WriteLine("The file could not be found");
            Environment.Exit(-1);
        }
        
        List<List<int>> rules = new List<List<int>>();
        List<List<int>> updates = new List<List<int>>();
        
        // Open the file and read it line by line into a list type 
        using (StreamReader reader = new StreamReader(filePath))
        {
            // create a list of lines containing all the current input
            string? line;
            int currentInLine = 0;
            while ((line = reader.ReadLine()) != null)
            { 
                // we need to stop, when we hit the empyt line and continue with the value list
                if (line.Length == 0) break;
                
                // generate a list of rules 
                rules.Add(new List<int>());
                var parsedLine = ruleFilter(line);
                rules[currentInLine].Add(int.Parse(parsedLine.Item1));
                rules[currentInLine].Add(int.Parse(parsedLine.Item2));
                currentInLine++;
            }
            
            while ((line = reader.ReadLine()) != null)
            { 
                // generate a list of rules 
                updates.Add(updateFilter(line));
            }
        }
        
        return (rules, updates);
    }

    public static (List<int>, List<int>) GenerateRulesPerPage(int page, List<List<int>> rules)
    {
        List<int>? checkBefore = new List<int>(); 
        List<int>? checkAfter = new List<int>();

        foreach (var rule in rules)
        {
            if (rule[0] == page)
            {
                checkAfter.Add(rule[1]);
            }
            
            if (rule[1] == page)
            {
                checkBefore.Add(rule[0]);
            }
        }

        // return both rules <check before, check after>
        return (checkBefore, checkAfter);
    }
    
    public static bool checkSingleUpdate(List<int> update, List<List<int>> rules)
    {
        bool updateGood = true;
        
        foreach (var (page, pageIndex) in update.Select((item, index) => (item, index)))
        {
            var ruleSet = GenerateRulesPerPage(page, rules);
            List<int> checkBefore = ruleSet.Item1;
            List<int> checkAfter = ruleSet.Item2;

            // run a check on both rule sets to see if we need to break
            for (int pageNo = 0; pageNo < pageIndex; pageNo++)
            {
                // these (before/after) need to be switched to detect outliers!
                if (checkAfter.Contains(update[pageNo]))
                {
                    updateGood = false;
                    break;
                }
            }

            for (int pageNo = pageIndex+1; pageNo < update.Count; pageNo++)
            {
                // these (before/after) need to be switched to detect outliers!
                if (checkBefore.Contains(update[pageNo]))
                {
                    updateGood = false;
                    break;
                }
            }
        }
        
        return updateGood;
    }
}
