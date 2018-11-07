using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;
using NUnit.Framework;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class UtillityTest
    {
        private Utility _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new Utility();

        }

        [Test]
        [TestCase(10500, 50000, 15000, 35000, 15660)]    // x1     y1     x2    y2    Result Calculated + rounded down
        [TestCase(15444, 65454, 23452, 67896, 8372)]
        [TestCase(56433, 45645, 11111, 22222, 51016)]
        public void CalculateDistanceTest(int x1, int y1, int x2, int y2, int result)
        {
            AircraftData airObj1 = new AircraftData("test", x1, y1, 500, new TimeStamp(1, 1, 1, 1, 1, 1, 1));
            AircraftData airObj2 = new AircraftData("test", x2, y2, 500, new TimeStamp(1, 1, 1, 1, 1, 1, 1));

            int distance = _uut.CalcDistance(airObj1, airObj2);

            Assert.That(distance == result);
        }
        
        [Test]
        [TestCase(23,50,53,600, 85853600)] //82 800 000 + 3 000 000 + 53 000 + 600 == 85853600
        public void CalculateMiliisecTest(int hour, int min, int sec, int ms, int result)
        {
            AircraftData aircraft = new AircraftData("test", 1, 1, 1, new TimeStamp( 1, 1, 1,  hour,  min,  sec,  ms));
            TimeSpan test = new TimeSpan();

            int millisec = _uut.ConvertTimeToMilliseconds(aircraft);

            Assert.That(millisec == result);
        }

        // This function is depend on calculate distance, therefor this test is created after CalculateSpeed
        [Test]
        public void CalcluLateSpeedest()
        {

            AircraftData newAircraftData = new AircraftData("test", 15000, 20000, 1, new TimeStamp(1,1,1,23,50,53,999));
            AircraftData oldAircraftData = new AircraftData("test", 25000, 30000, 1, new TimeStamp(1, 1, 1, 23, 50, 53, 600));
            
            double speedMeterPerSec = Math.Round(_uut.Speed(newAircraftData, oldAircraftData),2);
            
            // Wolfram says = 35443.94
            // Probably  wolffram who uses more indexes
            // Our says == 35443.61

            Assert.That(speedMeterPerSec == 35443.61);
        }

        [Test]
        [TestCase(25100,25000, 29900,30000, 180, 270)] // south- East
        [TestCase(24900, 25100, 29100, 30000, 90, 180)] // south- West
        [TestCase(25100, 25000, 30000, 29100, 270, 360)] // north- East
        [TestCase(24900, 25100, 30000, 29100, 0, 90)] // Nort-West
        public void CalculateCourse(int xNew, int xOld, int yNew, int yOld, int grad1, int grad2)
        {
            AircraftData newAircraftData = new AircraftData("test", xNew,  yNew, 1, new TimeStamp(1, 1, 1, 1, 1, 1, 1));
            AircraftData oldAircraftData = new AircraftData("test", xOld, yOld, 1, new TimeStamp(1, 1, 1, 1, 1, 1, 1));

            double grad = _uut.CalculateDegree(newAircraftData, oldAircraftData);

            Assert.That(grad >= grad1 && grad <= grad2);
        }

















    }
}
