package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;

public enum PaymentStatus {
    @SerializedName("0")
    Pending(0),
    
    @SerializedName("1")
    Completed(1),
    
    @SerializedName("2")
    Failed(2);

    private final int value;

    PaymentStatus(int value) {
        this.value = value;
    }

    public int getValue() {
        return value;
    }
} 