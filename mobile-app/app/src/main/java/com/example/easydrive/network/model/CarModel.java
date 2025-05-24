package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.List;
import java.util.UUID;

public class CarModel implements Serializable {
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

    public UUID getId() { return id; }
    public void setId(UUID id) { this.id = id; }
    public String getLicensePlate() { return licensePlate; }
    public String getModel() { return model; }
    public void setModel(String model) { this.model = model; }
    public String getManufacturer() { return manufacturer; }
    public int getYear() { return year; }
    public float getFuelLevel() { return fuelLevel; }
    public Location getLocation() { return location; }
    public CarStatus getStatus() { return status; }
    public double getPricePerDay() { return pricePerDay; }
}