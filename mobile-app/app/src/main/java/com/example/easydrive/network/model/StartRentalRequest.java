package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;
import java.util.UUID;

public class StartRentalRequest {
    @SerializedName("userId")
    private UUID userId;
    
    @SerializedName("carId")
    private UUID carId;
    
    @SerializedName("startLocation")
    private Location startLocation;

    // Getters and setters
    public UUID getUserId() { return userId; }
    public void setUserId(UUID userId) { this.userId = userId; }
    public UUID getCarId() { return carId; }
    public void setCarId(UUID carId) { this.carId = carId; }
    public Location getStartLocation() { return startLocation; }
    public void setStartLocation(Location startLocation) { this.startLocation = startLocation; }
} 