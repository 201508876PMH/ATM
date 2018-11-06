using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;

namespace ATM.EventArgsClasses
{
    public class AnalysedTransponderDataEventArgs : EventArgs
    {
        /* For calling this eventhandler, we expect a list of <AircraftData>...this is received from DecocerEventHandler */
        /* When this eventhandler is triggerede (has been called from 'raised event' we set received list to localList */
        public AnalysedTransponderDataEventArgs(List<AircraftData> _localList)
        {
            _AircraftData = _localList;
        }

        public List<AircraftData> _AircraftData {get; set; }
    }
}
