using ATM.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.classes
{
    public class ConsoleOutPutter : IConsoleOutPutter
    {

        public IAnalyser _Analyser { get; set; }


        public ConsoleOutPutter(IAnalyser analyser)
        {
            _Analyser = analyser;

                        +=


        }
        
        public void ClearConsole()
        {
            Console.Clear();
        }

        public void OutPutAircraftsWithinArea(List<AircraftData> aircraftData)
        {
            Console.WriteLine("Received transponder data:");

            foreach (var item in aircraftData)
            {
                Console.WriteLine(item.ToString());
            }
        }

     
    }
}
