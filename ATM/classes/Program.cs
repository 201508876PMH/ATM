﻿using System;
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
            IAnalyser analyser = new Analyser(new Utility(), new Log());
            IDecoder decoder = new classes.Decoder(new Utility());

            // Dependency injection with the real TDR
            TransponderReceiverClient system = new TransponderReceiverClient(receiver, analyser, decoder);

            // Let the real TDR execute in the background
            while (true)
                Thread.Sleep(1000);

        }
    }
}
