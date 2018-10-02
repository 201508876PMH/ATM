using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.classes
{
    public class Analyser
    {
        public Analyser()
        {
            _FilteredAircrafts = new List<AircraftData>();
        }

        private void FilterAircrafts(List<AircraftData> _list)
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

        public List<AircraftData> _FilteredAircrafts;
    }
}
