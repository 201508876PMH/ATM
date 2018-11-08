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
            string testerString = "ATR423;30000;31111;14000;20151006213456789";
            string testerString1 = "ATR424;31111;30000;14100;20151006213456789";
            
            List<string> fakeEventList = new List<string>();
            fakeEventList.Add(testerString);
            fakeEventList.Add(testerString1);

            RawTransponderDataEventArgs arg = new RawTransponderDataEventArgs(fakeEventList);
            _fakeReceiver.TransponderDataReady += Raise.EventWith(_fakeReceiver, arg);


           // _realUtility.CalcDistance(null, null).ReturnsForAnyArgs(3000);
           
            Assert.That(_nSeparationEventsRaised == 1);
            
        }

        [Test]
        public void TestEnterEventRaised()
        {

            string testerString = "ATR423;30000;31111;14000;20151006213456789";

            List<string> fakeEventList = new List<string>();
            fakeEventList.Add(testerString);

            RawTransponderDataEventArgs arg = new RawTransponderDataEventArgs(fakeEventList);
            _fakeReceiver.TransponderDataReady += Raise.EventWith(_fakeReceiver, arg);


            // _realUtility.CalcDistance(null, null).ReturnsForAnyArgs(3000);

            Assert.That(_nSeparationEventsRaised == 1);




        }



    }
}
