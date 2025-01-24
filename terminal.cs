public class Terminal
{
    private string terminalName;
    public string terminalname
    {
        get { return terminalName; }
        set { terminalName = value; }
    }
    private Dictionary<string, Airline> airlines;
    public Dictionary<string, Airline> Airlines
    {
        get { return airlines; }
        set { airlines = value; }
    }
    private Dictionary<string, Flight> flights;
    public Dictionary<string, Flight> Flights
    {
        get { return flights; }
        set { flights = value; }
    }
    private Dictionary<string, BoardingGate> boardingGates;
    public Dictinary<string, BoardingGate> BoardingGates
    {
        get { return boardingGates; }
        set { boardingGates = value; }
    }
    private Dictionary<string, double> gateFees;
    public Dictionary<string, double> GateFees;
    {
        get { return gateFees; }
        set { gateFees = value; }
    }

    public bool AddAirline(Airline)
    {
        if (!Airlines.ContainsKey(airlines.Code))
        {
            Airlines.Add(airlines.Code, airlines); 
            return true;
        }
        return false;
    }

    public bool AddBoardingGate(BoardingGate)
    {
        if (!BoardingGates.ContainsKey(boardingGates.GateName))
        {
            BoardingGates.Add(gate.GateName, boardingGates);
            return true;

        }
        return false;
    }

    public Airline GetAirlineFromFlight(Flight)
    {
        foreach (airline in Terminal.Airlines.Values)
        {
            if (airlines.Flights.ContainsKey(Flights.FlightNumber))
            {
                return airlines;
            }
       
        }
        return null;
    }
    public void PrintAirlineFees()
    {
        foreach (var airline in Airlines.Values)
        {
            Console.WriteLine($"Airline: {airline.Name}, Fees:");
        }
    }



