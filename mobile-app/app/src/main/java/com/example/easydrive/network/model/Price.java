package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;
import java.io.Serializable;

public class Price implements Serializable {
    @SerializedName("amount")
    private double amount;
    
    @SerializedName("currency")
    private String currency;

    public double getAmount() { return amount; }
    public void setAmount(double amount) { this.amount = amount; }
    public void setCurrency(String currency) { this.currency = currency; }
} 