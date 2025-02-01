using ProgAssignment;
using System;
using System.Collections.Generic;
using System.IO;

namespace testnumber500
{
    public class Program
    {
        static Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();
        static Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();
        static Dictionary<string, Flight> flights = new Dictionary<string, Flight>();

        public string TerminalName { get; private set; }

        public static void Main(string[] args)
        {
            LoadBoardingGates();

            Terminal terminal = new Terminal("Changi Airport Terminal 5");

            LoadFlightFile("flights.csv");
            LoadAirlines("airlines.csv");

            while (true)
            {
                Console.WriteLine("\n=============================================");
                Console.WriteLine("Welcome to Changi Airport Terminal 5");
                Console.WriteLine("=============================================");
                Console.WriteLine("1. List All Flights");
                Console.WriteLine("2. List Boarding Gates");
                Console.WriteLine("3. Assign a Boarding Gate to a Flight");
                Console.WriteLine("4. Create Flight");
                Console.WriteLine("5. Display Airline Flights");
                Console.WriteLine("6. Modify Flight Details");
                Console.WriteLine("7. Display Flight Schedule");
                Console.WriteLine("8. Load Flights from CSV");
                Console.WriteLine("9. Calculate Airline Fees");
                Console.WriteLine("0. Exit");

                Console.Write("Please select your option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    ListAllFlights();
                }
                else if (choice == "2")
                {
                    ListBoardingGates();
                }
                else if (choice == "3")
                {

                    AssignBoardingGatesToFlight();
                }
                else if (choice == "4")
                {
                    CreateNewFlight();
                }
                else if (choice == "5")
                {
                    DisplayAirlineFlights();
                }
                else if (choice == "6")
                {
                    ModifyFlightDetails();
                }
                else if (choice == "7")
                {
                    DisplayFlightSchedule();
                }

                else if (choice == "0")
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
        }



        // Loading Airlines from CSV and storing in the dictionary
        public static void LoadAirlines(string file)
        {
            Console.WriteLine("Loading Airlines...");
            using (var lines = new StreamReader(file))
            {
                string headerLine = lines.ReadLine();
                string line;
                while ((line = lines.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length >= 2)
                    {
                        string code = parts[0].Trim();
                        string name = parts[1].Trim();
                        airlines[code] = new Airline(code, name);
                    }
                }
            }
            Console.WriteLine($"{airlines.Count} Airlines Loaded!");
        }

        //Option 1 (List All Flights)
        public static void LoadFlightFile(string file)
        {
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
                { "AA", "American Airlines" }};

            Console.WriteLine("Loading Flights from CSV...");
            using (StreamReader reader = new StreamReader(file))
            
    
            {

                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] lines = line.Split(",");

                    string flightNumber = lines[0].Trim();
                    string airlineCode = lines[1].Trim();
                    string origin = lines[2].Trim();
                    string destination = lines[3].Trim();
                    DateTime expectedTime = Convert.ToDateTime(lines[4].Trim());


                    Airline airline = airlines.ContainsKey(airlineCode) ? airlines[airlineCode] : new Airline(airlineCode, "Unknown");


                    Flight newFlight = new DDJBFlight(flightNumber, origin, destination, expectedTime, airline, "Available");
                    flights[flightNumber] = newFlight;
                }
            }
            Console.WriteLine($"{flights.Count} Flights Loaded!");
        }


        public void ListAllFlights()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Flights for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");

            foreach (var flight in flights.Values)
            {
                Console.WriteLine("{0,-15} {1,-22} {2,-22} {3,-22} {4:dd/MM/yyyy hh:mm:ss tt}",
                    flight.FlightNumber,
                    flight.Airline.Name,
                    flight.Origin,
                    flight.Destination,
                    flight.ExpectedTime);
            }


        }

        //Option 2 (List Boarding Gates)
        public static void LoadBoardingGates()
        {
            Console.WriteLine("Loading boarding gates...");
            using (var lines = new StreamReader("boardinggates.csv"))
            {
                string headerLine = lines.ReadLine();
                string line;
                while ((line = lines.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length >= 4)
                    {
                        string gateName = parts[0].Trim();
                        bool supportsCFFT = parts[1].Trim().ToLower() == "true";
                        bool supportsDDJB = parts[2].Trim().ToLower() == "true";
                        bool supportsLWTT = parts[3].Trim().ToLower() == "true";
                        boardingGates[gateName] = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT);
                    }
                }
            }
            Console.WriteLine($"{boardingGates.Count} Boarding Gates Loaded!");
        }
        public void ListBoardingGates()
        {
            Console.WriteLine("\n=============================================");
            Console.WriteLine("List of Boarding Gates for " + TerminalName);
            Console.WriteLine("=============================================");
            Console.WriteLine("Gate Name       Supports CFFT   Supports DDJB   Supports LWTT");

            foreach (var gate in boardingGates.Values)
            {
                Console.WriteLine(value: $"{gate.GateName}         {gate.SupportsCFFT}             {gate.SupportsDDJB}              {gate.SupportsLWTT}");
            }
        }
        //Option 3 (Assign Boarding Gates to Flight)

        public void AssignBoardingGatesToFlight()
        {
            Console.Write("Enter Flight Number: ");
            string flightNumber = Console.ReadLine();

            if (!flights.ContainsKey(flightNumber))
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            Flight flight = flights[flightNumber];
            Console.WriteLine($"Flight Details: {flight}");

            while (true)
            {
                Console.Write("Enter Boarding Gate: ");
                string gateName = Console.ReadLine();

                if (!boardingGates.ContainsKey(gateName))
                {
                    Console.WriteLine("Invalid Boarding Gate. Try again.");
                    continue;
                }

                if (boardingGates[gateName].AssignedFlight != null)
                {
                    Console.WriteLine("Boarding Gate already assigned. Choose another gate.");
                    continue;
                }

                flight.AssignedGate = gateName;
                boardingGates[gateName].AssignedFlight = flight;
                break;
            }

            Console.Write("Would you like to update the flight status? (Y/N): ");
            string updateStatus = Console.ReadLine().ToUpper();
            if (updateStatus == "Y")
            {
                Console.Write("Enter new status (Delayed/Boarding/On Time): ");
                flight.Status = Console.ReadLine();
            }
            else
            {
                flight.Status = "On Time";
            }

            Console.WriteLine("Boarding Gate successfully assigned!");
        }


        // Option 4 (Create New Flight)
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

            Airline airline = airlines.ContainsKey(airlineCode) ? airlines[airlineCode] : new Airline(airlineCode, "Unknown");

            Flight newFlight = new DDJBFlight(flightNumber, origin, destination, expectedTime, airline, status);
            flights[flightNumber] = newFlight;
            Console.WriteLine("\n Flight Created Successfully!");
        }

        // Option 5 (Display Airline Flights)
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
                foreach (var flight in flights.Values)
                {
                    if (flight.Airline.Code == airlineCode)
                    {
                        Console.WriteLine($"Flight Number: {flight.FlightNumber}, Origin: {flight.Origin}, Destination: {flight.Destination}, Expected Time: {flight.ExpectedTime}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Airline not found.");
            }
        }

        // Option 6 (Modify Flight Details)
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
        //Option 7 (Display Flight Schedule)
    }

}

        
