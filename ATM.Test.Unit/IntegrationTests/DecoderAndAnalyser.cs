using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;
using ATM.interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;
using Decoder = ATM.classes.Decoder;

namespace ATM.Test.Unit.IntegrationTests
{
    [TestFixture]
    public class DecoderAndAnalyser
    {
        private int _nSeparationEventsRaised = 0;
        private int _nAnalysedDataReadyEventsRaised = 0;
        private int _nTrackEnteredAirspaceEventsRaised = 0;
        private int _nTrackLeftAirspaceEventsRaised = 0;

        //fakes
        private ITransponderReceiver _fakeReceiver;


        //Real
        private IUtility _realUtility;
        private IAnalyser _realAnalyser;
        private IDecoder _realDecoder;

        [SetUp]
        public void SetUp()
        {
            //fakes init
            _fakeReceiver = Substitute.For<ITransponderReceiver>();

            //Real init
            _realUtility = new Utility();
            _realDecoder = new Decoder(_fakeReceiver, _realUtility);
            _realAnalyser = new Analyser(_realUtility, _realDecoder);
            

            _realAnalyser.SeparationEvent += (o, args) =>
            {
                ++_nSeparationEventsRaised;
            };

            _realAnalyser.AnalysedDataReadyEvent += (o, args) =>
            {
                ++_nAnalysedDataReadyEventsRaised;
            };

            _realAnalyser.TrackEnteredAirSpaceEvent += (o, args) =>
            {
                ++_nTrackEnteredAirspaceEventsRaised;
            };

            _realAnalyser.TrackLeftAirSpaceEvent += (o, args) =>
            {
                ++_nTrackLeftAirspaceEventsRaised;
            };

        }



        [Test]
        public void TestSeparationEventRaised()
        {
            // SetUp Event
            string testerString  = "ATR423;30000;31111;14000;20151006213456789";
            string testerString1 = "ATR424;31111;30000;14100;20151006213456789";
            string testerString2 = "ATR425;50000;30000;18000;20151006213456789";

            List<string> fakeEventList = new List<string>();
            fakeEventList.Add(testerString);
            fakeEventList.Add(testerString1);
            fakeEventList.Add(testerString2);
            
            //Raise Event
            RawTransponderDataEventArgs arg = new RawTransponderDataEventArgs(fakeEventList);
            _fakeReceiver.TransponderDataReady += Raise.EventWith(_fakeReceiver, arg);

            // Expect a Separation Event
            Assert.That(_nSeparationEventsRaised == 1);

            //Raise Another Event 
            string testerString3 = "ATR426;82222;83333;14100;20151006213456789";
            string testerString4 = "ATR427;83333;82222;14200;20151006213456789";
            fakeEventList.Add(testerString3);
            fakeEventList.Add(testerString4);

            RawTransponderDataEventArgs arg1 = new RawTransponderDataEventArgs(fakeEventList);
            _fakeReceiver.TransponderDataReady += Raise.EventWith(_fakeReceiver, arg1);

            // Expect a Separation Event 1 + 2

            Assert.That(_nSeparationEventsRaised == 3);
            
        }

        [Test]
        public void TestEnterEventRaised()
        {

            // SetUp
            string testerString = "ATR423;30000;31111;14000;20151006213456789";
            List<string> fakeEventList = new List<string>();
            fakeEventList.Add(testerString);

            RawTransponderDataEventArgs arg = new RawTransponderDataEventArgs(fakeEventList);
            _fakeReceiver.TransponderDataReady += Raise.EventWith(_fakeReceiver, arg);


            Assert.That(_nTrackEnteredAirspaceEventsRaised == 1);

            string testerString2 = "ATR424;30000;31111;14000;20151006213456789";
            string testerString3 = "ATR425;30000;31111;14000;20151006213456789";
            fakeEventList.Add(testerString2);
            fakeEventList.Add(testerString3);

            RawTransponderDataEventArgs arg2 = new RawTransponderDataEventArgs(fakeEventList);
            _fakeReceiver.TransponderDataReady += Raise.EventWith(_fakeReceiver, arg2);


            Assert.That(_nTrackEnteredAirspaceEventsRaised == 3);
        }

        [Test]
        public void TestExitEventRaisedAndAnalysedDataReaddy()
        {
            // SetUp
            string testerString = "ATR423;30000;31111;14000;20151006213456789";
            string testerString2 = "ATR424;30000;31111;14000;20151006213456789";
            string testerString3 = "ATR425;30000;31111;14000;20151006213456789";
            string testerString4 = "ATR426;30000;31111;14000;20151006213456789";
            string testerString5 = "ATR427;30000;31111;14000;20151006213456789";

            List<string> fakeEventList = new List<string>();
            fakeEventList.Add(testerString);
            fakeEventList.Add(testerString2);
            fakeEventList.Add(testerString3);
            fakeEventList.Add(testerString4);
            fakeEventList.Add(testerString5);
            

            RawTransponderDataEventArgs arg = new RawTransponderDataEventArgs(fakeEventList);
            _fakeReceiver.TransponderDataReady += Raise.EventWith(_fakeReceiver, arg);

            // Check that 5 track entered
            Assert.That(_nTrackEnteredAirspaceEventsRaised == 5);

            // Insure no tracks has left yet.
            Assert.That(_nTrackLeftAirspaceEventsRaised == 0);
            
            // Check that Data only has been analysed one time
            Assert.That(_nAnalysedDataReadyEventsRaised == 1);


            // SetUp To Remove one track
            string testerString6 = "ATR423;30000;31111;14000;20151006213456789";
            string testerString7 = "ATR424;30000;31111;14000;20151006213456789";
            string testerString8 = "ATR425;30000;31111;14000;20151006213456789";
            string testerString9 = "ATR426;30000;31111;14000;20151006213456789";
           
            List<string> fakeEventList2 = new List<string>();
            fakeEventList2.Add(testerString6);
            fakeEventList2.Add(testerString7);
            fakeEventList2.Add(testerString8);
            fakeEventList2.Add(testerString9);

            RawTransponderDataEventArgs arg2 = new RawTransponderDataEventArgs(fakeEventList2);
            _fakeReceiver.TransponderDataReady += Raise.EventWith(_fakeReceiver, arg2);


            // Check that 5 track entered
            Assert.That(_nTrackEnteredAirspaceEventsRaised == 5);

            // Insure no tracks has left yet.
            Assert.That(_nTrackLeftAirspaceEventsRaised == 1);

            // Check that Data only has been analysed one time
            Assert.That(_nAnalysedDataReadyEventsRaised == 2);
            
        }



    }
}
