using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.EventArgsClasses;
using ATM.interfaces;

namespace ATM.classes
{
    public class Log : ILog
    {
        private IAnalyser _analyser;
        private StreamWriter _stream;

        public Log(IAnalyser analyser)
        {
            _analyser = analyser;

            _analyser.SeparationEvent += LogOnSeparationEvent;

            var fd = File.Create("log.txt");
            fd.Close();

            File.AppendAllText(@"log.txt", $"Seperation events log: " + Environment.NewLine + Environment.NewLine);
        }

        public void LogOnSeparationEvent(object o, SeparationAircraftsData data)
        {
            LogSeperationEvent(data.FirstAircraft, data.SecondAircraft);
        }

        public void LogSeperationEvent(AircraftData a1, AircraftData a2)
        {
            File.AppendAllText(@"log.txt", $"WARNING! Possible collision between flight {a1.Tag} and {a2.Tag}."
                                           + Environment.NewLine + a1.ToString() + Environment.NewLine + a2.ToString() + Environment.NewLine + Environment.NewLine);
        }
    }
}
