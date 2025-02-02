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
        public Flight? AssignedFlight { get; private set; }

        public bool supportsCFFT;
        public bool supportsDDJB;
        public bool supportsLWTT;
        public bool SupportsCFFT { get; private set; }
        public bool SupportsDDJB { get; private set; }
        public bool SupportsLWTT { get; private set; }

        private Flight flight;

        public BoardingGate(string gatename, bool cfft, bool ddjb, bool lwtt)
        {
            GateName = gatename;
            supportsCFFT = cfft;
            supportsDDJB = ddjb;
            supportsDDJB = ddjb;
            supportsLWTT = lwtt;
            AssignedFlight = null;
        }

        public double CalculateFees()
        {
            double fee = 0.0;
            if (supportsCFFT) fee += 50.0;
            if (supportsDDJB) fee += 30.0;
            if (supportsLWTT) fee += 20.0; 
            return fee;
        }

        public override string ToString()
        {
            return $"Gate: {GateName}, Supports CFFT: {supportsCFFT}, Supports DDJB: {supportsDDJB}, Supports LWTT: {supportsLWTT}, Assigned Flight: {(flight != null ? flight.FlightId : "None")}";
        }

        public void AssignFlight(Flight assignedFlight)
        {
            if (assignedFlight == null)
            {
                Console.WriteLine("Error: Cannot assign a null flight.");
                return;
            }

            AssignedFlight = assignedFlight;
        }

    }
}
