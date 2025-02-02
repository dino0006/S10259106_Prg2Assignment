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

        public void LoadBoardingGatesFromCsv(string filePath)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var values = line.Split(',');
                        if (values.Length != 4)
                            continue;

                        string gateName = values[0];
                        bool supportsCFFT = bool.TryParse(values[1], out bool cfft) && cfft;
                        bool supportsDDJB = bool.TryParse(values[2], out bool ddjb) && ddjb;
                        bool supportsLWTT = bool.TryParse(values[3], out bool lwtt) && lwtt;

                        BoardingGate gate = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT);
                        AddBoardingGate(gate);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
            }
        }







        public void CalculateAndPrintAirlineFees()
        {
            foreach (var airline in airlines.Values)
            {
                double totalFees = 0;

                foreach (var flight in airline.Flights.Values)
                {
                    double flightFee = flight.CalculateFees();

                    foreach (var gate in boardingGates.Values)
                    {
                        if (flight.AssignedGate == gate.GateName)
                        {
                            flightFee += gate.CalculateFees();
                        }
                    }

                    totalFees += flightFee;
                }

                Console.WriteLine($"Airline: {airline.Code}, Total Fees: {totalFees:C}");
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
            Console.WriteLine("\n=============================================");
            Console.WriteLine($"List of Airlines for {TerminalName}");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Airline Code",-15} {"Airline Name",-30}");
            Console.WriteLine("-------------------------------------------------");

            foreach (var airline in airlines.Values)
            {
            Console.WriteLine($"{airline.Code,-15} {airline.Name,-30}");
            }
            Console.Write("Enter Flight Number to modify: ");
            string flightNumber = Console.ReadLine();

            if (!flights.ContainsKey(flightNumber))
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            Flight flight = flights[flightNumber];
            Console.WriteLine($"Current Flight Details:\nFlight Number: {flight.FlightNumber}, Origin: {flight.Origin}, Destination: {flight.Destination}, Expected Time: {flight.ExpectedTime:hh:mm tt}, Status: {flight.Status}");

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

            SaveUpdatedFlightsToFile();
        }

        private void SaveUpdatedFlightsToFile()
        {
            string filePath = "flights.csv";

            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine("Flight Number,Origin,Destination,Expected Departure/Arrival,Special Request Code");

                    foreach (var flight in flights.Values)
                    {
                        sw.WriteLine($"{flight.FlightNumber},{flight.Origin},{flight.Destination},{flight.ExpectedTime:hh:mm tt},{flight.Status}");
                    }
                }

                Console.WriteLine("Flight details saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving flight details: {ex.Message}");
            }
        }

       
}

