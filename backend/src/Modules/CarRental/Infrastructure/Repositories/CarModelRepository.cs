using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using SharedFramework.Data.Repositories;

namespace CarRental.Infrastructure.Repositories;

public class CarModelRepository(DbContext dbContext) : EfRepository<CarModel>(dbContext);