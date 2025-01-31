using ProgAssignment;
using static ProgAssignment.Terminal;

var airlines = new Dictionary<string, Airline>();
var boardingGates = new Dictionary<string, BoardingGate>();

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
                terminal.ListAllFlights();
                break;

            case "2":
                terminal.ListBoardingGates();
                break;

            case "3":
                Console.Write("Enter Flight Number: ");
                string flightNumber = Console.ReadLine();
                Console.Write("Enter Boarding Gate Name: ");
                string gateName = Console.ReadLine();
                terminal.AssignBoardingGateToFlight(flightNumber, gateName);
                break;

            case "4":
                terminal.CreateNewFlight();
                break;

            case "5":
                terminal.DisplayAirlineFlights();
                break;

            case "6":
                terminal.ModifyFlightDetails();
                break;

            case "7":
                terminal.DisplayFlightSchedule();
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


