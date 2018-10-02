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
    public class ATMTest
    {
        private Analyser _uut;
        private IDecoder _decoder;

        [SetUp]
        public void Setup()
        {
            _uut = new Analyser();
            _decoder = Substitute.For<IDecoder>();
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

            _uut.FilterAircrafts(FakeAircrafts);

            Assert.That(_uut._FilteredAircrafts.IsNullOrEmpty());

            /*
            _uv.ValidateEntryRequest("1234").Returns(true);

            _uut.RequestEntry("1234");

            _uv.Received().ValidateEntryRequest("1234");

            _d.Received().Open();
            _en.Received().NotifyEntryGranted();

            _uut.DoorClosed(); //Ikke muligt at lukke døren
            _uut.DoorOpened();

            _d.Received().Close();
            */
        }
    }
}