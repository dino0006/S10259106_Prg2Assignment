using ProgAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using testnumber500;



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

IDictionary<string, Flight> FlightDictionary = new Dictionary<string, Flight>();

using (StreamReader sr = new("flights.csv"))
{
    double a = 0;
    string? b = sr.ReadLine();
    while ((b = sr.ReadLine()) != null)
    {
        a += 1;
        string[] flights = b.Split(',');
        NORMFlight flightdata = new NORMFlight(flights[0], flights[1], flights[2], Convert.ToDateTime(flights[3]));
        FlightDictionary.Add(flights[0], flightdata);
    }

    Console.WriteLine("Loading Flights...");
    Console.WriteLine($"{a} Flights loaded.");
}

DisplayMenu();







//Option 1
void ListAllFlights()
{
    List<Flight> FlightList = new List<Flight>();
    List<Airline> FAirline = new List<Airline>();
    string Name = " ";

    Console.WriteLine("\n============================================================");
    Console.WriteLine($"List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("==============================================================");

    foreach (var c in FlightDictionary)
    {
        Flight flightdata = c.Value;
        FlightList.Add(flightdata);
    }

    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        string? s = sr.ReadLine();
        while ((s = sr.ReadLine()) != null)
        {
            string[] flights = s.Split(",");
            Airline airline = new Airline
            {
                Name = flights[0],
                Code = flights[1],
            };
            FAirline.Add(airline);
        }
    }


    Console.WriteLine($"{"Flight Number",-10} {"Airline Name",-22} {"Origin",-25} {"Destination",-25} {"Expected Departure Time"}");

    for (int i = 0; i < FlightList.Count(); i++)
    {
        Flight D = FlightList[i];
        string[] split = D.FlightNumber.Split(" ");

        for (int j = 0; j < FAirline.Count(); j++)
        {
            if (FAirline[j].Code == split[0])
            {
                Name = FAirline[j].Name;
                break;
            }
        }

        Console.WriteLine($"{D.FlightNumber,-10} {Name,-22} {D.Origin,-25} {D.Destination,-25} {D.ExpectedTime}");
    }
}




//Option 2
void ListBoardingGates()
{
    Console.WriteLine("\n==========================================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("============================================================");
    Console.WriteLine("Gate Name");
    foreach (var gate in boardingGates.Values)
    {
        Console.WriteLine($"{gate.GateName}");
    }
}
//Option 3
void AssignBoardingGateToFlight()
{
    Console.WriteLine("===================================================");
    Console.WriteLine("Assign a Boarding Gate to a Flight");
    Console.WriteLine("===================================================");

    Console.Write("Enter Flight Number: ");
    string FlightNumber = Console.ReadLine();
    Flight GetFlightData = FlightDictionary[FlightNumber];
    List<Flight> flights = new List<Flight> { GetFlightData };
    Console.Write("Enter Boarding Gate Name: ");
    string GateName = Console.ReadLine();
    BoardingGate GetBoardingData = boardingGates[GateName];
    if (GetBoardingData.Flight != null)
    {
        Console.WriteLine($"Boarding Gate {GateName} is already assigned, please try again");
        return;
    }
    List<string> SpecialCode = new List<string>();
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        string? s = sr.ReadLine();
        while ((s = sr.ReadLine()) != null)
        {
            string[] line = s.Split(',');
            if (line[0] == FlightNumber)
            {
                if (line[4] == "")
                {
                    line[4] = "None";
                }
                SpecialCode.Add(line[4]);
            }
        }
    }

    Console.WriteLine("Flight Number: {0}", GetFlightData.FlightNumber);
    Console.WriteLine("Origin: {0}", GetFlightData.Origin);
    Console.WriteLine("Destination: {0}", GetFlightData.Destination);
    Console.WriteLine("Expected Time: {0}", GetFlightData.ExpectedTime);
    Console.WriteLine("Special Request Code: {0}", SpecialCode[0]);
    Console.WriteLine("Boarding Gate Name: {0}", GetBoardingData.GateName);
    Console.WriteLine("Supports DDJB: {0}", GetBoardingData.SupportsDDJB);
    Console.WriteLine("Supports CFFT: {0}", GetBoardingData.SupportsCFFT);
    Console.WriteLine("Supports LWTT: {0}", GetBoardingData.SupportsLWTT);


    Console.Write("Would you like to update the status of the flight? (Y/N) ");
    string update = Console.ReadLine();

    if (update == "Y")
    {
        Console.WriteLine("1. Delayed");
        Console.WriteLine("2. Boarding");
        Console.WriteLine("3. On Time");
        Console.Write("Please select the new status of the flight: ");
        string status = Console.ReadLine();

        if (status == "1")
        {
            GetFlightData.Status = "Delayed";
        }
        else if (status == "2")
        {
            GetFlightData.Status = "Boarding";
        }
        else if (status == "3")
        {
            GetFlightData.Status = "On Time";
        }
    }

    GetBoardingData.Flight = GetFlightData;
    Console.WriteLine($"Flight {FlightNumber} has been assigned to Boarding Gate {GateName}!");
}
// Option 4
void CreateNewFlight()
{
    while (true)
    {
        Console.Write("Enter Flight Number: ");
        string flightNumber = Console.ReadLine();

        Console.Write("Enter Origin: ");
        string origin = Console.ReadLine();

        Console.Write("Enter Destination: ");
        string destination = Console.ReadLine();

        Console.Write("Enter Expected Time (yyyy-MM-dd HH:mm): ");
        DateTime ExpectedTime = Convert.ToDateTime(Console.ReadLine());


        Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
        string RequestCode = Console.ReadLine();

        NORMFlight flightdata = new NORMFlight(flightNumber, origin, destination, ExpectedTime, RequestCode);
        FlightDictionary.Add(flightNumber, flightdata);

        using (StreamWriter write = new StreamWriter("flights.csv", true))
        {
            if (RequestCode == "None")
            {
                RequestCode = " ";

            }
            Console.WriteLine($"{flightNumber}, {origin}, {destination}, {ExpectedTime}, {RequestCode}");
        }
        Console.WriteLine($"Flight {flightNumber} has been added");

        Console.WriteLine("Would You Like to Add Another Flight? (Y/N)");
        string answer = Console.ReadLine();

        if (answer == "N")
        {
            break;
        }
        else { continue; }
    }
}

