using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.classes
{
    public class AircraftData : EventArgs
    {
        public AircraftData(string tag, int x_coordinate, int y_coordinate, int altitude, TimeStamp timeStamp)
        {
            Tag = tag;
            X_coordinate = x_coordinate;
            Y_coordinate = y_coordinate;
            Altitude = altitude;
            TimeStamp = timeStamp;
        }

        public string Tag { get; set; }
        public int X_coordinate { get; set; }
        public int Y_coordinate { get; set; }
        public int Altitude { get; set; }
        public TimeStamp TimeStamp { get; set; }
        public double Speed { get; set; }
        public double Coords { get; set; }
        public override string ToString()
        {
            return string.Format($"Tag: {Tag}\tX: {X_coordinate}m\tY: {Y_coordinate}m\tAlt: {Altitude}m\t\t" +
                                 $"Speed: {Math.Round(Speed, 2)}ms\t\tCoords: {Math.Round(Coords, 2)}deg\t\t{TimeStamp}");
        }
    }
}
