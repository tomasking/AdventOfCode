﻿namespace AdventOfCode.Day02
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Shouldly;
    using Xunit;

    /*
     * As you walk through the door, a glowing humanoid shape yells in your direction. "You there! Your state appears to be idle. 
     * Come help us repair the corruption in this spreadsheet - if we take another millisecond, we'll have to display an hourglass cursor!"
        The spreadsheet consists of rows of apparently-random numbers. To make sure the recovery process is on the right track, 
        they need you to calculate the spreadsheet's checksum. For each row, determine the difference between the largest value 
        and the smallest value; the checksum is the sum of all of these differences.

        For example, given the following spreadsheet:

        5 1 9 5
        7 5 3
        2 4 6 8
        The first row's largest and smallest values are 9 and 1, and their difference is 8.
        The second row's largest and smallest values are 7 and 3, and their difference is 4.
        The third row's difference is 6.
        In this example, the spreadsheet's checksum would be 8 + 4 + 6 = 18.

        What is the checksum for the spreadsheet in your puzzle input?
     */
    public class Day2b
    {
        public int Calculate(string[] input)
        {
            int total = 0;
            foreach (var line in input)
            {
                total += Difference(line);
            }

            return total;
        }

        private int Difference(string line)
        {
            List<int> numbers = line.Split(new []{' ', '\t'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            for (int i = 0; i < numbers.Count-1; i++)
            {
                for (int j = i+1; j < numbers.Count; j++)
                {
                    if (numbers[i] % numbers[j] == 0) return numbers[i] / numbers[j];
                    if (numbers[j] % numbers[i] == 0) return numbers[j] / numbers[i];
                }
            }

            throw new Exception();
        }

        [Fact]
        public void FirstOne()
        {
            string[] input = new[] {"5 9 2 8","9 4 7 3","3 8 6 5"};

            var result = Calculate(input);
            result.ShouldBe(9);
        }

        [Fact]
        public void MainBit()
        {
            string[] input = File.ReadAllLines("./day2input.txt");

            var result = Calculate(input);
            result.ShouldBe(250);
        }
    }
}
