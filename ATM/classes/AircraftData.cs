using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.classes
{
    public class AircraftData : EventArgs, IEquatable<AircraftData>
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

        /* This method is explicitly created for unittesting Decoder cloneListSuccedes method */
        public bool Equals(AircraftData other)
        {
            if (this.Tag == other.Tag && this.Altitude == other.Altitude
                                      && this.Coords == other.Coords
                                      && this.Speed == other.Speed
                                      && this.TimeStamp == other.TimeStamp
                                      && this.X_coordinate == other.X_coordinate
                                      && this.Y_coordinate == other.Y_coordinate)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            if (string.Format( $"{Math.Round(Coords, 2)} deg").Length < 9)
            {
                return string.Format($"Tag: {Tag}\tX: {X_coordinate}m\tY: {Y_coordinate}m\tAlt: {Altitude}m\t\t" +
                                     $"Speed: {Math.Round(Speed, 2)}ms\t\tCoords: {Math.Round(Coords, 2)}deg\t\t\t{TimeStamp}");
            }

            return string.Format($"Tag: {Tag}\tX: {X_coordinate}m\tY: {Y_coordinate}m\tAlt: {Altitude}m\t\t" +
                                 $"Speed: {Math.Round(Speed, 2)}ms\t\tCoords: {Math.Round(Coords, 2)}deg\t\t{TimeStamp}");
        }
    }
}
