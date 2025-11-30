using System;
using System.Collections.Generic;
using System.Linq;

public static class BruteForceAlgorithm {
    public static List<Driver> FindNearestDrivers(Client client, List<Driver> drivers, int count = 5) {
        return drivers
            .OrderBy(d => (d.X - client.X) * (d.X - client.X) + (d.Y - client.Y) * (d.Y - client.Y))
            .Take(count)
            .ToList();
    }
}