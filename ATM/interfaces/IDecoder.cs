using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;
using ATM.EventArgsClasses;
using TransponderReceiver;

namespace ATM.interfaces
{
    public interface IDecoder
    {
        // Getters and setters for airCraft list
        List<AircraftData> _Aircrafts { get; set; }
        List<AircraftData> _OldAircraftDatas { get; set; }
        
        event EventHandler<DecodedTransponderDataEventArgs> DecodedDataReadyEvent;

        void DecoderOnTransponderDataReady(object sender, RawTransponderDataEventArgs e);

        // A method for updating our lists, both old and new
        // We first clone the objects from TrasnponderData to a 'old' list
        void UpdateTransponderData(List<string> _TransponderData);

        // A method for for 'snipping' the received string from aircrafts into substrings.
        AircraftData DecodeString(string data);

        // A method for inserting speed. This method takes to lists as argument, reasoned
        // it inserts the speed 
        void InsertSpeedAndCourse(List<AircraftData> oList, List<AircraftData> nList);

    }
}
