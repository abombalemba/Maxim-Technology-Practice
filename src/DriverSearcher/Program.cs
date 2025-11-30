using System;
using System.Collections.Generic;

class Program {
    static void Main(string[] args) {
        if (args.Length > 0 && args[0] == "benchmark") {
            Console.WriteLine("Running benchmarks...");
            var summary = BenchmarkDotNet.Running.BenchmarkRunner.Run<DriverSearchBenchmarks>();
        } else {
            Console.WriteLine("Driver Searcher - Testing Algorithms");
            
            var drivers = new List<Driver> {
                new Driver(1, 1, 1),
                new Driver(2, 5, 5),
                new Driver(3, 10, 10),
                new Driver(4, 2, 2),
                new Driver(5, 8, 8),
                new Driver(6, 3, 3),
                new Driver(7, 7, 7),
                new Driver(8, 0, 0),
                new Driver(9, 15, 15),
                new Driver(10, 4, 4)
            };
            
            var client = new Client(0, 0);
            
            TestAlgorithm("BruteForce", () => BruteForceAlgorithm.FindNearestDrivers(client, drivers, 3));
            TestAlgorithm("SpiralSearch", () => SpiralSearchAlgorithm.FindNearestDrivers(client, drivers, 3));
            TestAlgorithm("ExpandingSquare", () => ExpandingSquareAlgorithm.FindNearestDrivers(client, drivers, 3));
        }
    }
    
    static void TestAlgorithm(string name, Func<List<Driver>> algorithm) {
        var result = algorithm();
        Console.WriteLine($"\n{name}:");

        foreach (var driver in result) {
            Console.WriteLine($"  Driver {driver.Id} at ({driver.X}, {driver.Y})");
        }
    }
}