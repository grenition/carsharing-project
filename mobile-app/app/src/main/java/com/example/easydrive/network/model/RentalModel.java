package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;
import java.util.UUID;

public class RentalModel {
    @SerializedName("id")
    private UUID id;
    
    @SerializedName("userId")
    private UUID userId;
    
    @SerializedName("carId")
    private UUID carId;
    
    @SerializedName("startTime")
    private String startTime;
    
    @SerializedName("endTime")
    private String endTime;
    
    @SerializedName("startLocation")
    private Location startLocation;
    
    @SerializedName("endLocation")
    private Location endLocation;
    
    @SerializedName("price")
    private Price price;
    
    @SerializedName("status")
    private RentalStatus status;
    
    @SerializedName("userModel")
    private UserModel userModel;
    
    @SerializedName("carModel")
    private CarModel carModel;

    // Getters and setters
    public UUID getId() { return id; }
    public void setId(UUID id) { this.id = id; }
    public UUID getUserId() { return userId; }
    public void setUserId(UUID userId) { this.userId = userId; }
    public UUID getCarId() { return carId; }
    public void setCarId(UUID carId) { this.carId = carId; }
    public String getStartTime() { return startTime; }
    public void setStartTime(String startTime) { this.startTime = startTime; }
    public String getEndTime() { return endTime; }
    public void setEndTime(String endTime) { this.endTime = endTime; }
    public Location getStartLocation() { return startLocation; }
    public void setStartLocation(Location startLocation) { this.startLocation = startLocation; }
    public Location getEndLocation() { return endLocation; }
    public void setEndLocation(Location endLocation) { this.endLocation = endLocation; }
    public Price getPrice() { return price; }
    public void setPrice(Price price) { this.price = price; }
    public RentalStatus getStatus() { return status; }
    public void setStatus(RentalStatus status) { this.status = status; }
    public UserModel getUserModel() { return userModel; }
    public void setUserModel(UserModel userModel) { this.userModel = userModel; }
    public CarModel getCarModel() { return carModel; }
    public void setCarModel(CarModel carModel) { this.carModel = carModel; }
} 