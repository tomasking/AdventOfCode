namespace AdventOfCode.Day24
{
    using Shouldly;
    using Xunit;
    using System.Collections.Generic;
    using Tuple;

    public class Day24Tests{
        
        [Fact]
        public void TestDoSomething()
        {
            List<Tuple<int,int>> input = new List<Tuple<int, int>>()


            Day24 day24 = new Day24();
            day24.DoSomething().ShouldBe(3);
        }
    }
}