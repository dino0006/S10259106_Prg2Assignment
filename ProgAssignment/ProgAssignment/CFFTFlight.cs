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
    public class CFFTFlight : Flight
    {
        private double requestFee;

        public double RequestFee
        {
            get { return requestFee; }
            set { requestFee = value; }
        }

        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "Available", double requestFee = 0.0) :
            base(flightNumber, origin, destination, expectedTime, status)
        {
            this.requestFee = requestFee;
        }

        public override double CalculateFees()
        {
            return requestFee;
        }

        public override string ToString()
        {
            return base.ToString() + $", RequestFee: {RequestFee:C}";
        }
    }
}
