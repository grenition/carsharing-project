package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;
import java.util.UUID;

public class StartRentalRequest {
    @SerializedName("carId")
    private UUID carId;
    
    @SerializedName("startLocation")
    private Location startLocation;

    public StartRentalRequest() {
    }

    public StartRentalRequest(UUID carId, Location startLocation) {
        this.carId = carId;
        this.startLocation = startLocation;
    }

    public UUID getCarId() { return carId; }
    public void setCarId(UUID carId) { this.carId = carId; }
    public Location getStartLocation() { return startLocation; }
    public void setStartLocation(Location startLocation) { this.startLocation = startLocation; }
} 