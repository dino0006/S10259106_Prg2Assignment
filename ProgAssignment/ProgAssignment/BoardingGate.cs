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

        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public BoardingGate() { }


        public BoardingGate(string gatename, bool cfft, bool ddjb, bool lwtt, Flight flight = null)
        {
            GateName = gatename;
            SupportsCFFT = cfft;
            SupportsDDJB = ddjb;
            SupportsDDJB = ddjb;
            SupportsLWTT = lwtt;
            AssignedFlight = null;
            Flight = flight;
        }

        public double CalculateFees()
        {
            double fee = 0.0;
            if (SupportsCFFT) fee += 50.0;
            if (SupportsDDJB) fee += 30.0;
            if (SupportsLWTT) fee += 20.0;
            return fee;
        }

        public override string ToString()
        {
            return $"Gate: {GateName}, Supports CFFT: {SupportsCFFT}, Supports DDJB: {SupportsDDJB}, Supports LWTT: {SupportsLWTT}, Assigned Flight: {(Flight != null ? Flight.FlightId : "None")}";
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
