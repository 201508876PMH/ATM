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
using TransponderReceiver;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class DecoderTest
    {
        private Decoder _uut;
        private IUtility _utility = new Utility();
        private ITransponderReceiver _reciever;

        [SetUp]
        public void setUp()
        {
            //if we test multiple classes in conjunction, we add mocks here:
            
            //_utility = Substitute.For<IUtility>();
            _reciever = Substitute.For<ITransponderReceiver>();

            //Constructor injection
            _uut = new Decoder(_reciever, _utility);
        }



        [Test]
        public void runSelfTest_DecodeString()
        {
            string stringToDecode = "ATR423;39045;12932;14000;20151006213456789";
            AircraftData holder;

            holder = _uut.DecodeString(stringToDecode);
            holder.Coords = 342.0;


            Assert.That(holder.Tag == "ATR423");
            Assert.That(holder.X_coordinate == int.Parse("39045"));
            Assert.That(holder.Y_coordinate == int.Parse("12932"));
            Assert.That(holder.Altitude == int.Parse("14000"));


            Assert.That(holder.TimeStamp.ms == 789);
            Assert.That(holder.TimeStamp.sec == 56);
            Assert.That(holder.TimeStamp.min == 34);
            Assert.That(holder.TimeStamp.hour == 21);
            Assert.That(holder.TimeStamp.day == 06);
            Assert.That(holder.TimeStamp.month == 10);
            Assert.That(holder.TimeStamp.year == 2015);
            Assert.That(holder.Coords == 342.0);

        }

        [Test]
        public void runSelfTest_InsertSpeedWhereListsAreEqual()
        {
            // After this test, all the new objects should have the speed of the old objects

            List<AircraftData> oldFakeList = new List<AircraftData>();
            oldFakeList.Add(new AircraftData("FlIGHT01", 0001, 00001, 00001, new TimeStamp(0001, 01, 1, 1, 1, 1, 1)));
            oldFakeList.Add(new AircraftData("FlIGHT02", 0002, 00002, 00002, new TimeStamp(0002, 02, 2, 2, 2, 2, 2)));
            oldFakeList.Add(new AircraftData("FlIGHT03", 0003, 00003, 00003, new TimeStamp(0003, 03, 3, 3, 3, 3, 3)));

            List<AircraftData> newFakeList = new List<AircraftData>();
            newFakeList.Add(new AircraftData("FlIGHT01", 0000, 00000, 00001, new TimeStamp(0001, 01, 1, 1, 1, 1, 1)));
            newFakeList.Add(new AircraftData("FlIGHT02", 0000, 00000, 00002, new TimeStamp(0002, 02, 2, 2, 2, 2, 2)));
            newFakeList.Add(new AircraftData("FlIGHT03", 0000, 00000, 00003, new TimeStamp(0003, 03, 3, 3, 3, 3, 3)));

          
         

            _uut.InsertSpeedAndCourse(oldFakeList, newFakeList);

            Assert.That(newFakeList[0].Speed == oldFakeList[0].Speed);
            Assert.That(newFakeList[1].Speed == oldFakeList[1].Speed);
            Assert.That(newFakeList[2].Speed == oldFakeList[2].Speed);

        }

        [Test]
        public void runSelfTest_InsertSpeedWhereOldListIsGreater()
        {
            // After this test, all the new objects should have the speed of the old objects

            List<AircraftData> oldFakeList = new List<AircraftData>();
            oldFakeList.Add(new AircraftData("FlIGHT01", 0001, 00001, 00001, new TimeStamp(0001, 01, 1, 1, 1, 1, 1)));
            oldFakeList.Add(new AircraftData("FlIGHT02", 0002, 00002, 00002, new TimeStamp(0002, 02, 2, 2, 2, 2, 2)));
            oldFakeList.Add(new AircraftData("FlIGHT03", 0003, 00003, 00003, new TimeStamp(0003, 03, 3, 3, 3, 3, 3)));

            List<AircraftData> newFakeList = new List<AircraftData>();
            newFakeList.Add(new AircraftData("FlIGHT01", 0000, 00000, 00001, new TimeStamp(0001, 01, 1, 1, 1, 1, 1)));
            newFakeList.Add(new AircraftData("FlIGHT02", 0000, 00000, 00002, new TimeStamp(0002, 02, 2, 2, 2, 2, 2)));


            _uut.InsertSpeedAndCourse(oldFakeList, newFakeList);

            Assert.That(newFakeList[0].Speed == oldFakeList[0].Speed);
            Assert.That(newFakeList[1].Speed == oldFakeList[1].Speed);

        }

        [Test]
        public void runSelfTest_InsertSpeedWhereOldListIsLess()
        {
            // After this test, all the new objects should have the speed of the old objects

            List<AircraftData> oldFakeList = new List<AircraftData>();
            oldFakeList.Add(new AircraftData("FlIGHT01", 0001, 00001, 00001, new TimeStamp(0001, 01, 1, 1, 1, 1, 1)));
            oldFakeList.Add(new AircraftData("FlIGHT02", 0002, 00002, 00002, new TimeStamp(0002, 02, 2, 2, 2, 2, 2)));


            List<AircraftData> newFakeList = new List<AircraftData>();
            newFakeList.Add(new AircraftData("FlIGHT01", 0000, 00000, 00001, new TimeStamp(0001, 01, 1, 1, 1, 1, 1)));
            newFakeList.Add(new AircraftData("FlIGHT02", 0000, 00000, 00002, new TimeStamp(0002, 02, 2, 2, 2, 2, 2)));
            newFakeList.Add(new AircraftData("FlIGHT03", 0000, 00000, 00003, new TimeStamp(0003, 03, 3, 3, 3, 3, 3)));

            _uut.InsertSpeedAndCourse(oldFakeList, newFakeList);

            Assert.That(newFakeList[0].Speed == oldFakeList[0].Speed);
            Assert.That(newFakeList[1].Speed == oldFakeList[1].Speed);

        }

        [Test]
        public void runSelfTest_UpdateTransponderData()
        {
            List<string> listOfStrings = new List<string>();
            string testerString = "ATR423;39045;12932;14000;20151006213456789";
            string testerString1 = "ATR424;39045;12932;14000;20151006213456789";

            listOfStrings.Add(testerString);
            listOfStrings.Add(testerString1);

            _uut.UpdateTransponderData(listOfStrings);

            Assert.That(_uut._Aircrafts[0].Tag == "ATR423");
            Assert.That(_uut._Aircrafts[1].Tag == "ATR424");
        }


        
    }
}
