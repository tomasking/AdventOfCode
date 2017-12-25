using System;
using System.Diagnostics;
using System.IO;
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

            _day24 = new Day24(output);
        }

        [Fact]
        public void OneComponentBridges()
        {
            var bridges = _day24.GetBridges(_components);

            OutputBridges(bridges._bridges);
        }


        [Fact]
        public void FinalTest()
        {
            string[] input = File.ReadAllLines("./day24/day24.txt").ToArray();
            List<Component> components = new List<Component>();

            foreach (var line in input)
            {
                var numbers = line.Split('/');
                components.Add(new Component(int.Parse(numbers[0]), int.Parse(numbers[1])));
            }
            
            var bridges = _day24.GetBridges(components);

           OutputBridges(bridges._bridges);
        }

        [Fact]
        public void FinalTestPartB()
        {
            string[] input = File.ReadAllLines("./day24/day24.txt").ToArray();
            List<Component> components = new List<Component>();

            foreach (var line in input)
            {
                var numbers = line.Split('/');
                components.Add(new Component(int.Parse(numbers[0]), int.Parse(numbers[1])));
            }

            var bridges = _day24.GetBridges(components)._bridges;

            var longestLength = 0;
            List<Bridge> longestBridges = new List<Bridge>();
            foreach (var bridge in bridges)
            {
                if (bridge.Components.Count > longestLength)
                {
                    longestLength = bridge.Components.Count;
                    longestBridges =new List<Bridge>() {bridge};
                }else if (bridge.Components.Count == longestLength)
                {
                    longestBridges.Add(bridge);
                }

            }
            foreach (var longestBridge in longestBridges.OrderByDescending(s=>s.Sum))
            {
                var s = string.Join("--", longestBridge.Components) + "  Sum: " + longestBridge.Sum;
                Output(s);

            }
        }


        private void OutputBridges(List<Bridge> bridges)
        {
            var topSum = 0;
            Bridge topBridge = null;
            foreach (var bridge in bridges)
            {
                if (bridge.Sum > topSum)
                {
                    topSum = bridge.Sum;
                    topBridge = bridge;
                }    
                
            }
            var s = string.Join("--", topBridge.Components) + "  Sum: " + topBridge.Sum;
            Output(s);
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