using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;
using ATM.EventArgsClasses;

namespace ATM.interfaces
{
    public interface ILog
    {
        void LogOnSeparationEvent(object o, SeparationAircraftsData data);
        void LogSeperationEvent(AircraftData a1, AircraftData a2);
    }
}
