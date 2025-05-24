package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;
import java.io.Serializable;
import java.util.UUID;

public class RentalModel implements Serializable {
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

    public UUID getId() { return id; }
    public void setId(UUID id) { this.id = id; }
    public UUID getCarId() { return carId; }
    public void setCarId(UUID carId) { this.carId = carId; }
    public String getStartTime() { return startTime; }
    public String getEndTime() { return endTime; }
    public Price getPrice() { return price; }
    public RentalStatus getStatus() { return status; }
}