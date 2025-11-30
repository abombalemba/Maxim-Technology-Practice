using System;
using System.Collections.Generic;
using System.Linq;

public static class ExpandingSquareAlgorithm {
    public static List<Driver> FindNearestDrivers(Client client, List<Driver> drivers, int count = 5) {
        var found = new List<Driver>();
        var visited = new HashSet<(int, int)>();
        
        for (int size = 0; size <= 100 && found.Count < count; size++) {
            for (int x = client.X - size; x <= client.X + size && found.Count < count; x++) {
                TryAddDriver(x, client.Y - size, drivers, found, visited);
                TryAddDriver(x, client.Y + size, drivers, found, visited);
            }
            
            for (int y = client.Y - size + 1; y < client.Y + size && found.Count < count; y++) {
                TryAddDriver(client.X - size, y, drivers, found, visited);
                TryAddDriver(client.X + size, y, drivers, found, visited);
            }
        }
        
        return found.Take(count).ToList();
    }
    
    private static void TryAddDriver(int x, int y, List<Driver> drivers, List<Driver> found, HashSet<(int, int)> visited) {
        if (x < 0 || y < 0 || visited.Contains((x, y)) || found.Count >= 5)
            return;
            
        visited.Add((x, y));
        var driver = drivers.FirstOrDefault(d => d.X == x && d.Y == y);
        if (driver != null) {
            found.Add(driver);
        }
    }
}