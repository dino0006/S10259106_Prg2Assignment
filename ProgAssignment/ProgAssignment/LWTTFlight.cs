public class LWTTFlight : Flight
{
    private double requestFee;
    public double RequestFee
    {
        get { return requestFee; }
        set { requestFee = value; }
    }
    public override double CalculateFees()
    {
        return RequestFee;
    }
    public override string ToString()
    {
        return string.Format(
    }
