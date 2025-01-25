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
        public double requestFee
        {
            get { return requestFee; }
            set { requestFee = value; }
        }
        public override double CalculateFees()
        {
            return requestFee
        }
        public override string ToString()
        {
            return
        }
    }
