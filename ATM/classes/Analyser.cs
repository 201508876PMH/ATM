using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.interfaces;

namespace ATM.classes
{
    public class Analyser : IAnalyser
    {
        private IUtility _utility;

        public Analyser(IUtility utility)
        {
            _FilteredAircrafts = new List<AircraftData>();
            _utility = utility;
        }

        public void FilterAircrafts(List<AircraftData> _list)
        {
            List<AircraftData> FilteredAircrafts = new List<AircraftData>();

            foreach (var item in _list)
            {
                if ((item.Altitude >= 500 && item.Altitude <= 20000) &&
                    (item.X_coordinate >= 10000 && item.X_coordinate <= 90000) &&
                    (item.Y_coordinate <= 90000 && item.Y_coordinate >= 10000))
                {
                    _FilteredAircrafts.Add(item);
                }
            }
        }

        public bool CheckForCollision(AircraftData obj1, AircraftData obj2)
        { 
            int AltDiff = Math.Abs(obj1.Altitude - obj2.Altitude);
            int Dist = _utility.CalcDistance(obj1, obj2);

            if (AltDiff <= 300 && Dist <= 5000)
            {
                return true;
            }

            return false;
        }

        public List<AircraftData> _FilteredAircrafts;
    }
}
