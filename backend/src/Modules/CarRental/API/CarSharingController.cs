using CarRental.Domain.Enums;
using CarRental.Domain.Models;
using CarRental.Domain.Values;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedFramework.Data.Repositories;

namespace CarRental.API;

[ApiController]
[Authorize]
[Route("api/carsharing")]
public class CarSharingController : ControllerBase
{
    private readonly IRepository<CarModel> _cars;
    private readonly IRepository<RentalModel> _rentals;
    private readonly IRepository<PaymentModel> _payments;

    public CarSharingController(
        IRepository<CarModel> cars,
        IRepository<RentalModel> rentals,
        IRepository<PaymentModel> payments)
    {
        _cars = cars;
        _rentals = rentals;
        _payments = payments;
    }

    // -------------------- CARS --------------------

    [HttpGet("cars")]
    public async Task<ActionResult<IEnumerable<CarModel>>> GetCars()
    {
        var cars = await _cars.GetAllAsync();
        return Ok(cars);
    }

    [HttpGet("cars/{id:guid}")]
    public async Task<ActionResult<CarModel>> GetCar(Guid id)
    {
        var car = await _cars.GetByIdAsync(id);
        return car == null ? NotFound() : Ok(car);
    }

    [HttpPost("cars")]
    public async Task<ActionResult<CarModel>> CreateCar([FromBody] CarModel car)
    {
        if (car.Id == Guid.Empty) car.Id = Guid.NewGuid();
        await _cars.AddAsync(car);
        await _cars.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
    }

    [HttpPut("cars/{id:guid}")]
    public async Task<IActionResult> UpdateCar(Guid id, [FromBody] CarModel car)
    {
        if (id != car.Id) return BadRequest();
        await _cars.UpdateAsync(car);
        await _cars.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("cars/{id:guid}")]
    public async Task<IActionResult> DeleteCar(Guid id)
    {
        var car = await _cars.GetByIdAsync(id);
        if (car == null) return NotFound();
        await _cars.DeleteAsync(car);
        await _cars.SaveChangesAsync();
        return NoContent();
    }

    // -------------------- RENTALS --------------------

    public record StartRentalRequest(Guid CarId, Location StartLocation);
    public record EndRentalRequest(Guid RentalId, Location EndLocation);

    [HttpPost("rentals/start")]
    public async Task<ActionResult<RentalModel>> StartRental([FromBody] StartRentalRequest req)
    {
        var car = await _cars.GetByIdAsync(req.CarId);
        if (car == null) return NotFound();
        if (car.Status != CarStatus.Available) return BadRequest();

        var rental = new RentalModel
        {
            Id = Guid.NewGuid(),
            CarId = car.Id,
            StartTime = DateTime.UtcNow,
            StartLocation = req.StartLocation,
            Status = RentalStatus.Active,
            Price = new Price(0m, "RUB")
        };

        car.Status = CarStatus.Rented;

        await _rentals.AddAsync(rental);
        await _cars.UpdateAsync(car);
        await _rentals.SaveChangesAsync();
        await _cars.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRental), new { id = rental.Id }, rental);
    }

    [HttpPost("rentals/end")]
    public async Task<ActionResult<RentalModel>> EndRental([FromBody] EndRentalRequest req)
    {
        var rental = await _rentals.GetByIdAsync(req.RentalId);
        if (rental == null) return NotFound();
        if (rental.Status != RentalStatus.Active) return BadRequest();

        var car = await _cars.GetByIdAsync(rental.CarId);
        if (car == null) return NotFound();

        rental.EndTime = DateTime.UtcNow;
        rental.EndLocation = req.EndLocation;

        var minutes = (rental.EndTime.Value - rental.StartTime).TotalMinutes;
        var amount = (decimal)minutes * 0.5m;

        rental.Price = new Price(amount, "RUB");
        rental.Status = RentalStatus.Completed;

        car.Status = CarStatus.Available;

        var payment = new PaymentModel
        {
            Id = Guid.NewGuid(),
            RentalId = rental.Id,
            Amount = amount,
            Type = PaymentType.Rental,
            Timestamp = DateTime.UtcNow,
            Status = PaymentStatus.Completed
        };

        await _payments.AddAsync(payment);
        await _rentals.UpdateAsync(rental);
        await _cars.UpdateAsync(car);

        await _payments.SaveChangesAsync();
        await _rentals.SaveChangesAsync();
        await _cars.SaveChangesAsync();

        return Ok(rental);
    }

    [HttpGet("rentals")]
    public async Task<ActionResult<IEnumerable<RentalModel>>> GetRentals()
    {
        var rentals = await _rentals.GetAllAsync();
        return Ok(rentals);
    }

    [HttpGet("rentals/{id:guid}")]
    public async Task<ActionResult<RentalModel>> GetRental(Guid id)
    {
        var rental = await _rentals.GetByIdAsync(id);
        return rental == null ? NotFound() : Ok(rental);
    }
}
