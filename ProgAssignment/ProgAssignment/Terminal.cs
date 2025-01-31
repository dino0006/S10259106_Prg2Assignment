//==========================================================
// Student Number : S10259106E
// Student Name : Ameenuddin
// Partner Name : Guang Cheng
//==========================================================
using System;
using System.Collections.Generic;
using System.IO;
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


        public void LoadFlightFile(string filePath)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    string header = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var parts = line.Split(',');

                        if (parts.Length < 4) continue;

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

                        string airlineCode = flightNumber.Substring(0, 2).ToUpper();
                        Airline airline = airlines.ContainsKey(airlineCode) ? airlines[airlineCode] : new Airline("Unknown", airlineCode);

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

                        flight.Airline = airline;
                        AddFlight(flight);
                        Console.WriteLine($"Flight {flightNumber} ({airline.Name}) loaded.");
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

        private const string BoardingGatesFilePath = "boardinggates.csv"; 

        public void LoadBoardingGatesFromCsv()
        {
            try
            {
                using (var reader = new StreamReader(BoardingGatesFilePath)) 
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
    }

    public void ListBoardingGates()
        {
            Console.WriteLine("\n=============================================");
            Console.WriteLine($"List of Boarding Gates for {TerminalName}");
            Console.WriteLine("=============================================");

            // Print the headers for DDJB, CFFT, and LWTT
            Console.WriteLine($"{"Gate Name",-15} {"DDJB",-20} {"CFFT",-20} {"LWTT",-20}");
            Console.WriteLine(new string('=', 80));

            foreach (var gate in boardingGates.Values)
            {
                // Print gate and its supports for each flight type
                Console.WriteLine($"{gate.GateName,-15} {gate.SupportsDDJB,-20} {gate.SupportsCFFT,-20} {gate.SupportsLWTT,-20}");
            }
        }



        private bool IsGateAssignedToFlight(string gate, string flightType)
        {
            // Check if the gate is assigned to any flight of the given flight type (DDJB, CFFT, LWTT)
            foreach (var flight in flights.Values)
            {
                if (flight.BoardingGate == gate && flight.SpecialRequestCode == flightType)
                {
                    return true;
                }
            }
            return false;
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
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{flight.FlightNumber},{flight.Origin},{flight.Destination},{flight.ExpectedTime:HH:mm},{flight.Status}");
            }
        }
    }