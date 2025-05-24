package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.List;
import java.util.UUID;

public class CarModel implements Serializable {
    private static final long serialVersionUID = 1L;
    
    @SerializedName("id")
    private UUID id;
    
    @SerializedName("vin")
    private String vin;
    
    @SerializedName("licensePlate")
    private String licensePlate;
    
    @SerializedName("model")
    private String model;
    
    @SerializedName("manufacturer")
    private String manufacturer;
    
    @SerializedName("year")
    private int year;
    
    @SerializedName("fuelLevel")
    private float fuelLevel;
    
    @SerializedName("location")
    private Location location;
    
    @SerializedName("status")
    private CarStatus status;
    
    @SerializedName("currentMileage")
    private int currentMileage;
    
    @SerializedName("lastServiceDate")
    private String lastServiceDate;
    
    @SerializedName("rentals")
    private List<RentalModel> rentals;

    @SerializedName("pricePerDay")
    private double pricePerDay;

    // Getters and setters
    public UUID getId() { return id; }
    public void setId(UUID id) { this.id = id; }
    public String getVin() { return vin; }
    public void setVin(String vin) { this.vin = vin; }
    public String getLicensePlate() { return licensePlate; }
    public void setLicensePlate(String licensePlate) { this.licensePlate = licensePlate; }
    public String getModel() { return model; }
    public void setModel(String model) { this.model = model; }
    public String getManufacturer() { return manufacturer; }
    public void setManufacturer(String manufacturer) { this.manufacturer = manufacturer; }
    public int getYear() { return year; }
    public void setYear(int year) { this.year = year; }
    public float getFuelLevel() { return fuelLevel; }
    public void setFuelLevel(float fuelLevel) { this.fuelLevel = fuelLevel; }
    public Location getLocation() { return location; }
    public void setLocation(Location location) { this.location = location; }
    public CarStatus getStatus() { return status; }
    public void setStatus(CarStatus status) { this.status = status; }
    public int getCurrentMileage() { return currentMileage; }
    public void setCurrentMileage(int currentMileage) { this.currentMileage = currentMileage; }
    public String getLastServiceDate() { return lastServiceDate; }
    public void setLastServiceDate(String lastServiceDate) { this.lastServiceDate = lastServiceDate; }
    public List<RentalModel> getRentals() { return rentals; }
    public void setRentals(List<RentalModel> rentals) { this.rentals = rentals; }
    public double getPricePerDay() { return pricePerDay; }
    public void setPricePerDay(double pricePerDay) { this.pricePerDay = pricePerDay; }
} 