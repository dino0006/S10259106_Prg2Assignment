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
    public class DDJBFlight : Flight
    {
        private const double DDJB_requestfee = 300;
        private double requestFee;

        public double RequestFee
        {
            get { return requestFee; }
            set { requestFee = value; }
        }

        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "Available", double requestFee = 0.0) :
            base(flightNumber, origin, destination, expectedTime, status, airline: null)
        {
            this.requestFee = requestFee;
        }

        public override double CalculateFees()
        {
            double baseFee = 0.0;

            if (Origin == "SIN")
                baseFee = 800.0;
            else if (Destination == "SIN")
                baseFee = 500.0;

            return baseFee + requestFee;
        }


        public override string ToString()
        {
            return base.ToString() + $", RequestFee: {RequestFee:C}";
        }
    }
}
