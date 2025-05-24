package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;
import java.util.UUID;

public class TopUpRequest {
    @SerializedName("userId")
    private UUID userId;
    
    @SerializedName("amount")
    private double amount;

    // Getters and setters
    public UUID getUserId() { return userId; }
    public void setUserId(UUID userId) { this.userId = userId; }
    public double getAmount() { return amount; }
    public void setAmount(double amount) { this.amount = amount; }
} 