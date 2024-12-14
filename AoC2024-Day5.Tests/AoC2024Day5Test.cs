using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoC2024_Day5.Tests;

[TestClass]
[TestSubject(typeof(AoC2024Day5))]
public class AoC2024Day5Test
{

    [TestMethod]
    public void runRuleTests()
    {
        
        {
            var testMatrix = new List<List<int>>
            {
                new List<int> { 75, 47},
                new List<int> { 75, 61 },
            };
            
            // 75,47,61
            var test = AoC2024Day5.GenerateRulesPerPage(75, testMatrix);
            var checkBefore = test.Item1;
            var checkAfter = test.Item2;
            
            Assert.AreEqual(47, checkAfter[0]);
            Assert.AreEqual(61, checkAfter[1]);
        }
        
        {
            var testMatrix = new List<List<int>>
            {
                new List<int> { 75, 47},
                new List<int> { 47, 61 },
            };
            
            // 75,47,61
            var test = AoC2024Day5.GenerateRulesPerPage(47, testMatrix);
            var checkBefore = test.Item1;
            var checkAfter = test.Item2;
            
            Assert.AreEqual(61, checkAfter[0]);
            Assert.AreEqual(75, checkBefore[0]);
        }
        
        {
            var testMatrix = new List<List<int>>
            {
                new List<int> { 75, 47 },
                new List<int> { 75, 61 },
            };

            var update = new List<int> { 75, 47, 61 };
            Assert.AreEqual(true, AoC2024Day5.checkSingleUpdate(update, testMatrix));
        }
        
        {
            var testMatrix = new List<List<int>>
            {
                new List<int> { 75, 47 },
                new List<int> { 47, 61 },
                new List<int> { 47, 53 },
                new List<int> { 47, 29 },
            };

            var update = new List<int> { 75, 47, 61, 53, 29 };
            Assert.AreEqual(true, AoC2024Day5.checkSingleUpdate(update, testMatrix));
        }
        
        {
            var testMatrix = new List<List<int>>
            {
                new List<int> { 75, 47 },
                new List<int> { 47, 61 },
                new List<int> { 47, 53 },
                new List<int> { 47, 29 },
                new List<int> { 97, 75 },
            };

            var update = new List<int> { 75, 97, 47, 61, 53 };
            Assert.AreEqual(false, AoC2024Day5.checkSingleUpdate(update, testMatrix));
        }
    }
}