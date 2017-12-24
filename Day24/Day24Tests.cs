using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Schema;
using Xunit.Abstractions;

namespace AdventOfCode.Day24
{
    using Shouldly;
    using Xunit;
    using System.Collections.Generic;

    public class Day24Tests
    {
        private readonly ITestOutputHelper output;
        private List<Component> _components;
        private Day24 _day24;

        public Day24Tests(ITestOutputHelper output)
        {
            this.output = output;
            _components = new List<Component>();
            _components.Add(new Component(0, 2));
            _components.Add(new Component(2, 2));
            _components.Add(new Component(2, 3));
            _components.Add(new Component(3, 4));
            _components.Add(new Component(3, 5));
            _components.Add(new Component(0, 1));
            _components.Add(new Component(10, 1));
            _components.Add(new Component(9, 10));

            _day24 = new Day24();
        }

        [Fact]
        public void OneComponentBridges()
        {
            var bridges = _day24.GetBridges(_components);

            OutputBridges(bridges.AllBridges);
        }

        private void OutputBridges(List<Bridge> bridges)
        {
            foreach (var bridge in bridges)
            {
                var s = string.Join("--", bridge.Components);
                Output(s);
            }
        }

        private void Output(string message)
        {
            if (output == null)
            {
                Console.WriteLine(message);
            }
            else
            {
                output.WriteLine(message);
            }
        }
    }

}