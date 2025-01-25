using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgAssignment
{
    public class Airline
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private Dictionary<string, Flight> Flights;
        public Dictionary<string, Flight> flights
        {
            get { return flights; }
            set { flights = value; }
        } = new Dictionary<string, Flight>();

        public bool AddFlight(Flight)
}


    



