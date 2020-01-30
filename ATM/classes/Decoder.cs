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
    public class Decoder : IDecodddder
    {
        private IUtility _utility;
        private ITransponderReceiver _receiver;

        // We create two lists, reasoned that when calculating flight speed,
        // we need the distance the flight has traveled from the previous to current location
        public List<AircraftData> _Aircrafts { get; set; } = new List<AircraftData>();
        public List<AircraftData> _OldAircraftDatas { get; set; } = new List<AircraftData>();

        public event EventHandler<DecodedTransponderDataEventArgs> DecodedDataReadyEvent;

        public Decoder(ITransponderReceiver receiver, IUtility utility)
        {
            _receiver = receiver;
            _utility = utility;
            _Aircrafts = new List<AircraftData>();
            _OldAircraftDatas = new List<AircraftData>();

            receiver.TransponderDataReady += DecoderOnTransponderDataReady;
        }

        public void DecoderOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {

            UpdateTransponderData(e.TransponderData);
        }

        
        // A method for updating our lists, both old and new
        // We first clone the objects from TrasnponderData to a 'old' list
        public void UpdateTransponderData(List<string> _TransponderData)
        {
            _OldAircraftDatas.Clear();
            _OldAircraftDatas = _utility.CloneList(_Aircrafts);
            _Aircrafts.Clear();

            foreach (var item in _TransponderData)
            {
                _Aircrafts.Add(DecodeString(item));
            }

            InsertSpeedAndCourse(_OldAircraftDatas, _Aircrafts);
            if (DecodedDataReadyEvent !=null)
            {
                DecodedDataReadyEvent(this, new DecodedTransponderDataEventArgs(_Aircrafts));
            }
            
        }


        // A method for for 'snipping' the received string from aircrafts into substrings.
        public AircraftData DecodeString(string data)
        {
            string[] strArray = data.Split(';');

            int year = int.Parse(strArray[4].Substring(0, 4));
            int month = int.Parse(strArray[4].Substring(4, 2));
            int day = int.Parse(strArray[4].Substring(6, 2));
            int hour = int.Parse(strArray[4].Substring(8, 2));
            int min = int.Parse(strArray[4].Substring(10, 2));
            int sec = int.Parse(strArray[4].Substring(12, 2));
            int ms = int.Parse(strArray[4].Substring(14, 3));

            return new AircraftData(strArray[0], int.Parse(strArray[1]), int.Parse(strArray[2]), int.Parse(strArray[3]),
                new TimeStamp(year, month, day, hour, min, sec, ms));
        }


        // A method for inserting speed. This method takes to lists as argument, reasoned
        // it inserts the speed 
        public void InsertSpeedAndCourse(List<AircraftData> oList, List<AircraftData> nList)
        {
            int i = 0;

            // We create three cases, reasoned that the old and new list may vary in length
            if (oList.Count() == nList.Count())
            {

                foreach (var item in nList)
                {
                    item.Speed = _utility.Speed(item, oList[i]);
                    item.Coords = _utility.CalculateDegree(item, oList[i]);
                    ++i;
                }
            }
            else if (oList.Count() > nList.Count())
            {
                for (int j = 0; j < nList.Count(); j++)
                {
                    nList[j].Speed = _utility.Speed(nList[j], oList[j]);
                    nList[j].Coords = _utility.CalculateDegree(nList[j], oList[j]);
                }
            }
            else
            {
                for (int j = 0; j < oList.Count(); j++)
                {
                    nList[j].Speed = _utility.Speed(nList[j], oList[j]);

                    nList[j].Coords = _utility.CalculateDegree(nList[j], oList[j]);
                }
            }
        }
    }
}
