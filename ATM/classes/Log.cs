using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.interfaces;

namespace ATM.classes
{
    public class Log : ILog
    {
        public Log()
        {
            File.Create("log.txt");
        }

        public void LogSeperationEvent(AircraftData a1, AircraftData a2)
        {
            File.AppendAllText(@"log.txt", $"WARNING! Possible collision between flight {a1.Tag} and {a2.Tag}."
                                           + Environment.NewLine + a1.ToString() + Environment.NewLine + a2.ToString() + Environment.NewLine + Environment.NewLine);
        }
    }
}
