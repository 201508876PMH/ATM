using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ATM.classes;
using ATM.interfaces;
using TransponderReceiver;

namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            // Using the real transponder data receiver

            ITransponderReceiver receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            IDecoder decoder = new classes.Decoder(receiver, new Utility());

            IAnalyser analyser = new Analyser(new Utility(), decoder);

            IConsoleOutPutter consoleOutPutter = new ConsoleOutPutter(analyser);

<<<<<<< HEAD
            ILog log = new Log(analyser);
=======

       
            /*
            ITransponderReceiver receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            IAnalyser analyser = new Analyser(new Utility(), new Log());
            //IDecoder decoder = new classes.Decoder(new Utility());

            // Dependency injection with the real TDR
            TransponderReceiverClient system = new TransponderReceiverClient(receiver, analyser, decoder);

            // Let the real TDR execute in the background
            
             */
>>>>>>> parent of cdf40cd... DecoderTest:
            
            while (true)
                Thread.Sleep(1000);
        }
    }
}
