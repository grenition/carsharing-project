package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;

public enum RentalStatus {
    @SerializedName("0")
    Active(0),
    
    @SerializedName("1")
    Completed(1),
    
    @SerializedName("2")
    Cancelled(2),
    
    @SerializedName("3")
    Scheduled(3);

    private final int value;

    RentalStatus(int value) {
        this.value = value;
    }

    public int getValue() {
        return value;
    }
} 