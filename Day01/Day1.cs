namespace AdventOfCode.Day01
{
    using System.Linq;

    public class Day1
    {
        //1122 produces a sum of 3 (1 + 2) because the first digit(1) matches the second digit and the third digit(2) matches the fourth digit.
        //1111 produces 4 because each digit (all 1) matches the next.
        //1234 produces 0 because no digit matches the next.
        //91212129 produces 9 because the only digit that matches the next one is the last digit, 9.

        public int Calculate(string input)
        {
            var numbers = input.ToCharArray().Select(i => int.Parse(i.ToString())).ToList();

            int total = 0;
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                if (numbers[i] == numbers[i + 1])
                {
                    total += numbers[i];
                }
            }

            if (numbers[numbers.Count - 1] == numbers[0])
            {
                total += numbers[0];
            }

            return total;
        }
    }
}
