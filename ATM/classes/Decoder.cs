using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM.classes
{
    class Decoder
    {
        public List<AircraftData> _Aircrafts { get; set; }
        public List<AircraftData> _OldAircraftDatas { get; set; }
    }
}