//Option 5
void DisplayAirlineFlights()
{
    Console.Write("Enter Airline Code: ");
    string airlineCode = Console.ReadLine();

    if (airlines.ContainsKey(airlineCode))
    {
        Airline airline = airlines[airlineCode];
        Console.WriteLine($"\n=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
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

/*Option 6
void ModifyFlightDetails()
{
    Console.WriteLine("\n=============================================");
    Console.WriteLine($"List of Airlines for Changi Airport Terminal 5");
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

void SaveUpdatedFlightsToFile()
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
}*/

//Option 7
void DisplayFlightSchedule()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-15} {1,-25} {2,-25} {3,-25} {4,-36} {5,-16} {6,-10}",
        "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time", "Status", "Boarding Gate");

    List<Flight> flights = new List<Flight>(FlightDictionary.Values);
    flights.Sort();

    List<Airline> airlines = new List<Airline>();
    string[] airlineLines = File.ReadAllLines("airlines.csv");
    for (int i = 1; i < airlineLines.Length; i++)
    {
        string[] parts = airlineLines[i].Split(',');
        airlines.Add(new Airline { Code = parts[0].Trim(), Name = parts[1].Trim() });
    }

    List<BoardingGate> boardingGates = new List<BoardingGate>();

    foreach (var flight in flights)
    {
        string airlineName = "Unknown Airline";
        foreach (var airline in airlines)
        {
            if (flight.FlightNumber.StartsWith(airline.Code))
            {
                airlineName = airline.Name;
                break;
            }
        }

        string gateName = "Unassigned";
        foreach (var gate in boardingGates)
        {
            if (gate.Flight != null && gate.Flight.FlightNumber == flight.FlightNumber)
            {
                gateName = gate.GateName;
                break;
            }
        }

        Console.WriteLine("{0,-15} {1,-25} {2,-25} {3,-25} {4,-36} {5,-16} {6,-10}",
            flight.FlightNumber, airlineName, flight.Origin, flight.Destination, flight.ExpectedTime, flight.Status, gateName);
    }
}
void DisplayMenu()
{
    try

    {

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
                    ListBoardingGates();
                    break;

                case "3":
                    AssignBoardingGateToFlight();
                    break;

                case "4":
                    CreateNewFlight();
                    break;

                case "5":
                    DisplayAirlineFlights();
                    break;

                /* case "6":
                     ModifyFlightDetails();
                     break;*/

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

}















