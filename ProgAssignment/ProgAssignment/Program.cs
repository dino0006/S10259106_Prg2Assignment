using ProgAssignment;
using testnumber500;
using static ProgAssignment.Terminal;

var airlines = new Dictionary<string, Airline>();
var boardingGates = new Dictionary<string, BoardingGate>();
var flights = new Dictionary<string, Flight>();

Console.WriteLine("Loading Airlines...");
using (var lines = new StreamReader("airlines.csv"))
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



try
{
    Terminal terminal = new Terminal("Changi Airport Terminal 5");


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
        Console.WriteLine("0. Exit");

        Console.Write("Please select your option: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ListAllFlights();
                break;

            case "2":
                Console.WriteLine("\n=================================================");
                Console.WriteLine(" List of Boarding Gates for Changi Airport Terminal 5 ");
                Console.WriteLine("=================================================");
                Console.WriteLine($"{"Gate Name",-10} {"DDJB",-6} {"CFFT",-6} {"LWTT",-6}");

                foreach (var gate in boardingGates.Values)
                {
                    Console.WriteLine($"{gate.GateName,-10} {gate.supportsDDJB,-6} {gate.supportsCFFT,-6} {gate.supportsLWTT,-6}");
                }
                break;

            case "3":
                /*Console.Write("Enter Flight Number: ");
                string flightNumber = Console.ReadLine();
                Console.Write("Enter Boarding Gate Name: ");
                string gateName = Console.ReadLine();
                AssignBoardingGateToFlight();
                break;

            case "4":
                CreateNewFlight();
                break;

            case "5":
                DisplayAirlineFlights();
                break;

            case "6":
                ModifyFlightDetails();
                break;

            case "7":
                DisplayFlightSchedule();
                break;

            case "0":
                Console.WriteLine("Goodbye!");
                return;

            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

//Option 1 (List all Flights)
void ListAllFlights()
{
    Console.WriteLine("\n=============================================");
    Console.WriteLine($"List of Flights for Changi Airport Terminal 5");
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

void LoadFlightFile(string flightFilePath)
{
    try
    {
        using (var reader = new StreamReader(flightFilePath))
        {
            string header = reader.ReadLine();

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






                Console.WriteLine($"Loaded flight {flightNumber} from {origin} to {destination}.");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading flights: {ex.Message}");
    }
}


//Option 2 (Load Boarding Gate)
void ListBoardingGates()
{
    Console.WriteLine("\n=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Gate Name       Supports CFFT   Supports DDJB   Supports LWTT");

    foreach (var gate in boardingGates.Values)
    {
        Console.WriteLine(value: $"{gate.GateName}         {gate.supportsCFFT}             {gate.supportsDDJB}              {gate.supportsLWTT}");
    }
}
void LoadBoardingGateFile(string boardingGateFilePath)
{
    try
    {
        using (var reader = new StreamReader(boardingGateFilePath))
        {
            string header = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var parts = line.Split(',');

                if (parts.Length < 4)
                {
                    Console.WriteLine($"Invalid boarding gate data: {line}");
                    continue;
                }

                string gateName = parts[0].Trim();
                bool supportsDDJB = bool.Parse(parts[1].Trim());
                bool supportsCFFT = bool.Parse(parts[2].Trim());
                bool supportsLWTT = bool.Parse(parts[3].Trim());


                BoardingGate gate = new BoardingGate(gateName, supportsDDJB, supportsCFFT, supportsLWTT);
                boardingGates[gateName] = gate;

                Console.WriteLine($"Loaded boarding gate: {gateName}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading boarding gates: {ex.Message}");
    }
}

//Option 3 (Assign Boarding Gate to Flight)

void AssignBoardingGateToFlight();
{
    Console.Write("Enter Flight Number: ");
    string flightNumber = Console.ReadLine();

    if (string.IsNullOrEmpty(flightNumber) || !flights.ContainsKey(flightNumber))
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

        if (string.IsNullOrEmpty(gateName) || !boardingGates.ContainsKey(gateName))
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
    string updateStatus = Console.ReadLine()?.ToUpper();
    if (updateStatus == "Y")
    {
        Console.Write("Enter new status (Delayed/Boarding/On Time): ");
        string newStatus = Console.ReadLine();
        if (!string.IsNullOrEmpty(newStatus))
        {
            flight.Status = newStatus;
        }
        else
        {
            Console.WriteLine("Invalid status. Keeping current status.");
        }
    }
    else
    {
        flight.Status = "On Time";
    }

    Console.WriteLine("Boarding Gate successfully assigned!");
}

//Option 4 (Create Flight)
void CreateNewFlight()
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

//Option 5 (Display Airline Flight)

void DisplayAirlineFlights()
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


//Option 6 (Modify Flight Details)
void ModifyFlightDetails()
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
void SaveFlightToFile(Flight flight)
{
    string filePath = "flights.csv";
    using (StreamWriter sw = File.AppendText(filePath))
    {
        string flightData = $"{flight.FlightNumber},{flight.Origin},{flight.Destination},{flight.ExpectedTime:hh:mm tt},{flight.Status}";
        sw.WriteLine(flightData);
    }
}
//Option 7 (Display Flight Schedule)
void DisplayFlightSchedule()
{
    Console.WriteLine("\n=============================================");
    Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");

    foreach (var flight in flights.Values)
    {
        Console.WriteLine($"{flight.FlightNumber}          {flight.Airline.Name}        {flight.Origin}            {flight.Destination}           {flight.ExpectedTime:dd/MM/yyyy hh:mm:ss tt}");
    }
}

