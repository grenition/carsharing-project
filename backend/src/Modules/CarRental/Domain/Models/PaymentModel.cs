using CarRental.Domain.Enums;

namespace CarRental.Domain.Models;

public class PaymentModel : BaseEntity
{
    public Guid? RentalId          { get; set; }
    public decimal Amount          { get; set; }
    public PaymentType   Type      { get; set; }
    public DateTime      Timestamp { get; set; }
    public PaymentStatus Status    { get; set; }
}