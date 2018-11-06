using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;

namespace ATM.EventArgsClasses
{
    public class SeparationAircraftsData : EventArgs
    {
        public SeparationAircraftsData(AircraftData a1, AircraftData a2)
        {
            FirstAircraft = a1;
            SecondAircraft = a2;
        }

        public AircraftData FirstAircraft { get; set; }
        public AircraftData SecondAircraft { get; set; }
    }
}
