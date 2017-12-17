using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode
{
    /*
     This spinlock's algorithm is simple but efficient, quickly consuming everything in its path. It starts with a circular buffer containing only the value 0, 
which it marks as the current position. It then steps forward through the circular buffer some number of steps (your puzzle input) before inserting the 
first new value, 1, after the value it stopped on. The inserted value becomes the current position. Then, it steps forward from there the same number of 
steps, and wherever it stops, inserts after it the second new value, 2, and uses that as the new current position again.

It repeats this process of stepping forward, inserting a new value, and using the location of the inserted value as the new current position a total 
of 2017 times, inserting 2017 as its final operation, and ending with a total of 2018 values (including 0) in the circular buffer.

For example, if the spinlock were to step 3 times per insert, the circular buffer would begin to evolve like this (using parentheses to mark the current 
position after each iteration of the algorithm):

(0), the initial state before any insertions.
0 (1): the spinlock steps forward three times (0, 0, 0), and then inserts the first value, 1, after it. 1 becomes the current position.
0 (2) 1: the spinlock steps forward three times (0, 1, 0), and then inserts the second value, 2, after it. 2 becomes the current position.
0  2 (3) 1: the spinlock steps forward three times (1, 0, 2), and then inserts the third value, 3, after it. 3 becomes the current position.
And so on:

0  2 (4) 3  1
0 (5) 2  4  3  1
0  5  2  4  3 (6) 1
0  5 (7) 2  4  3  6  1
0  5  7  2  4  3 (8) 6  1
0 (9) 5  7  2  4  3  8  6  1
Eventually, after 2017 insertions, the section of the circular buffer near the last insertion looks like this:

1512  1134  151 (2017) 638  1513  851
Perhaps, if you can identify the value that will ultimately be after the last value written (2017), you can short-circuit the spinlock. In this example, that would be 638.

What is the value after 2017 in your completed circular buffer?

        Puzzle input is 316
     */
    public class Day17
    {
        private readonly ITestOutputHelper output;
        private Stopwatch _stopwatch;

        public Day17(ITestOutputHelper output)
        {
  
            this.output = output;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public List<int> Calculate(int input, int numberOfTurns)
        {          
            var currentRow = new List<int>(numberOfTurns+1) {0};

            int currentPosition = 0;
           
            for (int i = 1; i < numberOfTurns+1; i++)
            {
                currentRow = DoLine(input, ref currentPosition, i, currentRow);
            }
            return currentRow;
        }

        public List<int> DoLine(int puzzleInput, ref int currentPosition, int turnNumber, List<int> currentRow)
        {
            if (turnNumber % 500000 == 0)
            {
                output.WriteLine(turnNumber.ToString() + " " + _stopwatch.Elapsed.TotalSeconds.ToString());
                _stopwatch.Reset();
                _stopwatch.Start();
            }

            for (int i = 0; i < puzzleInput; i++)
            {
                currentPosition = currentPosition + 1;
                if (currentPosition == currentRow.Count)
                {
                    currentPosition = 0;
                }
            }

            
            currentRow.Insert(currentPosition+1, turnNumber);
            currentPosition++;
            return currentRow;
        }

        [Fact]
        public void Main()
        {
            /* List<List<int>> expectedResult = new List<List<int>>()
             {
                 new List<int>() {0},
                new List<int>() {0, 1},
                new List<int>() {0, 2, 1},
                new List<int> {0, 2, 3, 1},
                new List<int> {0, 2, 4, 3, 1},
                 new List<int> {0, (5), 2, 4, 3, 1},
                 new List<int> {0, 5, 2, 4, 3, (6), 1},
                 new List<int> {0, 5, (7), 2, 4, 3, 6, 1},
                 new List<int> {0, 5, 7, 2, 4, 3, (8), 6, 1},
                 new List<int> {0, (9), 5, 7, 2, 4, 3, 8, 6, 1}
             };*/

            //var result = Calculate(3, expectedResult.Count-1);
            // result.ShouldBe(expectedResult);
            var result = Calculate(316, 2017);
            FinalResult(result, 2017).ShouldBe(180);

            //return;
            result = Calculate(316, 50000000);
            
            var finalResult = FinalResult(result, 0);

            finalResult.ShouldBe(180);
            // result.ShouldBe(expectedResult);
        }

        private int FinalResult(List<int> result, int numberToLookFor)
        {
            var finalResult = -1;
            for (int i = 0; i < result.Count - 1; i++)
            {
                if (result[i] == numberToLookFor)
                {
                    finalResult = result[i + 1];
                    break;
                }
            }

            output.WriteLine("Final Result: " + finalResult.ToString());

            return finalResult;
        }
    }
}
