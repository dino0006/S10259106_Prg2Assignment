//==========================================================
// Student Number : S10266598D
// Student Name : Guang Cheng
// Partner Name : Ameenuddin
//==========================================================
using ProgAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testnumber500
{
    public class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "Available")
            : base(flightNumber, origin, destination, expectedTime, status, airline:null)
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


