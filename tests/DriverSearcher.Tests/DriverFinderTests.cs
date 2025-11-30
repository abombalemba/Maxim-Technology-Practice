using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DriverSearcher.Tests {
    [TestFixture]
    public class DriverFinderTests {
        private List<Driver> _testDrivers;
        private Client _testClient;

        [SetUp]
        public void Setup() {
            _testDrivers = new List<Driver> {
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
            
            _testClient = new Client(0, 0);
        }

        [Test]
        public void BruteForceAlgorithm_FindNearestDrivers_ReturnsCorrectNumberOfDrivers() {
            var result = BruteForceAlgorithm.FindNearestDrivers(_testClient, _testDrivers, 3);
            
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void BruteForceAlgorithm_FindNearestDrivers_ReturnsClosestDriversFirst() {
            var result = BruteForceAlgorithm.FindNearestDrivers(_testClient, _testDrivers, 3);
            
            Assert.That(result[0].Id, Is.EqualTo(8));
            Assert.That(result[1].Id, Is.EqualTo(1));
            Assert.That(result[2].Id, Is.EqualTo(4));
        }

        [Test]
        public void SpiralSearchAlgorithm_FindNearestDrivers_ReturnsCorrectDrivers() {
            var result = SpiralSearchAlgorithm.FindNearestDrivers(_testClient, _testDrivers, 3);
            
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.Select(d => d.Id), Contains.Item(8));
        }

        [Test]
        public void ExpandingSquareAlgorithm_FindNearestDrivers_ReturnsCorrectDrivers() {
            var result = ExpandingSquareAlgorithm.FindNearestDrivers(_testClient, _testDrivers, 3);
            
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.Select(d => d.Id), Contains.Item(8));
        }

        [Test]
        public void AllAlgorithms_WithEmptyDriverList_ReturnEmptyList() {
            var emptyDrivers = new List<Driver>();
            
            var result1 = BruteForceAlgorithm.FindNearestDrivers(_testClient, emptyDrivers, 5);
            var result2 = SpiralSearchAlgorithm.FindNearestDrivers(_testClient, emptyDrivers, 5);
            var result3 = ExpandingSquareAlgorithm.FindNearestDrivers(_testClient, emptyDrivers, 5);
            
            Assert.That(result1, Is.Empty);
            Assert.That(result2, Is.Empty);
            Assert.That(result3, Is.Empty);
        }

        [Test]
        public void AllAlgorithms_WithLessDriversThanRequested_ReturnAllAvailableDrivers() {
            var fewDrivers = new List<Driver> {
                new Driver(1, 1, 1),
                new Driver(2, 2, 2)
            };
            
            var result1 = BruteForceAlgorithm.FindNearestDrivers(_testClient, fewDrivers, 5);
            var result2 = SpiralSearchAlgorithm.FindNearestDrivers(_testClient, fewDrivers, 5);
            var result3 = ExpandingSquareAlgorithm.FindNearestDrivers(_testClient, fewDrivers, 5);
            
            Assert.That(result1.Count, Is.EqualTo(2));
            Assert.That(result2.Count, Is.EqualTo(2));
            Assert.That(result3.Count, Is.EqualTo(2));
        }

        [Test]
        public void AllAlgorithms_WithSameClientPositionAsDriver_ReturnThatDriverFirst() {
            var driversWithExactMatch = new List<Driver> {
                new Driver(1, 5, 5),
                new Driver(2, 0, 0),
                new Driver(3, 10, 10)
            };
            
            var result1 = BruteForceAlgorithm.FindNearestDrivers(_testClient, driversWithExactMatch, 1);
            var result2 = SpiralSearchAlgorithm.FindNearestDrivers(_testClient, driversWithExactMatch, 1);
            var result3 = ExpandingSquareAlgorithm.FindNearestDrivers(_testClient, driversWithExactMatch, 1);
            
            Assert.That(result1[0].Id, Is.EqualTo(2));
            Assert.That(result2[0].Id, Is.EqualTo(2));
            Assert.That(result3[0].Id, Is.EqualTo(2));
        }

        [Test]
        public void Algorithms_ReturnResultsInCorrectOrder() {
            var bruteResult = BruteForceAlgorithm.FindNearestDrivers(_testClient, _testDrivers, 5);
            var spiralResult = SpiralSearchAlgorithm.FindNearestDrivers(_testClient, _testDrivers, 5);
            var squareResult = ExpandingSquareAlgorithm.FindNearestDrivers(_testClient, _testDrivers, 5);
            
            Assert.That(bruteResult[0].Id, Is.EqualTo(8));
            Assert.That(spiralResult.Any(r => r.Id == 8), Is.True);
            Assert.That(squareResult.Any(r => r.Id == 8), Is.True);
        }
    }
}