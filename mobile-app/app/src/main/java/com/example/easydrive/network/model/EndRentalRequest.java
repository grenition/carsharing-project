package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;
import java.util.UUID;

public class EndRentalRequest {
    @SerializedName("rentalId")
    private UUID rentalId;
    
    @SerializedName("endLocation")
    private Location endLocation;

    public UUID getRentalId() { return rentalId; }
    public void setRentalId(UUID rentalId) { this.rentalId = rentalId; }
    public Location getEndLocation() { return endLocation; }
    public void setEndLocation(Location endLocation) { this.endLocation = endLocation; }
} 