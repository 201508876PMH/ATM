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
        //private ILog _log;
        private IDecoder _decoder;


        [SetUp]
        public void Setup()
        {
            _utility = Substitute.For<IUtility>();
            //_log = Substitute.For<ILog>();
            _decoder = Substitute.For<classes.Decoder>();
            _uut = new Analyser(_utility, _decoder);

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

        [Test]
        public void Test_AnalyseData()
        {
            List<AircraftData> FakeAircrafts = new List<AircraftData>();

            AircraftData a1 = new AircraftData("Plane1", 40000, 40000, 10000, null);
            AircraftData a2 = new AircraftData("Plane2", 10000, 10000, 11000, null);
            AircraftData a3 = new AircraftData("Plane3", 70000, 70000, 7000, null);
            AircraftData a4 = new AircraftData("Plane4", 20000, 20000, 12000, null);
            AircraftData a5 = new AircraftData("Plane5", 75000, 75000, 7300, null);
            AircraftData a6 = new AircraftData("Plane6", 30000, 30000, 9000, null);

            FakeAircrafts.Add(a1);
            FakeAircrafts.Add(a2);
            FakeAircrafts.Add(a3);
            FakeAircrafts.Add(a4);
            FakeAircrafts.Add(a5);
            FakeAircrafts.Add(a6);

            _utility.CalcDistance(a3, a5).Returns(5000);

            _uut.AnalyseData(FakeAircrafts);

            //_log.Received().LogSeperationEvent(a3, a5);
        }
    }
}