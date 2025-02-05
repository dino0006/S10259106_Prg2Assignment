using Microsoft.VisualBasic.FileIO;
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
        string[] Flights = b.Split(',');
        NORMFlight flightdata = new NORMFlight(Flights[0], Flights[1], Flights[2], Convert.ToDateTime(Flights[3]));
        FlightDictionary.Add(Flights[0], flightdata);
    }

    Console.WriteLine("Loading Flights...");
    Console.WriteLine($"{a} Flights loaded.");
}






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
    Console.WriteLine("\n=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Gate Name       Supports CFFT   Supports DDJB   Supports LWTT");

    foreach (var gate in boardingGates.Values)
    {
        Console.WriteLine(value: $"{gate.GateName}         {gate.SupportsCFFT}             {gate.SupportsDDJB}              {gate.SupportsLWTT}");
    }
}






//Option 3
void AssignBoardingGateToFlight()
{
    Console.WriteLine("===================================================");
    Console.WriteLine("Assign a Boarding Gate to a Flight");
    Console.WriteLine("===================================================");

    Console.Write("Enter Flight Number: ");
    string FlightNumber = Console.ReadLine()?.ToUpper();
    Flight GetFlightData = FlightDictionary[FlightNumber];
    List<Flight> flights = new List<Flight> { GetFlightData };
    Console.Write("Enter Boarding Gate Name: ");
    string GateName = Console.ReadLine()?.ToUpper();
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



//Option 6
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

    Console.Write("\nEnter Airline Code to modify flights: ");
    string airlineCode = Console.ReadLine()?.ToUpper();

    if (!airlines.ContainsKey(airlineCode))
    {
        Console.WriteLine("Invalid airline code. Returning to menu.");
        return;
    }

    Airline selectedAirline = airlines[airlineCode];

    if (selectedAirline.Flights.Count == 0)
    {
        Console.WriteLine("No flights available for this airline.");
        return;
    }

    Console.WriteLine($"\nFlights for {selectedAirline.Name}:");
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine($"{"Flight Number",-10} {"Origin",-15} {"Destination",-15} {"Expected Time",-15}");
    Console.WriteLine("-------------------------------------------------");

    foreach (var flight in selectedAirline.Flights.Values)
    {
        Console.WriteLine($"{flight.FlightNumber,-10} {flight.Origin,-15} {flight.Destination,-15} {flight.ExpectedTime:dd/MM/yyyy HH:mm}");
    }
    Console.WriteLine("\nChoose an option:");
    Console.WriteLine("[1] Modify an existing flight");
    Console.WriteLine("[2] Delete an existing flight");
    Console.Write("Enter choice: ");
    string choice = Console.ReadLine();

    Flight updatedFlight = null;
    if (choice == "1")
    {
        Console.Write("Enter Flight Number to modify: ");
        string flightNumber = Console.ReadLine();

        if (!selectedAirline.Flights.ContainsKey(flightNumber))
        {
            Console.WriteLine("Flight not found.");
            return;
        }

        Flight flight = selectedAirline.Flights[flightNumber];

        Console.WriteLine("\nWhat would you like to modify?");
        Console.WriteLine("[1] Basic Information (Origin, Destination, Expected Time)");
        Console.WriteLine("[2] Status");
        Console.WriteLine("[3] Special Request Code");
        Console.WriteLine("[4] Boarding Gate");
        Console.Write("Enter choice: ");
        string modifyChoice = Console.ReadLine();

        switch (modifyChoice)
        {
            case "1":
                Console.Write($"Enter new Origin (current: {flight.Origin}): ");
                string newOrigin = Console.ReadLine();
                if (!string.IsNullOrEmpty(newOrigin)) flight.Origin = newOrigin;

                Console.Write($"Enter new Destination (current: {flight.Destination}): ");
                string newDestination = Console.ReadLine();
                if (!string.IsNullOrEmpty(newDestination)) flight.Destination = newDestination;

                Console.Write($"Enter new Expected Time (dd/MM/yyyy HH:mm) (current: {flight.ExpectedTime:dd/MM/yyyy HH:mm}): ");
                string newTimeInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(newTimeInput))
                {
                    if (DateTime.TryParseExact(newTimeInput, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime newTime))
                    {
                        flight.ExpectedTime = newTime;
                    }
                    else
                    {
                        Console.WriteLine("Invalid time format. Keeping current time.");
                    }
                }
                break;
            case "2":
                Console.Write($"Enter new Status (current: {flight.Status}): ");
                string newStatus = Console.ReadLine();
                if (!string.IsNullOrEmpty(newStatus)) flight.Status = newStatus;
                break;

            case "3":
                Console.Write($"Enter new Special Request Code (current: {flight.SpecialRequestCode ?? "None"}): ");
                string newRequestCode = Console.ReadLine();
                if (!string.IsNullOrEmpty(newRequestCode)) flight.SpecialRequestCode = newRequestCode;
                break;

            case "4":
                Console.Write($"Enter new Boarding Gate (current: {flight.BoardingGate ?? "None"}): ");
                string newBoardingGate = Console.ReadLine();
                if (!string.IsNullOrEmpty(newBoardingGate)) flight.BoardingGate = newBoardingGate;
                break;

            default:
                Console.WriteLine("Invalid choice.");
                return;
        }
        updatedFlight = flight;
        Console.WriteLine("\nFlight details updated successfully!");
    }
    else if (choice == "2")
    {
        Console.Write("Enter Flight Number to delete: ");
        string flightNumber = Console.ReadLine();

        if (!selectedAirline.Flights.ContainsKey(flightNumber))
        {
            Console.WriteLine("Flight not found.");
            return;
        }

        Console.Write($"Are you sure you want to delete flight {flightNumber}? [Y/N]: ");
        string confirmation = Console.ReadLine()?.ToUpper();

        if (confirmation == "Y")
        {
            selectedAirline.Flights.Remove(flightNumber);
            Console.WriteLine("Flight deleted successfully.");
            SaveUpdatedFlightsToFile();
        }
        else
        {
            Console.WriteLine("Flight deletion cancelled.");
        }
    }
    else
    {
        Console.WriteLine("Invalid choice.");
    }

    if (updatedFlight != null)
    {
        Console.WriteLine($"\nFlight updated!");
        Console.WriteLine($"Flight Number: {updatedFlight.FlightNumber}");
        Console.WriteLine($"Airline Name: {updatedFlight.Airline.Name}");
        Console.WriteLine($"Origin: {updatedFlight.Origin}");
        Console.WriteLine($"Destination: {updatedFlight.Destination}");
        Console.WriteLine($"Expected Departure/Arrival Time: {updatedFlight.ExpectedTime:dd/M/yyyy h:mm:ss tt}");
        Console.WriteLine($"Status: {updatedFlight.Status}");
        Console.WriteLine($"Special Request Code: {updatedFlight.SpecialRequestCode ?? "None"}");
        Console.WriteLine($"Boarding Gate: {updatedFlight.BoardingGate ?? "Unassigned"}");

        SaveUpdatedFlightsToFile();
    }
    void SaveUpdatedFlightsToFile()
    {
        string filePath = "flights.csv";

        try
        {
            if (FlightDictionary == null || FlightDictionary.Count == 0)
            {
                Console.WriteLine("No flights available to save.");
                return;
            }

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("Flight Number,Origin,Destination,Expected Departure/Arrival,Special Request Code");

                foreach (var flight in FlightDictionary.Values)
                {
                    sw.WriteLine($"{flight.FlightNumber},{flight.Origin},{flight.Destination},{flight.ExpectedTime:yyyy-MM-dd HH:mm},{flight.Status}");
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




//Option 7
void DisplayFlightSchedule()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-35} {5,-15} {6,-10}",
        "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time", "Status", "Boarding Gate");

    string Name = "";
    string BoardingGate = "Not assigned";
    List<Flight> Flight = new List<Flight>();


    List<Airline> FindingAirline = new List<Airline>();


    List<BoardingGate> Boarding = new List<BoardingGate>();

    foreach (var flight in FlightDictionary)
    {
        Flight flightdata = flight.Value;
        Flight.Add(flightdata);
    }
    Flight.Sort((f1, f2) => f1.ExpectedTime.CompareTo(f2.ExpectedTime));


    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        string? s = sr.ReadLine();
        while ((s = sr.ReadLine()) != null)
        {
            string[] line = s.Split(',');
            Airline airline = new Airline
            {
                Name = line[0],
                Code = line[1],
            };
            FindingAirline.Add(airline);
        }
    }

    foreach (var boardinggates in boardingGates)
    {
        BoardingGate boardingdata = boardinggates.Value;
        Boarding.Add(boardingdata);
    }
    for (int j = 0; j < Flight.Count(); j++)
    {
        BoardingGate = "Not Assigned";
        Flight FlightData = Flight[j];
        string[] split = FlightData.FlightNumber.Split(" ");

        for (int x = 0; x < Boarding.Count(); x++)
        {
            BoardingGate boardingGate = Boarding[x];
            Flight FlightData2 = boardingGate.Flight;
            if (FlightData2 != null)
            {
                if (FlightData2.FlightNumber == FlightData.FlightNumber)
                {
                    BoardingGate = boardingGate.GateName;
                }
            }
        }
        for (int i = 0; i < FindingAirline.Count(); i++)
        {
            Airline AirlineCode = FindingAirline[i];
            if (AirlineCode.Code == split[0])
            {
                Name = AirlineCode.Name;
            }
        }

        Console.WriteLine("{0,-15} {1,-25} {2,-25} {3,-25} {4,-36} {5,-16} {6,-10}",
        FlightData.FlightNumber, Name, FlightData.Origin, FlightData.Destination, FlightData.ExpectedTime, FlightData.Status, BoardingGate);
    }
}


void DisplayMenu()
{
    Console.WriteLine("=============================================");
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
    Console.WriteLine("==============================================");
}

bool finish = false;
while (!finish)
{
    try
    {

        DisplayMenu();

        Console.Write("Enter your choice: ");
        string userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int option))
        {

            switch (option)
            {
                case 1:
                    ListAllFlights();
                    break;
                case 2:
                    ListBoardingGates();
                    break;
                case 3:
                    AssignBoardingGateToFlight();
                    break;
                case 4:
                    CreateNewFlight();
                    break;
                case 5:
                    DisplayAirlineFlights();
                    break;
                case 6:
                    ModifyFlightDetails();
                    break;
                case 7:
                    DisplayFlightSchedule();
                    break;
                case 0:
                    Console.WriteLine("Exiting program...");
                    finish = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.\n");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.\n");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}























