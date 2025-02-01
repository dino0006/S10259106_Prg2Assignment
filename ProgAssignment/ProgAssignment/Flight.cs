//==========================================================
// Student Number : S10259106E
// Student Name : Ameenuddin
// Partner Name : Guang Cheng
//==========================================================
//student id: s10266598d
//student name: guangcheng
//partner name: ameen
using System;
using testnumber500;

namespace ProgAssignment
{
    public class Flight
    {
        private string flightNumber;
        public string FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
        }

        private string origin;
        public string Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        private string destination;
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        private DateTime expectedTime;
        public DateTime ExpectedTime
        {
            get { return expectedTime; }
            set { expectedTime = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string AssignedGate { get; internal set; }

        private Airline airline;
        public Airline Airline
        {
            get { return airline; }
            set { airline = value; }
        }

        public string FlightId { get; internal set; }

        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status = "Available", Airline airline = null)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = status;
            Airline = airline;
        }

        public abstract double CalculateFees();

        public override string ToString()
        {
            if (Airline == null)
            {
                return $"Flight: {FlightNumber}, Origin: {Origin}, Destination: {Destination}, Status: {Status}, Airline: N/A";
            }
            return $"Flight: {FlightNumber}, Origin: {Origin}, Destination: {Destination}, Status: {Status}, Airline: {Airline.Name}";
        }
    }
}


