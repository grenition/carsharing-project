package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;
import java.util.UUID;

public class PaymentModel {
    @SerializedName("id")
    private UUID id;
    
    @SerializedName("userId")
    private UUID userId;
    
    @SerializedName("rentalId")
    private UUID rentalId;
    
    @SerializedName("amount")
    private double amount;
    
    @SerializedName("type")
    private PaymentType type;
    
    @SerializedName("timestamp")
    private String timestamp;
    
    @SerializedName("status")
    private PaymentStatus status;
    
    @SerializedName("user")
    private UserModel user;
    
    @SerializedName("rental")
    private RentalModel rental;

    // Getters and setters
    public UUID getId() { return id; }
    public void setId(UUID id) { this.id = id; }
    public UUID getUserId() { return userId; }
    public void setUserId(UUID userId) { this.userId = userId; }
    public UUID getRentalId() { return rentalId; }
    public void setRentalId(UUID rentalId) { this.rentalId = rentalId; }
    public double getAmount() { return amount; }
    public void setAmount(double amount) { this.amount = amount; }
    public PaymentType getType() { return type; }
    public void setType(PaymentType type) { this.type = type; }
    public String getTimestamp() { return timestamp; }
    public void setTimestamp(String timestamp) { this.timestamp = timestamp; }
    public PaymentStatus getStatus() { return status; }
    public void setStatus(PaymentStatus status) { this.status = status; }
    public UserModel getUser() { return user; }
    public void setUser(UserModel user) { this.user = user; }
    public RentalModel getRental() { return rental; }
    public void setRental(RentalModel rental) { this.rental = rental; }
} 