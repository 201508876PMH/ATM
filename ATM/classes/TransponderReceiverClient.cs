using TransponderReceiver;

namespace ATM.classes
{
    class TransponderReceiverClient
    {
        private ITransponderReceiver _receiver;
        private IAnalyser _analyser;
        private IDecoder _decoder;

        // Using constructor injection
        public TransponderReceiverClient(ITransponderReceiver receiver, IAnalyser analyser, IDecoder decoder)
        {
            // set Interface fields // Fakes or real
            _receiver = receiver;
            _analyser = analyser;
            _receiver = receiver;


            // Attach to the event of the real or the fake TDR
            _receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // Decode data
            

            



        }
        
    }
}
