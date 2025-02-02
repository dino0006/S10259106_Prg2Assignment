//==========================================================
// Student Number : S10266598D
// Student Name : Guang Cheng
// Partner Name : Ameenuddin
//==========================================================
using ProgAssignment;
using System;

namespace testnumber500
{
    public class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }

        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "Available", double requestFee = 0.0)
    : base(flightNumber: flightNumber, origin: origin, destination: destination, expectedTime: expectedTime, status: status, airline: null)
        {
            this.RequestFee = requestFee;
        }


        public override double CalculateFees()
        {
            return RequestFee;
        }

        public override string ToString()
        {
            return base.ToString() + $", RequestFee: {RequestFee:C}";
        }
    }
}
