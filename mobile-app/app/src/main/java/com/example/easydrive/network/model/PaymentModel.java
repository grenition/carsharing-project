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
} 