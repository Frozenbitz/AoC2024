using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoC2024_Day4.Tests;

[TestClass]
[TestSubject(typeof(AoC2024Day4))]
public class KernelTests
{

    [TestMethod]
    public void runKernel()
    {
        // test some basic and overlapping horizontal kernels
        {
            string kernelToTest1 = ".SAMXMS...";
            Assert.AreEqual(1, AoC2024Day4.horizontalKernelFilter(kernelToTest1));
        }

        {
            string kernelToTest2 = ".SAMXMAS...";
            Assert.AreEqual(2, AoC2024Day4.horizontalKernelFilter(kernelToTest2));
        }

        {
            string kernelToTest3 = ".SAMXMASAMX...";
            Assert.AreEqual(3, AoC2024Day4.horizontalKernelFilter(kernelToTest3));
        }

        {
            var validMatrix = new List<List<char>>
            {
                new List<char> { 'a', 'b', 'c', 'd' },
                new List<char> { 'e', 'f', 'g', 'h' },
                new List<char> { 'i', 'j', 'k', 'l' },
                new List<char> { 'm', 'n', 'o', 'p' }
            };
            string localMatrix = "C:\\Users\\phell\\RiderProjects\\AoC2024\\AoC2024-Day4\\Inputs\\testingMatrix.txt";
            List<List<char>> matrix = AoC2024Day4.generateCharMatrix(localMatrix);

            foreach (var (line, lineIndex) in validMatrix.Select((item, index) => (item, index)))
            {
                foreach (var (token, index) in line.Select((item, index) => (item, index)))
                {
                    Assert.AreEqual(token, matrix[lineIndex][index]);
                }
            }
        }

        {
            // check transpose 
            var validMatrix = new List<List<char>>
            {
                new List<char> { 'a', 'e', 'i', 'm' },
                new List<char> { 'b', 'f', 'j', 'n' },
                new List<char> { 'c', 'g', 'k', 'o' },
                new List<char> { 'd', 'h', 'l', 'p' }
            };
            string localMatrix = "C:\\Users\\phell\\RiderProjects\\AoC2024\\AoC2024-Day4\\Inputs\\testingMatrix.txt";
            List<List<char>> matrix = AoC2024Day4.generateCharMatrix(localMatrix);
            matrix = AoC2024Day4.transposeCharMatrix(matrix);

            foreach (var (line, lineIndex) in validMatrix.Select((item, index) => (item, index)))
            {
                foreach (var (token, index) in line.Select((item, index) => (item, index)))
                {
                    Assert.AreEqual(token, matrix[lineIndex][index]);
                }
            }
        }
        
        
        {
            var testMatrix = new List<List<char>>
            {
                new List<char> { 'X', '.', '.', '.' },
                new List<char> { '.', 'M', '.', '.' },
                new List<char> { '.', '.', 'A', '.' },
                new List<char> { '.', '.', '.', 'S' }
            };
            Assert.AreEqual(1, AoC2024Day4.boxKernelFilter(testMatrix));    
        }
        
        {
            var testMatrix = new List<List<char>>
            {
                new List<char> { '.', '.', '.', 'S' },
                new List<char> { '.', '.', 'A', '.' },
                new List<char> { '.', 'M', '.', '.' },
                new List<char> { 'X', '.', '.', '.' }
            };
            Assert.AreEqual(1, AoC2024Day4.boxKernelFilter(testMatrix));    
        }
        
        {
            var testMatrix = new List<List<char>>
            {
                new List<char> { 'S', '.', '.', 'X' },
                new List<char> { '.', 'A', 'M', '.' },
                new List<char> { '.', 'A', 'M', '.' },
                new List<char> { 'S', '.', '.', 'X' }
            };
            Assert.AreEqual(2, AoC2024Day4.boxKernelFilter(testMatrix));    
        }

        {
            string filePath ="C:\\Users\\phell\\RiderProjects\\AoC2024\\AoC2024-Day4\\Inputs\\testSample.txt";
            List<List<char>> letterMap = AoC2024Day4.generateCharMatrix(filePath);
            Assert.AreEqual(1, AoC2024Day4.SlidingWindowFilter(letterMap, 4));
        }

        {
            var testMatrix = new List<List<char>>
            {
                new List<char> { 'M', '.', 'S' },
                new List<char> { '.', 'A', '.' },
                new List<char> { 'M', '.', 'S' },
            };
            Assert.AreEqual(1, AoC2024Day4.masKernelFilter(testMatrix));    
        }
        
        {
            var testMatrix = new List<List<char>>
            {
                new List<char> { 'M', '.', 'M' },
                new List<char> { '.', 'A', '.' },
                new List<char> { 'S', '.', 'S' },
            };
            Assert.AreEqual(1, AoC2024Day4.masKernelFilter(testMatrix));    
        }
        
        {
            var testMatrix = new List<List<char>>
            {
                new List<char> { 'S', '.', 'M' },
                new List<char> { '.', 'A', '.' },
                new List<char> { 'S', '.', 'M' },
            };
            Assert.AreEqual(1, AoC2024Day4.masKernelFilter(testMatrix));    
        }
        
        
        {
            var testMatrix = new List<List<char>>
            {
                new List<char> { 'S', '.', 'S' },
                new List<char> { '.', 'A', '.' },
                new List<char> { 'M', '.', 'M' },
            };
            Assert.AreEqual(1, AoC2024Day4.masKernelFilter(testMatrix));    
        }

    }
}