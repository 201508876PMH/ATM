﻿using ATM.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.interfaces
{
    public interface IUtility
    {
        int CalcDistance(AircraftData obj1, AircraftData obj2);

        int ConvertTimeToMilliseconds(AircraftData obj);

        double Speed(AircraftData newPosition, AircraftData oldPosition);
    }
}
