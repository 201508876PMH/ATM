using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;

namespace ATM.EventArgsClasses
{
    public class DecodedTransponderDataEventArgs : EventArgs
    {
        public DecodedTransponderDataEventArgs(List<AircraftData> _list)
        {
            _AircraftData = _list;
        }

        public List<AircraftData> _AircraftData { get; set; }
    }
}
