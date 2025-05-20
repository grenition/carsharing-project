using CarRental.Domain.Enums;
using CarRental.Domain.Values;

namespace CarRental.Domain.Models;

public class RentalModel : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid CarId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public Location StartLocation { get; set; } = null!;

    public Location? EndLocation { get; set; }

    public Price Price { get; set; } = null!;

    public RentalStatus Status { get; set; }

    public UserModel UserModel { get; set; } = null!;

    public CarModel CarModel { get; set; } = null!;
}