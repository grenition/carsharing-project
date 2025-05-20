using CarRental.Domain.Enums;
using CarRental.Domain.Values;

namespace CarRental.Domain.Models;

public class UserModel : BaseEntity
{
    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DriverLicense DriverLicense { get; set; } = null!;

    public decimal Balance { get; set; }

    public UserStatus Status { get; set; }

    public DateTime RegisteredAt { get; set; }

    public string IdentityId { get; set; } = null!;

    public ICollection<RentalModel>? Rentals { get; set; }

    public ICollection<PaymentModel>? Payments { get; set; }
}