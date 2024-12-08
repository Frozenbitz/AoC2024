using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoC2024_Day4.Tests;

[TestClass]
[TestSubject(typeof(Program))]
public class KernelTests
{

    [TestMethod]
    public void runKernel()
    {
        // test some basic and overlapping horizontal kernels
        {
            string kernelToTest1 = ".SAMXMS...";
            Assert.AreEqual(1, Program.horizontalKernelFilter(kernelToTest1));
        }

        {
            string kernelToTest2 = ".SAMXMAS...";
            Assert.AreEqual(2, Program.horizontalKernelFilter(kernelToTest2));
        }

        {
            string kernelToTest3 = ".SAMXMASAMX...";
            Assert.AreEqual(3, Program.horizontalKernelFilter(kernelToTest3));
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
            List<List<char>> matrix = Program.generateCharMatrix(localMatrix);

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
            List<List<char>> matrix = Program.generateCharMatrix(localMatrix);
            matrix = Program.transposeCharMatrix(matrix);

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
            Assert.AreEqual(1, Program.boxKernelFilter(testMatrix));    
        }
        
        {
            var testMatrix = new List<List<char>>
            {
                new List<char> { '.', '.', '.', 'S' },
                new List<char> { '.', '.', 'A', '.' },
                new List<char> { '.', 'M', '.', '.' },
                new List<char> { 'X', '.', '.', '.' }
            };
            Assert.AreEqual(1, Program.boxKernelFilter(testMatrix));    
        }
        
        {
            var testMatrix = new List<List<char>>
            {
                new List<char> { 'S', '.', '.', 'X' },
                new List<char> { '.', 'A', 'M', '.' },
                new List<char> { '.', 'A', 'M', '.' },
                new List<char> { 'S', '.', '.', 'X' }
            };
            Assert.AreEqual(2, Program.boxKernelFilter(testMatrix));    
        }

        {
            string filePath ="C:\\Users\\phell\\RiderProjects\\AoC2024\\AoC2024-Day4\\Inputs\\testSample.txt";
            List<List<char>> letterMap = Program.generateCharMatrix(filePath);
            Assert.AreEqual(1, Program.SlidingWindowFilter(letterMap, 4));
        }

        {
            var testMatrix = new List<List<char>>
            {
                new List<char> { 'M', '.', 'S' },
                new List<char> { '.', 'A', '.' },
                new List<char> { 'M', '.', 'S' },
            };
            Assert.AreEqual(1, Program.masKernelFilter(testMatrix));    
        }
        
        {
            var testMatrix = new List<List<char>>
            {
                new List<char> { 'M', '.', 'M' },
                new List<char> { '.', 'A', '.' },
                new List<char> { 'S', '.', 'S' },
            };
            Assert.AreEqual(1, Program.masKernelFilter(testMatrix));    
        }
        
        {
            var testMatrix = new List<List<char>>
            {
                new List<char> { 'S', '.', 'M' },
                new List<char> { '.', 'A', '.' },
                new List<char> { 'S', '.', 'M' },
            };
            Assert.AreEqual(1, Program.masKernelFilter(testMatrix));    
        }
        
        
        {
            var testMatrix = new List<List<char>>
            {
                new List<char> { 'S', '.', 'S' },
                new List<char> { '.', 'A', '.' },
                new List<char> { 'M', '.', 'M' },
            };
            Assert.AreEqual(1, Program.masKernelFilter(testMatrix));    
        }

    } 
}