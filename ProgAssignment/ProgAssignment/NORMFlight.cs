using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgAssignment
{
    public class NORMFlight : Flight
    {
        public override double CalculateFees()
        {
            return 100.0;
        }
        public override string ToString()
        {
            return base.ToString();
        }

    }
}

