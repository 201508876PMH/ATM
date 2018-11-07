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

            ILog log = new Log(analyser);
            
            while (true)
                Thread.Sleep(1000);
        }
    }
}
