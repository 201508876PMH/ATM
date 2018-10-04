using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;
using ATM.interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class TransponderRechieverClientTest
    {

        private TransponderReceiverClient _uut;
        private IAnalyser _fakeAnalyser;
        private IDecoder _fakeDecoder;
        private ITransponderReceiver _fakeReceiver; 

        [SetUp]
        public void Setup()
        {

            _fakeAnalyser = Substitute.For<IAnalyser>();
            _fakeDecoder = Substitute.For<IDecoder>();
            _fakeReceiver = Substitute.For<ITransponderReceiver>(); 

            _uut  = new TransponderReceiverClient(_fakeReceiver, _fakeAnalyser, _fakeDecoder);
        }

        [Test]
        public void FakeEventTest()
        {
            // Setup test data
            List<string> fakeData = new List<string>();
            fakeData.Add("Test11;23412;89723;23000;20151006232032543");
            fakeData.Add("Test21;32122;65212;12000;20151006232032543");
            fakeData.Add("Test31;54654;32125;4000;20151006232032543");
            fakeData.Add("Test41;32122;65212;12000;20151006232032543");
            fakeData.Add("Test51;54654;32125;4000;20151006232032543");


            int numberOfEvents = 0;
            _fakeReceiver.TransponderDataReady += (sender, args) => numberOfEvents++;
            
            
            // EROOR here
              
            
            //Raise event with specific args, any sender:
            //_fakeReceiver.TransponderDataReady += Raise.EventWith(this, new RawTransponderDataEventArgs(fakeData)).;
            //Raise event with specific args and sender:
            //_fakeReceiver.TransponderDataReady += Raise.EventWith(this, new RawTransponderDataEventArgs(fakeData));
               
            //Assert.AreEqual(2, numberOfEvents);

            // Assert something here or use an NSubstitute Received
        }






    }
}
