using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ATM.classes;
using ATM.EventArgsClasses;
using ATM.interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class LogTest
    {
        private ILog _log;
        private IAnalyser _analyser;

        [SetUp]
        public void Setup()
        {
            _analyser = Substitute.For<IAnalyser>();
            _log = new Log(_analyser);
        }

        [Test]
        public void Test_LogSeperationEvent()
        {
            AircraftData a1 = new AircraftData("ZRK564", 10000, 40000, 10000, new TimeStamp(2018, 10, 2, 14, 0, 0, 0));
            AircraftData a2 = new AircraftData("BRR594", 60000, 40000, 12000, new TimeStamp(2018, 10, 2, 14, 0, 0, 0));

            SeparationAircraftsData s1 = new SeparationAircraftsData(a1, a2);

            _analyser.SeparationEvent += Raise.EventWith(_analyser, s1);

            using (StreamReader sr = new StreamReader("log.txt"))
            {
                string contents = sr.ReadToEnd();

                Assert.IsTrue(contents.Contains("ZRK564 and BRR594"));
            }

            //Check that the same event do not reoccurr in log file 
            _analyser.SeparationEvent += Raise.EventWith(_analyser, s1);

            using (StreamReader sr = new StreamReader("log.txt"))
            {
                string contents = sr.ReadToEnd();

                int count = Regex.Matches(contents, "ZRK564 and BRR594").Count;

                Assert.AreEqual(count, 1);
            }

            AircraftData a3 = new AircraftData("ERS123", 10000, 40000, 10000, new TimeStamp(2018, 10, 2, 14, 0, 0, 0));
            AircraftData a4 = new AircraftData("TER123", 60000, 40000, 12000, new TimeStamp(2018, 10, 2, 14, 0, 0, 0));

            SeparationAircraftsData s2 = new SeparationAircraftsData(a3, a4);

            //Check that second event is added to log file
            _analyser.SeparationEvent += Raise.EventWith(_analyser, s2);

            using (StreamReader sr = new StreamReader("log.txt"))
            {
                string contents = sr.ReadToEnd();

                int count = Regex.Matches(contents, "ERS123 and TER123").Count;

                Assert.AreEqual(count, 1);
            }
        }
    }
}
