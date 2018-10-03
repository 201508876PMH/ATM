using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;

namespace ATM.interfaces
{
    public interface ILog
    {
        void LogSeperationEvent(AircraftData a1, AircraftData a2);
    }
}
