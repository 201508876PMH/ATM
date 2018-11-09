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
using TransponderReceiver;


namespace ATM.Test.Unit.IntegrationTests
{
    [TestFixture]
    public class DecoderAnalyserLog
    {
        // Fakes
        private ITransponderReceiver _fakeTransponderReceiver;

        // Real
        private IDecoder _realDecoder;
        private IAnalyser _realAnalyser;
        private ILog _realLog;

        // We create this reference reasoned our Decoder-constructor takes an IUtility as second argument.
        private IUtility _realUtility;


        [SetUp]
        public void SetUp()
        {
            /*
             * We dont create a mock of our class, as it is the class from which we test from
             */

            // If we test multiple classes in conjunction, we add mocks here: 
            // This is a fake, therefor we substitute it.
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            
            // We instantiate our 'real' variables 
            _realUtility = new Utility();
            _realDecoder = new classes.Decoder(_fakeTransponderReceiver, _realUtility);
            _realAnalyser = new Analyser(_realUtility, _realDecoder);
            _realLog = new Log(_realAnalyser);
           
        }

        [Test]
        public void testLoggingEventCorrectlyloggsFlights()
        {
            // Custom made strings from our fake TransponderReceiverEvent
            // These strings fall in the category of a seperationEvent (two planes Colliding) 
            string testerString0 = "ATR423;30000;31111;14000;20151006213456789";
            string testerString1 = "ATR424;31111;30000;14100;20151006213456789";

            // We create a 'fake' list of string and add the two previously created strings
            List<string> fakeEventList = new List<string>();
            fakeEventList.Add(testerString0);
            fakeEventList.Add(testerString1);
            
            // We use our 'fake' list as argument, instead of the .Dll file
            // This we where we raise our first event "inject our own even"
            RawTransponderDataEventArgs arg = new RawTransponderDataEventArgs(fakeEventList);

            // We here subscribe 
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(_fakeTransponderReceiver, arg);

            // We use [StreamReader] for reading the log.txt file located in bin/debug
            // Here we assert that our file contains atleast the two .tag(s) of our flights
            using (StreamReader sr = new StreamReader("log.txt"))
            {
                string contents = sr.ReadToEnd();

                Assert.IsTrue(contents.Contains("ATR423"));
                Assert.IsTrue(contents.Contains("ATR424"));

            }
        }

        [Test]
        public void testLoggingEventCorrectlyloggsThree()
        {
            // Custom made strings from our fake TransponderReceiverEvent
            // These strings fall in the category of a seperationEvent (two planes Colliding) 
            string testerString0 = "ATR423;30000;31111;14000;20151006213456789";
            string testerString1 = "ATR424;31111;30000;14100;20151006213456789";
            string testerString2 = "ATR425;31111;30000;14100;20151006213456789";

            // We create a 'fake' list of string and add the two previously created strings
            List<string> fakeEventList = new List<string>();
            fakeEventList.Add(testerString0);
            fakeEventList.Add(testerString1);
            fakeEventList.Add(testerString2);

            // We use our 'fake' list as argument, instead of the .Dll file
            // This we where we raise our first event "inject our own even"
            RawTransponderDataEventArgs arg = new RawTransponderDataEventArgs(fakeEventList);

            // We here subscribe 
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(_fakeTransponderReceiver, arg);

            // We use [StreamReader] for reading the log.txt file located in bin/debug
            // Here we assert that our file contains atleast the two .tag(s) of our flights
            using (StreamReader sr = new StreamReader("log.txt"))
            {
                string contents = sr.ReadToEnd();

                Assert.IsTrue(contents.Contains("ATR423"));
                Assert.IsTrue(contents.Contains("ATR424"));
                Assert.IsTrue(contents.Contains("ATR425"));

            }
        }

        [Test]
        public void testLoggingEventCorrectlyloggsNone()
        {
            // Custom made strings from our fake TransponderReceiverEvent
            // These strings fall in the category of a seperationEvent (two planes Colliding) 
            string testerString0 = "ATR423;30000;31111;14000;20151006213456789";
            string testerString1 = "ATR424;51111;30000;14100;20151006213456789";
            string testerString2 = "ATR425;81111;30000;14100;20151006213456789";

            // We create a 'fake' list of string and add the two previously created strings
            List<string> fakeEventList = new List<string>();
            fakeEventList.Add(testerString0);
            fakeEventList.Add(testerString1);
            fakeEventList.Add(testerString2);

            // We use our 'fake' list as argument, instead of the .Dll file
            // This we where we raise our first event "inject our own even"
            RawTransponderDataEventArgs arg = new RawTransponderDataEventArgs(fakeEventList);

            // We here subscribe 
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(_fakeTransponderReceiver, arg);

            // We use [StreamReader] for reading the log.txt file located in bin/debug
            // Here we assert that our file contains atleast the two .tag(s) of our flights
            using (StreamReader sr = new StreamReader("log.txt"))
            {
                string contents = sr.ReadToEnd();

                Assert.IsTrue(contents.Contains(""));

            }
        }


    }
}
