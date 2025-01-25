using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgAssignment
{
    public class DDJBFlight : Flight
    {
        private double requestFee;
        public double RequestFee
        {
            get { return requestFee; }
            set { requestFee = value; }
        }
        public override double CalculateFees()
        {
            return RequestFee;
        }
        public override string ToString
        {
        return 
    }
    }
