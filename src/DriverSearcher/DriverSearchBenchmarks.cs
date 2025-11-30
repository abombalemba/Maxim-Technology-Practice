using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

[MemoryDiagnoser]
[RankColumn]
public class DriverSearchBenchmarks {
    private List<Driver> _drivers = null!;
    private Client _client = null!;

    [GlobalSetup]
    public void Setup() {
        var random = new Random(42);
        _drivers = new List<Driver>();
        
        for (int i = 0; i < 1000; i++) {
            _drivers.Add(new Driver(i, random.Next(0, 1000), random.Next(0, 1000)));
        }
        
        _client = new Client(500, 500);
    }

    [Benchmark]
    public List<Driver> BruteForce() => BruteForceAlgorithm.FindNearestDrivers(_client, _drivers, 5);

    [Benchmark]
    public List<Driver> SpiralSearch() => SpiralSearchAlgorithm.FindNearestDrivers(_client, _drivers, 5);

    [Benchmark] 
    public List<Driver> ExpandingSquare() => ExpandingSquareAlgorithm.FindNearestDrivers(_client, _drivers, 5);
}