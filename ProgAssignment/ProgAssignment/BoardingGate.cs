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
        public object AssignedFlight { get; internal set; }

        public bool supportsCFFT;
        public bool supportsDDJB;
        public bool supportsLWTT;
        public object AssignedFlight { get; internal set; }
        public bool SupportsCFFT { get; internal set; }
        public bool SupportsDDJB { get; internal set; }
        public bool SupportsLWTT { get; internal set; }

        private bool supportsCFFT;
        private bool supportsDDJB;
        private bool supportsLWTT;
        private Flight flight;

        public BoardingGate(string name, bool cfft, bool ddjb, bool lwtt)
        {
            GateName = name;
            supportsCFFT = cfft;
            supportsDDJB = djdb;
            supportsDDJB = ddjb;
            supportsLWTT = lwtt;
            flight = null;
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
            return $"Gate: {GateName}, Supports CFFT: {supportsCFFT}, Supports DJDB: {supportsDDJB}, Supports LWTT: {supportsLWTT}, Assigned Flight: {(flight != null ? flight.FlightId : "None")}";
        }

        public void AssignFlight(Flight assignedFlight)
        {
            flight = assignedFlight;
        }
    }
}
