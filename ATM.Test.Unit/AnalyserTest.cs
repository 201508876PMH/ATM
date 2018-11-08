using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;
using ATM.EventArgsClasses;
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
        private IDecoder _decoder;

        private int _nSeparationEventsRaised = 0;
        private int _nAnalysedDataReadyEventsRaised = 0;
        private int _nTrackEnteredAirspaceEventsRaised = 0;
        private int _nTrackLeftAirspaceEventsRaised = 0;

        [SetUp]
        public void Setup()
        {
            _utility = Substitute.For<IUtility>();
            _decoder = Substitute.For<IDecoder>();
            _uut = new Analyser(_utility, _decoder);
            
            _uut.SeparationEvent += (o, args) =>
            {
                ++_nSeparationEventsRaised;
            };

            _uut.AnalysedDataReadyEvent += (o, args) =>
            {
                ++_nAnalysedDataReadyEventsRaised;
            };

            _uut.TrackEnteredAirSpaceEvent += (o, args) =>
            {
                ++_nTrackEnteredAirspaceEventsRaised;
            };

            _uut.TrackLeftAirSpaceEvent += (o, args) =>
            {
                ++_nTrackLeftAirspaceEventsRaised;
            };
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

            _uut.FilterAircrafts(
                );

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

            TimeStamp ts = new TimeStamp(2018, 10, 2, 14, 0, 0, 0);

            AircraftData a1 = new AircraftData("Plane1", 40000, 40000, 10000, ts);
            AircraftData a2 = new AircraftData("Plane2", 10000, 10000, 11000, ts);
            AircraftData a3 = new AircraftData("Plane3", 70000, 70000, 7000, ts);
            AircraftData a4 = new AircraftData("Plane4", 20000, 20000, 12000, ts);
            AircraftData a5 = new AircraftData("Plane5", 75000, 75000, 7300, ts);
            AircraftData a6 = new AircraftData("Plane6", 30000, 30000, 9000, ts);

            FakeAircrafts.Add(a1);
            FakeAircrafts.Add(a2);
            FakeAircrafts.Add(a3);
            FakeAircrafts.Add(a4);
            FakeAircrafts.Add(a5);
            FakeAircrafts.Add(a6);

            _uut._FilteredAircrafts.Add(a1);
            _uut._FilteredAircrafts.Add(a2);
            _uut._FilteredAircrafts.Add(a3);
            _uut._FilteredAircrafts.Add(a4);
            _uut._FilteredAircrafts.Add(a5);
            _uut._FilteredAircrafts.Add(a6);

            List<AircraftData> OldFakeAircrafts = new List<AircraftData>();
            
            OldFakeAircrafts.Add(a1);
            OldFakeAircrafts.Add(a2);
            OldFakeAircrafts.Add(a3);
            OldFakeAircrafts.Add(a4);
            OldFakeAircrafts.Add(a5);
            OldFakeAircrafts.Add(a6);

            _utility.CloneList(_uut._FilteredAircrafts).Returns(FakeAircrafts);

            _utility.CalcDistance(a3, a5).Returns(5000);

            //_uut.AnalyseData(FakeAircrafts);
            _uut.AnalyseEventMethod(_uut, new DecodedTransponderDataEventArgs(FakeAircrafts));

            Assert.AreEqual(_nSeparationEventsRaised, 1);
            Assert.AreEqual(_nAnalysedDataReadyEventsRaised, 1);
        }

        [Test]
        public void Test_TrackEnteredAirspaceEvent()
        {
            TimeStamp ts = new TimeStamp(2018, 10, 2, 14, 0, 0, 0);
            AircraftData a1 = new AircraftData("Plane1", 40000, 40000, 10000, ts);
            AircraftData a2 = new AircraftData("Plane2", 10000, 10000, 11000, ts);
            AircraftData a3 = new AircraftData("Plane3", 70000, 70000, 7000, ts);
            AircraftData a4 = new AircraftData("Plane4", 20000, 20000, 12000, ts);

            _uut._OldFilteredAircrafts.Add(a1);
            _uut._OldFilteredAircrafts.Add(a2);

            _uut._FilteredAircrafts.Add(a1);
            _uut._FilteredAircrafts.Add(a2);
            _uut._FilteredAircrafts.Add(a3);
            _uut._FilteredAircrafts.Add(a4);

            _uut.CheckForTrackEnteredAirspace();
            Assert.AreEqual(_nTrackEnteredAirspaceEventsRaised, 2);
        }

        [Test]
        public void Test_TrackLeftAirspaceEvent()
        {
            TimeStamp ts = new TimeStamp(2018, 10, 2, 14, 0, 0, 0);
            AircraftData a1 = new AircraftData("Plane1", 40000, 40000, 10000, ts);
            AircraftData a2 = new AircraftData("Plane2", 10000, 10000, 11000, ts);
            AircraftData a3 = new AircraftData("Plane3", 70000, 70000, 7000, ts);
            AircraftData a4 = new AircraftData("Plane4", 20000, 20000, 12000, ts);

            _uut._OldFilteredAircrafts.Add(a1);
            _uut._OldFilteredAircrafts.Add(a2);
            _uut._OldFilteredAircrafts.Add(a3);
            _uut._OldFilteredAircrafts.Add(a4);

            _uut._FilteredAircrafts.Add(a2);
            _uut._FilteredAircrafts.Add(a4);

            _uut.CheckForTrackLeftAirspace();
            Assert.AreEqual(_nTrackLeftAirspaceEventsRaised, 2);
        }
    }
}