public abstract class Flight
{
    private string flightNumber;
    public string FlightNumber
    {
        get { return flightNumber; }
        set { flightNumber = value; }
    }
    private string origin;
    public string Origin
    {
        get { return origin; }
        set { origin = value; }
    }
    private string destination;
    public string Destination;
    {
        get { return destination; }
        set { destination = value; } 
    }
    private DateTime expectedTime;
    public DateTime ExpectedTime
    { 
        get { return expectedTime; } 
        set { expectedTime = value; } 
    }

    private string status;
    public string Status
    {
        get { return status; }
        set { status = value; }
    }
    public Flight(string flightNumber, string airlineName, string origin, string destination, DateTime expectedTime, string status="Available")
    {
        FlightNumber = flightNumber;
        AirlineName = airlineName;
        Origin = origin;
        Destination = destination;
        ExpectedTime = expectedTime;
        Status = status;

    }

    public abstract double CalculateFees();

    public override string ToString()
    {
        return $"Flight: {FlightNumber}, Origin: {Origin}, Destination: {Destination}, Status: {Status}";
    }

}
