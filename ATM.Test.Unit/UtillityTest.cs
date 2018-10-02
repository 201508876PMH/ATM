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
        [TestCase(10500, 50000,15000,35000, 15660)]
        [TestCase(15444, 65454, 23452, 67896, 8372)]
        [TestCase(56433, 45645, 11111, 22222, 16132)]
        public void CalculateDistance(int x1, int y1, int x2, int y2, int result)
        {
            AircraftData airObj1 = new AircraftData("test", x1, y1, 500, new TimeStamp(1, 1, 1, 1, 1, 1, 1));
            AircraftData airObj2 = new AircraftData("test", x2, y2, 500, new TimeStamp(1, 1, 1, 1, 1, 1, 1));

            int distance = _uut.CalcDistance(airObj1, airObj2);

            Console.WriteLine(distance);
            Assert.That(distance == result);



        }







    }
}
