using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;
using ATM.interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class LogTest
    {
        private Log _log;

        [SetUp]
        public void Setup()
        {
            _log = new Log();
        }

        [Test]
        public void Test_LogSeperationEvent()
        {
            AircraftData a1 = new AircraftData("ZRK564", 10000, 40000, 10000, new TimeStamp(2018, 10, 2, 14, 0, 0, 0));
            AircraftData a2 = new AircraftData("BRR594", 60000, 40000, 12000, new TimeStamp(2018, 10, 2, 14, 0, 0, 0));

            _log.LogSeperationEvent(a1, a2);

            using (StreamReader sr = new StreamReader("log.txt"))
            {
                string contents = sr.ReadToEnd();

                Assert.IsTrue(contents.Contains("ZRK564"));
                Assert.IsTrue(contents.Contains("BRR594"));
            }
        }
    }
}
