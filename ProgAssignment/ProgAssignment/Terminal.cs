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
using testnumber500;

namespace ProgAssignment
{
    public class Terminal
    {
        public string TerminalName { get; set; }
        private Dictionary<string, Airline> airlines;
        private Dictionary<string, Flight> flights;
        private Dictionary<string, BoardingGate> boardingGates;
        private Dictionary<string, double> gateFees;

        public Terminal(string name)
        {
            TerminalName = name;
            airlines = new Dictionary<string, Airline>();
            flights = new Dictionary<string, Flight>();
            boardingGates = new Dictionary<string, BoardingGate>();
            gateFees = new Dictionary<string, double>();
            LoadFlightFile("flights.csv");
        }

        public bool AddAirline(Airline airline)
        {
            if (!airlines.ContainsKey(airline.Code))
            {
                airlines.Add(airline.Code, airline);
                return true;
            }
            Console.WriteLine($"Airline '{airline.Code}' already exists.");
            return false;
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (!boardingGates.ContainsKey(boardingGate.GateName))
            {
                boardingGates.Add(boardingGate.GateName, boardingGate);
                return true;
            }
            Console.WriteLine($"Boarding gate '{boardingGate.GateName}' already exists.");
            return false;
        }

        public bool AddFlight(Flight flight)
        {
            if (!flights.ContainsKey(flight.FlightNumber))
            {
                flights.Add(flight.FlightNumber, flight);
                Console.WriteLine($"Added flight {flight.FlightNumber}");
                return true;
            }
            Console.WriteLine($"Flight '{flight.FlightNumber}' already exists.");
            return false;
        }

        public Airline GetAirlineFromFlight(Flight flight)
        {
            foreach (var airline in airlines.Values)
            {
                if (airline.Flights.ContainsKey(flight.FlightNumber))
                    return airline;
            }
            return null;
        }

        public void PrintAirlineFees()
        {
            foreach (var fee in gateFees)
            {
                Console.WriteLine($"Gate: {fee.Key}, Fee: {fee.Value:C}");
            }
        }

        public override string ToString()
        {
            return $"Terminal: {TerminalName}, Airlines: {airlines.Count}, Flights: {flights.Count}, Boarding Gates: {boardingGates.Count}";
        }
    }
}

            
            Flight newFlight = new DDJBFlight(flightNumber, origin, destination, expectedTime, status, 0.0)
            {
                Airline = airline  
            };

            
            flights[flightNumber] = newFlight;
            Console.WriteLine("\n Flight Created Successfully!");
        }


        public void SaveFlightToFile(Flight flight)
        {
            string filePath = "flights.csv";
            using (StreamWriter sw = File.AppendText(filePath))
            {
                string flightData = $"{flight.FlightNumber},{flight.Origin},{flight.Destination},{flight.ExpectedTime:hh:mm tt},{flight.Status}";
                sw.WriteLine(flightData);
            }
        }
        public void DisplayAirlineFlights()
        {
            Console.Write("Enter Airline Code: ");
            string airlineCode = Console.ReadLine();

            if (airlines.ContainsKey(airlineCode))
            {
                Airline airline = airlines[airlineCode];
                Console.WriteLine($"\n=============================================");
                Console.WriteLine($"Flights for {airline.Name}");
                Console.WriteLine("=============================================");
                foreach (var flight in airline.Flights.Values)
                {
                    Console.WriteLine($"Flight Number: {flight.FlightNumber}, Origin: {flight.Origin}, Destination: {flight.Destination}, Expected Time: {flight.ExpectedTime}");
                }
            }
            else
            {
                Console.WriteLine("Airline not found.");
            }
        }
        public void ModifyFlightDetails()
        {
            Console.Write("Enter Flight Number to modify: ");
            string flightNumber = Console.ReadLine();

            if (!flights.ContainsKey(flightNumber))
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            Flight flight = flights[flightNumber];
            Console.WriteLine($"Current Flight Details: {flight}");

            Console.Write("Enter new status (or leave blank to keep current): ");
            string newStatus = Console.ReadLine();
            if (!string.IsNullOrEmpty(newStatus))
            {
                flight.Status = newStatus;
            }

            Console.Write("Enter new expected time (hh:mm tt) or leave blank to keep current: ");
            string newTimeInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTimeInput))
            {
                try
                {
                    flight.ExpectedTime = DateTime.ParseExact(newTimeInput, "h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid time format. Keeping current time.");
                }
            }

            Console.WriteLine("Flight details updated.");
        }

        public void DisplayFlightSchedule()
        {
            Console.WriteLine("\n=============================================");
            Console.WriteLine("Flight Schedule for " + TerminalName);
            Console.WriteLine("=============================================");
            Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");

            foreach (var flight in flights.Values)
            {
                Console.WriteLine($"{flight.FlightNumber}          {flight.Airline.Name}        {flight.Origin}            {flight.Destination}           {flight.ExpectedTime:dd/MM/yyyy hh:mm:ss tt}");
            }
        }
        public void ListBoardingGates()
        {
            Console.WriteLine("\n=============================================");
            Console.WriteLine("List of Boarding Gates for " + TerminalName);
            Console.WriteLine("=============================================");
            Console.WriteLine("Gate Name       Supports CFFT   Supports DDJB   Supports LWTT");

            foreach (var gate in boardingGates.Values)
            {
                Console.WriteLine(value: $"{gate.GateName}         {gate.supportsCFFT}             {gate.supportsDDJB}              {gate.supportsLWTT}");
            }
        }




    }
}

