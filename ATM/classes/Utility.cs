using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.interfaces;
using NSubstitute.Routing.Handlers;

namespace ATM.classes
{
    public class Utility : IUtility
    {
        public int CalcDistance(AircraftData obj1, AircraftData obj2)
        {
            return (int)Math.Sqrt(Math.Pow((double)(obj2.X_coordinate - obj1.X_coordinate), 2) +
                                  Math.Pow((double)(obj2.Y_coordinate - obj1.Y_coordinate), 2));
        }

        public int ConvertTimeToMilliseconds(AircraftData obj)
        {
            int hour = obj.TimeStamp.hour * 60 * 60 * 1000;
            int minut = obj.TimeStamp.min * 60 * 1000;
            int sec = obj.TimeStamp.sec * 1000;

            return hour + minut + sec + obj.TimeStamp.ms;
        }

        public double Speed(AircraftData newPosition, AircraftData oldPosition)
        {
            int timeDiff = Math.Abs(ConvertTimeToMilliseconds(newPosition) - ConvertTimeToMilliseconds(oldPosition));

            return CalcDistance(newPosition, oldPosition)/((double)timeDiff / 1000);
        }



        public double CalculateDegree(AircraftData newPosition, AircraftData oldPosition)
        {

            double vektorX = newPosition.X_coordinate - oldPosition.X_coordinate;
            double vektorY = newPosition.Y_coordinate - oldPosition.Y_coordinate;


            double angel = (Math.Atan(vektorY/vektorX) *( 180 / Math.PI));

            if (vektorX < 0)
            {
                angel += 180-90;
            }
            else if (vektorY < 0)
            {
                angel += 360-90;
            }
            else
            {
                if ((angel -90)<0)
                {
                    angel += 360-90;
                }
                else
                {
                    angel -= 90;
                }
            }
            /*
            if (angel - 90 < 0)
            {
                return angel - 90 + 360;
            }
            else
            {
                return angel - 90;
            }
            */
            

            return angel;


        }
        
    }
}
