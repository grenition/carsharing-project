using CarRental.Domain.Enums;

namespace CarRental.Domain.Models;

public class PaymentModel : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid? RentalId { get; set; }

    public decimal Amount { get; set; }

    public PaymentType Type { get; set; }

    public DateTime Timestamp { get; set; }

    public PaymentStatus Status { get; set; }

    public UserModel User { get; set; } = null!;

    public RentalModel? Rental { get; set; }
}