using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;

namespace ATM.interfaces
{
    public interface IAnalyser
    {
        void FilterAircrafts(List<AircraftData> _list);
        bool CheckForCollision(AircraftData obj1, AircraftData obj2);
    }
}
