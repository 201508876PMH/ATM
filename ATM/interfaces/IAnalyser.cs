using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;
using ATM.EventArgsClasses;

namespace ATM.interfaces
{
    public interface IAnalyser
    {
        List<AircraftData> _FilteredAircrafts { get; set; }
        void FilterAircrafts(List<AircraftData> _list);
        bool CheckForCollision(AircraftData obj1, AircraftData obj2);
        void AnalyseData(List<AircraftData> _aircrafts);

        event EventHandler<AnalysedTransponderDataEventArgs> AnalysedTransponderDataEventArgs;
    }
}
