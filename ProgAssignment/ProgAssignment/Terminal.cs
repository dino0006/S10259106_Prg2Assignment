//==========================================================
// Student Number : S10259106E
// Student Name : Ameenuddin
// Partner Name : Guang Cheng
//==========================================================
using ProgAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testnumber500;
public class Terminal
{
    public string TerminalName { get; set; }
    public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
    public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
    public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();
    public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();



    public Terminal(string name)
    {
        TerminalName = name;
        Airlines = new Dictionary<string, Airline>();
        Flights = new Dictionary<string, Flight>();
        BoardingGates = new Dictionary<string, BoardingGate>();
        GateFees = new Dictionary<string, double>();
    }

    public bool AddAirline(Airline airline)
    {
        if (!Airlines.ContainsKey(airline.Code))
        {
            Airlines.Add(airline.Code, airline);
            return true;
        }
        Console.WriteLine($"Airline '{airline.Code}' already exists.");
        return false;
    }

    public bool AddBoardingGate(BoardingGate boardingGate)
    {
        if (!BoardingGates.ContainsKey(boardingGate.GateName))
        {
            BoardingGates.Add(boardingGate.GateName, boardingGate);
            return true;
        }
        Console.WriteLine($"Boarding gate '{boardingGate.GateName}' already exists.");
        return false;
    }

    public bool AddFlight(Flight flight)
    {
        if (!Flights.ContainsKey(flight.FlightNumber))
        {
            Flights.Add(flight.FlightNumber, flight);
            Console.WriteLine($"Added flight {flight.FlightNumber}");
            return true;
        }
        Console.WriteLine($"Flight '{flight.FlightNumber}' already exists.");
        return false;
    }

    public Airline GetAirlineFromFlight(Flight flight)
    {
        foreach (var airline in Airlines.Values)
        {
            if (airline.Flights.ContainsKey(flight.FlightNumber))
                return airline;
        }
        return null;
    }

    public void PrintAirlineFees()
    {
        foreach (var fee in GateFees)
        {
            Console.WriteLine($"Gate: {fee.Key}, Fee: {fee.Value:C}");
        }
    }

    public override string ToString()
    {
        return $"Terminal: {TerminalName}, Airlines: {Airlines.Count}, Flights: {Flights.Count}, Boarding Gates: {BoardingGates.Count}";
    }


}