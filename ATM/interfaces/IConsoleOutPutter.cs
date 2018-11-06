using ATM.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.interfaces
{
    public interface IConsoleOutPutter
    {
        
        void ClearConsole();

        
        void OutPutAircraftsWithinArea(List<AircraftData> aircraftData);




    }
}
