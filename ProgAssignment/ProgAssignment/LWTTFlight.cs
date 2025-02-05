//==========================================================
// Student Number : S10266598D
// Student Name : Guang Cheng
// Partner Name : Ameenuddin
//==========================================================
using ProgAssignment;
using System;

namespace testnumber500
{
    public class LWTTFlight : Flight
    {
        private const double LWTT_requestFee = 500.0;

        public double RequestFee
        {
            get { return LWTT_requestFee; }
            set { }
        }

        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "Available")
    : base(flightNumber: flightNumber, origin: origin, destination: destination, expectedTime: expectedTime, status: status, airline: null)
        {
        }

        public override double CalculateFees()
        {
            double baseFee = 0.0;

            if (Origin == "SIN")
                baseFee = 800.0;
            else if (Destination == "SIN")
                baseFee = 500.0;

            return baseFee + LWTT_requestFee;
        }

        public override string ToString()
        {
            return base.ToString() + $", RequestFee: {RequestFee:C}";
        }
    }
}
