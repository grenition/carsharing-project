namespace CarRental.Domain.Values;

public class DriverLicense
{
    public string Number { get; private set; }

    public DateTime IssuedDate { get; private set; }

    public DateTime ExpirationDate { get; private set; }

    public DriverLicense(string number, DateTime issuedDate, DateTime expirationDate)
    {
        Number = number;
        IssuedDate = issuedDate;
        ExpirationDate = expirationDate;
    }
}