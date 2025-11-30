using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;

[SimpleJob(RuntimeMoniker.Net50)]
[MemoryDiagnoser]
[RankColumn]
public class DriverSearchBenchmarks {
    private List<Driver> _drivers;
    private Client _client;

    [GlobalSetup]
    public void Setup() {
        var random = new Random(42);
        _drivers = new List<Driver>();
        
        for (int i = 0; i < 1000; i++) {
            _drivers.Add(new Driver(
                i,
                random.Next(0, 1000),
                random.Next(0, 1000)
            ));
        }
        
        _client = new Client(500, 500);
    }

    [Benchmark]
    public List<Driver> BruteForce() {
        return BruteForceAlgorithm.FindNearestDrivers(_client, _drivers, 5);
    }

    [Benchmark]
    public List<Driver> SpiralSearch() {
        return SpiralSearchAlgorithm.FindNearestDrivers(_client, _drivers, 5);
    }

    [Benchmark] 
    public List<Driver> ExpandingSquare() {
        return ExpandingSquareAlgorithm.FindNearestDrivers(_client, _drivers, 5);
    }
}