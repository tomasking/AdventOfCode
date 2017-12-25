 using System;
 using System.Collections;
 using System.Collections.Generic;
 using System.Dynamic;
 using System.Linq;
 using System.Runtime.InteropServices;
 using Xunit.Abstractions;

namespace AdventOfCode.Day24
{
    public class Day24
    {
        private readonly ITestOutputHelper _output;

        public Day24(ITestOutputHelper output)
        {
            _output = output;
        }

        public Bridges GetBridges(List<Component> components)
        {
            Bridges bridges = new Bridges(components);
            
            
            List<Bridge> bridgesToIterate = new List<Bridge>(bridges._bridges);
            
            do
            {
                List<Bridge> newLot = new List<Bridge>();
               foreach (var bridge in bridgesToIterate)
                {
                   
                    newLot.AddRange(bridge.Iterate());
                }
                
                bridges._bridges.AddRange(newLot);
                bridgesToIterate = new List<Bridge>(newLot);
                
            } while (bridgesToIterate.Count > 0);

            return bridges;
        }
    }

    public class Bridges
    {
        public readonly List<Bridge> _bridges = new List<Bridge>();

        public Bridges(List<Component> components)
        {
            var zeroComponents = components.Where(c => c.Ports.Contains(0));
            foreach (var zeroComponent in zeroComponents)
            {
                _bridges.Add(new Bridge(0, new List<Component>(),  zeroComponent, components));
            }
        }
    }


    public class Bridge
    {
        public int Sum
        {
            get { return Components.Sum(c => c.Ports[0] + c.Ports[1]); }
        }

        public List<Component> Components { get; }

        public List<Component> ComponentsLeft { get; }

        private int sparePin = 0;
      
        public Bridge(int portToMatch, List<Component> startingComponents, Component zeroComponent, List<Component> remainingComponents)
        {
            Components = new List<Component>(startingComponents) {zeroComponent};
            sparePin = zeroComponent.Ports[0]==portToMatch ? zeroComponent.Ports[1] : zeroComponent.Ports[0];

            ComponentsLeft = new List<Component>(remainingComponents);
            ComponentsLeft.Remove(zeroComponent);
        }

        public bool CanAdd(Component bridgeComponent)
        {
            if (bridgeComponent.Ports.Contains(sparePin))
            {
                return true;
            }
            return false;
        }

        public List<Bridge> Iterate()
        {
            List<Bridge> newBridges = new List<Bridge>();
            foreach (var component in new List<Component>(ComponentsLeft))
            {
                if (CanAdd(component))
                {
                    newBridges.Add(new Bridge(sparePin, Components, component, ComponentsLeft));
                }
            }
           
            return newBridges;
        }
    }

    public class Component
    {
        public List<int> Ports { get; }

        public Component(int pins1, int pins2)
        {
            Ports = new List<int>() {pins1,pins2 };
        }

        public override string ToString()
        {
            return Ports[0] + "/" + Ports[1];
        }
    }
}

/*
The CPU itself is a large, black building surrounded by a bottomless pit. Enormous metal tubes extend outward from the side of the building at regular intervals and descend down into the void. There's no way to cross, but you need to get inside.

No way, of course, other than building a bridge out of the magnetic remainingComponents strewn about nearby.

Each component has two ports, one on each end. The ports come in all different types, and only matching types can be connected. You take an inventory of the remainingComponents by their port types (your puzzle input). Each port is identified by the number of pins it uses; more pins mean a stronger connection for your bridge. A 3/7 component, for example, has a type-3 port on one side, and a type-7 port on the other.

Your side of the pit is metallic; a perfect surface to connect a magnetic, zero-pin port. Because of this, the first port you use must be of type 0. It doesn't matter what type of port you end with; your goal is just to make the bridge as strong as possible.

The strength of a bridge is the sum of the port types in each component. For example, if your bridge is made of remainingComponents 0/3, 3/7, and 7/4, your bridge has a strength of 0+3 + 3+7 + 7+4 = 24.

For example, suppose you had the following remainingComponents:

0/2
2/2
2/3
3/4
3/5
0/1
10/1
9/10
With them, you could make the following valid bridges:

0/1
0/1--10/1
0/1--10/1--9/10
0/2
0/2--2/3
0/2--2/3--3/4
0/2--2/3--3/5
0/2--2/2
0/2--2/2--2/3
0/2--2/2--2/3--3/4
0/2--2/2--2/3--3/5
(Note how, as shown by 10/1, order of ports within a component doesn't matter. However, you may only use each port on a component once.)

Of these bridges, the strongest one is 0/1--10/1--9/10; it has a strength of 0+1 + 1+10 + 10+9 = 31.

What is the strength of the strongest bridge you can make with the remainingComponents you have available?

 */