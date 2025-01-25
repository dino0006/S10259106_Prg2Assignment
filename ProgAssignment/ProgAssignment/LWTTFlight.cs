using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgAssignment
{
    public class LWTTFlight : Flight
    {
        private double requestFee;

        public double RequestFee
        {
            get { return requestFee; }
            set { requestFee = value; }
        }

        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "Available", double requestFee = 0.0) :
            base(flightNumber, origin, destination, expectedTime, status)
        {
            this.requestFee = requestFee;
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
