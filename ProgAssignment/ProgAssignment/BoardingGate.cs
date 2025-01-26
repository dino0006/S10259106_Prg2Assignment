//==========================================================
// Student Number : S10259106E
// Student Name : Ameenuddin
// Partner Name : Guang Cheng
//==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgAssignment
{
    public class BoardingGate
    {
        public string GateName { get; private set; }
        private bool supportsCFFT;
        private bool supportsDJDB;
        private bool supportsLWTT;
        private Flight flight;

        public BoardingGate(string name, bool cfft, bool djdb, bool lwtt)
        {
            GateName = name;
            supportsCFFT = cfft;
            supportsDJDB = djdb;
            supportsLWTT = lwtt;
            flight = null;
        }

        public double CalculateFees()
        {
            double fee = 0.0;
            if (supportsCFFT) fee += 50.0;
            if (supportsDJDB) fee += 30.0;
            if (supportsLWTT) fee += 20.0; 
            return fee;
        }

        public override string ToString()
        {
            return $"Gate: {GateName}, Supports CFFT: {supportsCFFT}, Supports DJDB: {supportsDJDB}, Supports LWTT: {supportsLWTT}, Assigned Flight: {(flight != null ? flight.FlightId : "None")}";
        }

        public void AssignFlight(Flight assignedFlight)
        {
            flight = assignedFlight;
        }
    }
}
