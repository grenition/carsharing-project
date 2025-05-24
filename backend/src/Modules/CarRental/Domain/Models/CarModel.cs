using CarRental.Domain.Enums;
using CarRental.Domain.Values;

namespace CarRental.Domain.Models;

public class CarModel : BaseEntity
{
    public string VIN { get; set; } = null!;

    public string LicensePlate { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public int Year { get; set; }
    public decimal PricePerDay { get; set; }

    public float FuelLevel { get; set; }

    public Location Location { get; set; } = null!;

    public CarStatus Status { get; set; }

    public int CurrentMileage { get; set; }

    public DateTime LastServiceDate { get; set; }

    public ICollection<RentalModel>? Rentals { get; set; }
}