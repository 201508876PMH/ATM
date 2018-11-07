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

            // Decoder will hook the on reciever Event
            IDecoder decoder = new classes.Decoder(receiver, new Utility());

            // Analyser will hook on the DecoderDataReaddyEvent
            IAnalyser analyser = new Analyser(new Utility(), decoder);

            //consoleOutPutter will 
            IConsoleOutPutter consoleOutPutter = new ConsoleOutPutter(analyser);
            

            while (true)
                Thread.Sleep(1000);

        }
    }
}
