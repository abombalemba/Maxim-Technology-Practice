using BenchmarkDotNet.Running;
using System;

namespace DriverSearcher.Benchmarks {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Running Driver Search Benchmarks...");
            var summary = BenchmarkRunner.Run<DriverSearchBenchmarks>();
            Console.WriteLine("Benchmarks completed!");
        }
    }
}