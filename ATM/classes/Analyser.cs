using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.EventArgsClasses;
using ATM.interfaces;
using TransponderReceiver;

namespace ATM.classes
{
    public class Analyser : IAnalyser
    {
        private IUtility _utility;
        //private ILog _log;

        public List<AircraftData> _FilteredAircrafts { get; set; }
        public List<AircraftData> _OldFilteredAircrafts { get; set; } = new List<AircraftData>();

        /* Our [Analyser]eventhandler for hooking to the [Decoder]eventhandler */
        public event EventHandler<AnalysedTransponderDataEventArgs> AnalysedDataReadyEvent;

        public event EventHandler<SeparationAircraftsData> SeparationEvent;
        public event EventHandler<AircraftData> TrackEnteredAirSpaceEvent;
        public event EventHandler<AircraftData> TrackLeftAirSpaceEvent;

        public Analyser(IUtility utility, IDecoder decoder)
        {
            _FilteredAircrafts = new List<AircraftData>();
            //_OldFilteredAircrafts = new List<AircraftData>();
            _utility = utility;

            /* Here we hook our [Analyser]eventHandler to the AnalyserOfTransponderDataEventArgs method */
            decoder.DecodedDataReadyEvent += AnalyseEventMethod;
        }

        /* The method from which we hook to. This method a default parameter signature, which must be held */
        /* Our method then calls the last executing method from our class[Analyser] with the matching argument */
        public void AnalyseEventMethod(object data, DecodedTransponderDataEventArgs e)
        {
            AnalyseData(e._AircraftData);
        }
        

        public void FilterAircrafts(List<AircraftData> _list)
        {
            _OldFilteredAircrafts = _utility.CloneList(_FilteredAircrafts);

            _FilteredAircrafts.Clear();

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

        public void AnalyseData(List<AircraftData> _aircrafts)
        {
            FilterAircrafts(_aircrafts);
            CheckForTrackEnteredAirspace();
            CheckForTrackLeftAirspace();

            for (int i = 0; i < _FilteredAircrafts.Count(); i++)
            {
                for (int j = i + 1; j < _FilteredAircrafts.Count(); j++)
                {
                    if (CheckForCollision(_FilteredAircrafts[i], _FilteredAircrafts[j]) == true)
                    {
                        SeparationEvent(this, new SeparationAircraftsData(_FilteredAircrafts[i], _FilteredAircrafts[j]));
                    }
                }
            }
            /* We remember to raise an event for the next class, that wants to hook */
            AnalysedDataReadyEvent(this, new AnalysedTransponderDataEventArgs(_FilteredAircrafts));
        }



        public void CheckForTrackEnteredAirspace()
        {
            foreach (var item in _FilteredAircrafts)
            {
                if (CheckIfTrackIsNewInAirspace(item))
                {
                    //raise track entered airspace event
                    TrackEnteredAirSpaceEvent(this, item);
                }
            }
        }

        public bool CheckIfTrackIsNewInAirspace(AircraftData track)
        {
            foreach (var item in _OldFilteredAircrafts)
            {
                if (item.Tag == track.Tag)
                {
                    return false;
                }
            }
            return true;
        }

        public void CheckForTrackLeftAirspace()
        {
            foreach (var item in _OldFilteredAircrafts)
            {
                if (CheckIfTrackIsGoneFromAirspace(item))
                {
                    //raise event
                    TrackLeftAirSpaceEvent(this, item);
                }
            }
        }

        public bool CheckIfTrackIsGoneFromAirspace(AircraftData OldTrack)
        {
            foreach (var item in _FilteredAircrafts)
            {
                if (item.Tag == OldTrack.Tag)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
