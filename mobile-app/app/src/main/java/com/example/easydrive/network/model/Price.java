package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;
import java.io.Serializable;

public class Price implements Serializable {
    private static final long serialVersionUID = 1L;
    
    @SerializedName("amount")
    private double amount;
    
    @SerializedName("currency")
    private String currency;

    // Getters and setters
    public double getAmount() { return amount; }
    public void setAmount(double amount) { this.amount = amount; }
    public String getCurrency() { return currency; }
    public void setCurrency(String currency) { this.currency = currency; }
} 