using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.interfaces;
using NSubstitute;
using NUnit.Framework;
using Decoder = ATM.classes.Decoder;
using ATM.classes;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class DecoderTest
    {
        private Decoder _uut;
        private IUtility _utility;

        [SetUp]
        public void setUp()
        {
            /*
             * We dont create a mock of our Decoder class, as it is the class from which we test from
             */

            //if we test multiple classes in conjunction, we add mocks here:
            _utility = Substitute.For<IUtility>();
            
            
            //Constructor injection
            _uut = new Decoder(_utility);
        }

        [Test]
        // Test that when given a empty list, we receive 
        public void runSelfTest_cloneListFails()
        {
            List<AircraftData> fakeListEmpty = new List<AircraftData>();
            List<AircraftData> holderList = new List<AircraftData>();

            List<AircraftData> fakeListFull = new List<AircraftData>();
            fakeListFull.Add(new AircraftData("FlIGHT01", 8001, 40001, 10001, new TimeStamp(2018, 10, 2, 14, 0, 0, 0)));
            fakeListFull.Add(new AircraftData("FLIGHT02", 8002, 40002, 10002, new TimeStamp(2019, 11, 3, 15, 1, 1, 2)));
            fakeListFull.Add(new AircraftData("FLIGHT03", 8003, 40003, 10003, new TimeStamp(2010, 12, 4, 16, 2, 2, 3)));

            holderList = _uut.CloneList(fakeListFull);

            Assert.That(fakeListEmpty, Is.Not.AnyOf(holderList));
        }

        [Test]
        // Test that when given a empty list, we receive 
        public void runSelfTest_cloneListSuccedes()
        {
            List<AircraftData> fakeListEmpty = new List<AircraftData>();
            List<AircraftData> holderList = new List<AircraftData>();

            List<AircraftData> fakeListFull = new List<AircraftData>();
            fakeListFull.Add(new AircraftData("FlIGHT01", 8001, 40001, 10001, new TimeStamp(2018, 10, 2, 14, 0, 0, 0)));
            fakeListFull.Add(new AircraftData("FLIGHT02", 8002, 40002, 10002, new TimeStamp(2019, 11, 3, 15, 1, 1, 2)));
            fakeListFull.Add(new AircraftData("FLIGHT03", 8003, 40003, 10003, new TimeStamp(2010, 12, 4, 16, 2, 2, 3)));

            holderList = _uut.CloneList(fakeListFull);

            Assert.That(fakeListFull, Is.EqualTo(holderList));
        }


        [Test]
        public void runSelfTest_DecodeString()
        {

        }
    }
}
