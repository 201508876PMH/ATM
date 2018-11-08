using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.EventArgsClasses;
using ATM.interfaces;
using Castle.Core.Internal;

namespace ATM.classes
{
    public class Log : ILog
    {
        private IAnalyser _analyser;
        private List<SeparationAircraftsData> _listOfTwoPlanesAlreadyInvolvedInSeparationEvents;

        public Log(IAnalyser analyser)
        {
            _analyser = analyser;
            _listOfTwoPlanesAlreadyInvolvedInSeparationEvents = new List<SeparationAircraftsData>();

            _analyser.SeparationEvent += LogOnSeparationEvent;

            var fd = File.Create("log.txt");
            fd.Close();

            File.AppendAllText(@"log.txt", $"Seperation events log: " + Environment.NewLine + Environment.NewLine);
        }

        public void LogOnSeparationEvent(object o, SeparationAircraftsData data)
        {
            if (_listOfTwoPlanesAlreadyInvolvedInSeparationEvents.IsNullOrEmpty())
            {
                _listOfTwoPlanesAlreadyInvolvedInSeparationEvents.Add(data);
            }
            else
            {
                foreach (var item in _listOfTwoPlanesAlreadyInvolvedInSeparationEvents)
                {
                    if ((item.FirstAircraft.Tag == data.FirstAircraft.Tag && item.SecondAircraft.Tag == data.SecondAircraft.Tag) ||
                        (item.FirstAircraft.Tag == data.SecondAircraft.Tag && item.SecondAircraft.Tag == data.FirstAircraft.Tag))
                    {
                        return;
                    }
                }
                _listOfTwoPlanesAlreadyInvolvedInSeparationEvents.Add(data);
            }
            
            LogSeperationEvent(data.FirstAircraft, data.SecondAircraft);
        }

        public void LogSeperationEvent(AircraftData a1, AircraftData a2)
        {
            
           File.AppendAllText(@"log.txt", $"WARNING! Possible collision between flight {a1.Tag} and {a2.Tag}."
                                           + Environment.NewLine + a1.ToString() + Environment.NewLine + a2.ToString() + Environment.NewLine + Environment.NewLine);
        }
    }
}
