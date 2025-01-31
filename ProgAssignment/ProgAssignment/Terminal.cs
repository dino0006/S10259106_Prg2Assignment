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


        public void LoadFlightFile(string filePath)
        {
            // Dictionary mapping airline codes to names
            var airlineNames = new Dictionary<string, string>
    {
        { "SQ", "Singapore Airlines" },
        { "MH", "Malaysia Airlines" },
        { "JL", "Japan Airlines" },
        { "CX", "Cathay Pacific" },
        { "QF", "Qantas Airways" },
        { "TR", "Scoot" },
        { "EK", "Emirates" },
        { "BA", "British Airways" },
        { "AA", "American Airlines" }
    };

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    string header = reader.ReadLine(); // Skip header row

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var parts = line.Split(',');

                        if (parts.Length < 4)
                        {
                            Console.WriteLine($"Invalid flight data: {line}");
                            continue;
                        }

                        string flightNumber = parts[0].Trim();
                        string origin = parts[1].Trim();
                        string destination = parts[2].Trim();
                        DateTime expectedTime;

                        try
                        {
                            expectedTime = DateTime.ParseExact(parts[3].Trim(), "h:mm tt", null);
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Invalid date format for flight {flightNumber}: {ex.Message}");
                            continue;
                        }

                        string specialRequestCode = parts.Length > 4 ? parts[4].Trim() : "";

                        // Extract airline code from flight number (first 2 characters)
                        string airlineCode = flightNumber.Substring(0, 2);

                        // Get airline name from dictionary or default to "Unknown Airline"
                        string airlineName = airlineNames.ContainsKey(airlineCode) ? airlineNames[airlineCode] : "Unknown Airline";

                        // Check if airline exists in dictionary, otherwise create it
                        if (!airlines.ContainsKey(airlineCode))
                        {
                            airlines[airlineCode] = new Airline(airlineCode, airlineName);
                            Console.WriteLine($"Created new airline: {airlineName} ({airlineCode})");
                        }

                        Airline airline = airlines[airlineCode];

                        // Create the correct flight type based on special request
                        Flight flight;
                        switch (specialRequestCode)
                        {
                            case "CFFT":
                                flight = new CFFTFlight(flightNumber, origin, destination, expectedTime);
                                break;
                            case "LWTT":
                                flight = new LWTTFlight(flightNumber, origin, destination, expectedTime);
                                break;
                            case "DDJB":
                                flight = new DDJBFlight(flightNumber, origin, destination, expectedTime);
                                break;
                            default:
                                flight = new NORMFlight(flightNumber, origin, destination, expectedTime);
                                break;
                        }

                        // Assign flight to airline and add to flights dictionary
                        flight.Airline = airline;
                        airline.Flights[flightNumber] = flight;
                        flights[flightNumber] = flight;

                        Console.WriteLine($"Loaded flight {flightNumber} ({airlineName}) from {origin} to {destination}.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading flights: " + ex.Message);
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


        public void ListAllFlights()
        {
            Console.WriteLine("\n=============================================");
            Console.WriteLine($"List of Flights for {TerminalName}");
            Console.WriteLine("=============================================");

            Console.WriteLine($"Number of flights loaded: {flights.Count}\n");

            if (flights.Count == 0)
            {
                Console.WriteLine("No flights available.");
                return;
            }

            Console.WriteLine($"{"Flight Number",-10} {"Airline Name",-22} {"Origin",-25} {"Destination",-25} {"Expected Departure Time"}");
            Console.WriteLine(new string('=', 90));

            foreach (var flight in flights.Values)
            {
                string airlineName = flight.Airline != null ? flight.Airline.Name : "Unknown Airline";

                Console.WriteLine($"{flight.FlightNumber,-10} {airlineName,-22} {flight.Origin,-25} {flight.Destination,-25} {flight.ExpectedTime:dd/MM/yyyy hh:mm:ss tt}");
            }
        }





        public void AssignBoardingGateToFlight(string flightNumber, string gateName)
        {
            if (!flights.ContainsKey(flightNumber))
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            var flight = flights[flightNumber];

            if (!boardingGates.ContainsKey(gateName))
            {
                Console.WriteLine("Invalid Boarding Gate.");
                return;
            }

            var gate = boardingGates[gateName];

            if (gate.AssignedFlight != null)
            {
                Console.WriteLine($"Gate {gateName} is already assigned to another flight.");
                return;
            }

            gate.AssignFlight(flight);
            Console.WriteLine($"Gate {gateName} has been assigned to flight {flightNumber}.");

            Console.Write("Would you like to update the status of the flight (Y/N)? ");
            if (Console.ReadLine().Trim().ToUpper() == "Y")
            {
                Console.WriteLine("Select a new status: Delayed, Boarding, On Time");
                string newStatus = Console.ReadLine().Trim();
                flight.Status = newStatus;
                Console.WriteLine($"Status updated to {newStatus}");
            }
        }


        public void CreateNewFlight()
        {
            Console.Write("Enter Flight Number: ");
            string flightNumber = Console.ReadLine();

            Console.Write("Enter Airline Code (e.g., SQ, MH, CX): ");
            string airlineCode = Console.ReadLine().ToUpper(); 

            Console.Write("Enter Origin: ");
            string origin = Console.ReadLine();

            Console.Write("Enter Destination: ");
            string destination = Console.ReadLine();

            Console.Write("Enter Expected Time (yyyy-MM-dd HH:mm): ");
            DateTime expectedTime;
            while (!DateTime.TryParse(Console.ReadLine(), out expectedTime))
            {
                Console.Write("Invalid date format! Please enter again (yyyy-MM-dd HH:mm): ");
            }

            Console.Write("Enter Status (default: Available): ");
            string status = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(status))
            {
                status = "Available";
            }

            
            Airline airline = null;
            if (airlines.ContainsKey(airlineCode))
            {
                airline = airlines[airlineCode];  
            }
            else
            {
                Console.WriteLine($"⚠ Airline code '{airlineCode}' not found! Using 'N/A'.");
                airline = new Airline("Unknown", "N/A"); 
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

