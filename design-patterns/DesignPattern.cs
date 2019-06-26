using design_patterns.Abstractions;
using System;

namespace design_patterns
{
    abstract class DesignPattern : IDesignPattern
    {
        public abstract string PatternName { get; } 

        public virtual void ExecuteSample()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"{PatternName}");
            Console.WriteLine("--------------------------------");
        }
    }
}
