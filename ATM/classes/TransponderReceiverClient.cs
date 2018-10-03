using System;
using ATM.interfaces;
using TransponderReceiver;

namespace ATM.classes
{
    public class TransponderReceiverClient
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
            _decoder = decoder;


            // Attach to the event of the real or the fake TDR
            _receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // Decode data
            Console.Clear();
            Console.WriteLine("Received transponder data:");

            _decoder.UpdateTransponderData(e.TransponderData);

            _analyser.AnalyseData(((Decoder)_decoder)._Aircrafts);

            foreach (var item in ((Analyser)_analyser)._FilteredAircrafts)
            {
                Console.WriteLine(item.ToString());
            }
            
            //_decoder.
        }
        
    }
}
