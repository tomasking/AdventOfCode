using System;
using Xunit;
using AdventOfCode.Core;
using Shouldly;

namespace AdventOfCode.Core
{ 
    public class Day18Tests{
        
        [Fact]
        public void TestDoSomething()
        {
            Day18 day18 = new Day18();
            day18.DoSomething().ShouldBe(4);
        }
    }
}