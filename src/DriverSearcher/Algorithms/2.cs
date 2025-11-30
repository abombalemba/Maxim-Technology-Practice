using System;
using System.Collections.Generic;
using System.Linq;

public static class SpiralSearchAlgorithm {
    public static List<Driver> FindNearestDrivers(Client client, List<Driver> drivers, int count = 5) {
        var found = new List<Driver>();
        var visited = new HashSet<(int, int)>();
        
        for (int radius = 0; radius <= 100 && found.Count < count; radius++) {
            for (int dx = -radius; dx <= radius && found.Count < count; dx++) {
                for (int dy = -radius; dy <= radius && found.Count < count; dy++) {
                    if (Math.Abs(dx) != radius && Math.Abs(dy) != radius)
                        continue;
                        
                    int searchX = client.X + dx;
                    int searchY = client.Y + dy;
                    
                    if (searchX < 0 || searchY < 0) continue;
                    
                    var driver = drivers.FirstOrDefault(d => d.X == searchX && d.Y == searchY);
                    if (driver != null && !found.Contains(driver)) {
                        found.Add(driver);
                    }
                }
            }
        }
        
        return found.Take(count).ToList();
    }
}