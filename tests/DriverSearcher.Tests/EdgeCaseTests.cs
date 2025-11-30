using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DriverSearcher.Tests {
    [TestFixture]
    public class EdgeCaseTests {
        [Test]
        public void Algorithms_WithNegativeCoordinates_WorkCorrectly() {
            var drivers = new List<Driver> {
                new Driver(1, -5, -5),
                new Driver(2, -1, -1),
                new Driver(3, -10, -10)
            };
            var client = new Client(-1, -1);
            
            var result1 = BruteForceAlgorithm.FindNearestDrivers(client, drivers, 1);
            var result2 = SpiralSearchAlgorithm.FindNearestDrivers(client, drivers, 1);
            var result3 = ExpandingSquareAlgorithm.FindNearestDrivers(client, drivers, 1);
            
            Assert.That(result1[0].Id, Is.EqualTo(2));
        }

        [Test]
        public void Algorithms_WithLargeCoordinates_WorkCorrectly() {
            var drivers = new List<Driver> {
                new Driver(1, 1000, 1000),
                new Driver(2, 1001, 1001),
                new Driver(3, 999, 999)
            };
            var client = new Client(1000, 1000);
            
            var result1 = BruteForceAlgorithm.FindNearestDrivers(client, drivers, 1);
            
            Assert.That(result1[0].Id, Is.EqualTo(1));
        }

        [Test]
        public void Algorithms_WithSingleDriver_ReturnThatDriver() {
            var singleDriver = new List<Driver> { new Driver(1, 5, 5) };
            var client = new Client(0, 0);
            
            var result1 = BruteForceAlgorithm.FindNearestDrivers(client, singleDriver, 5);
            var result2 = SpiralSearchAlgorithm.FindNearestDrivers(client, singleDriver, 5);
            var result3 = ExpandingSquareAlgorithm.FindNearestDrivers(client, singleDriver, 5);
            
            Assert.That(result1.Count, Is.EqualTo(1));
            Assert.That(result2.Count, Is.EqualTo(1));
            Assert.That(result3.Count, Is.EqualTo(1));
            Assert.That(result1[0].Id, Is.EqualTo(1));
        }
    }
}