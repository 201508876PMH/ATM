using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;
using ATM.interfaces;
using Castle.Core.Internal;
using NUnit.Framework;
using NSubstitute;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class AnalyserTest
    {
        private Analyser _uut;
        private IUtility _utility;

        [SetUp]
        public void Setup()
        {
            _utility = Substitute.For<IUtility>();
            _uut = new Analyser(_utility);
        }

        [Test]
        public void Test_FilterAircrafts()
        {
            List<AircraftData> FakeAircrafts = new List<AircraftData>();

            FakeAircrafts.Add(new AircraftData("ZRK564", 8000, 40000, 10000, new TimeStamp(2018, 10, 2, 14, 0, 0, 0)));
            FakeAircrafts.Add(new AircraftData("BRR594", 60000, 40000, 22000, new TimeStamp(2018, 10, 2, 14, 0, 0, 0)));
            FakeAircrafts.Add(new AircraftData("BXX794", 60000, 95000, 7000, new TimeStamp(2018, 10, 2, 14, 0, 0, 0)));
            FakeAircrafts.Add(new AircraftData("XMW494", 60000, 40000, 300, new TimeStamp(2018, 10, 2, 14, 0, 0, 0)));
            FakeAircrafts.Add(new AircraftData("XRM294", 1000, 95000, 300, new TimeStamp(2018, 10, 2, 14, 0, 0, 0)));
            FakeAircrafts.Add(new AircraftData("TEE666", 10000, 90000, 500, new TimeStamp(2018, 10, 2, 14, 0, 0, 0)));

            _uut.FilterAircrafts(FakeAircrafts);

            Assert.That(_uut._FilteredAircrafts[0].Tag == "TEE666" && _uut._FilteredAircrafts.Count() == 1);
        }

        [TestCase(10000, 9800, 4000, true)]
        [TestCase(10000, 9800, 6000, false)]
        [TestCase(10000, 6000, 4000, false)]
        public void Test_CheckForCollisionIsTrue(int Alt1, int Alt2, int Dist, bool Collision)
        {
            AircraftData a1 = new AircraftData("", 0, 0, Alt1, null);
            AircraftData a2 = new AircraftData("", 0, 0, Alt2, null);

            _utility.CalcDistance(a1, a2).Returns(Dist);
            
            Assert.That(_uut.CheckForCollision(a1, a2) == Collision);
        }
    }
}