namespace AdventOfCode.Day18
{
    using Shouldly;
    using Xunit;

    public class Day18Tests{
        
        [Fact]
        public void TestDoSomething()
        {
            Day18 day18 = new Day18();
            day18.DoSomething().ShouldBe(3);
        }
    }
}