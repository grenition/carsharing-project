namespace CarRental.Domain.Values;

public class Location
{
    public double Latitude { get; private set; }

    public double Longitude { get; private set; }

    public string Address { get; private set; }

    public Location(double latitude, double longitude, string address)
    {
        Latitude = latitude;
        Longitude = longitude;
        Address = address;
    }
}