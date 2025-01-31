﻿//==========================================================
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
        private string terminalName;
        private Dictionary<string, Airline> airlines;
        private Dictionary<string, Flight> flights;
        private Dictionary<string, BoardingGate> boardingGates;
        private Dictionary<string, double> gateFees;

        public Terminal(string name)
        {
            terminalName = name;
            airlines = new Dictionary<string, Airline>();
            flights = new Dictionary<string, Flight>();
            boardingGates = new Dictionary<string, BoardingGate>();
            gateFees = new Dictionary<string, double>();
        }

        public bool AddAirline(Airline airline)
        {
            if (!airlines.ContainsKey(airline.Name))
            {
                airlines.Add(airline.Name, airline);
                return true;
            }
            return false;
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (!boardingGates.ContainsKey(boardingGate.GateName))
            {
                boardingGates.Add(boardingGate.GateName, boardingGate);
                return true;
            }
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
                Console.WriteLine($"Gate: {fee.Key}, Fee: {fee.Value}");
            }
        }

        public override string ToString()
        {
            return $"Terminal: {terminalName}, Airlines: {airlines.Count}, Flights: {flights.Count}, Boarding Gates: {boardingGates.Count}";
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
                        bool supportsCFFT = bool.Parse(values[1]);
                        bool supportsDJDB = bool.Parse(values[2]);
                        bool supportsLWTT = bool.Parse(values[3]);

                        BoardingGate gate = new BoardingGate(gateName, supportsCFFT, supportsDJDB, supportsLWTT);
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
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    string header = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var parts = line.Split(',');
                        string flightNumber = parts[0].Replace(" ", "");
                        string origin = parts[1].Replace(" ", "");
                        string destination = parts[2].Replace(" ", "");

                        DateTime expectedTime;
                        try
                        {
                            expectedTime = DateTime.ParseExact(parts[3].Replace(" ", ""), "h:mm tt", null);
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Invalid date format for flight {flightNumber}: {ex.Message}");
                            continue;
                        }

                        string specialRequestCode = parts.Length > 4 ? parts[4].Replace(" ", "") : " ";

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

                        if (!AddFlight(flight))
                        {
                            Console.WriteLine($"Failed to add flight {flight.FlightNumber}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading flights: " + ex.Message);
            }
        }

        private bool AddFlight(Flight flight)
        {
            throw new NotImplementedException();
        }
    }
}
