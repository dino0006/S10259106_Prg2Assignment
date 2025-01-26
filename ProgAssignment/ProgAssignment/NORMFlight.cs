//==========================================================
// Student Number : S10266598D
// Student Name : Guang Cheng
// Partner Name : Ameenuddin
//==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgAssignment
{
    public class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "Available")
            : base(flightNumber, origin, destination, expectedTime, status)
        {
        }
        public override double CalculateFees()
        {
            return 0;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
