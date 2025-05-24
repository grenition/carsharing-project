package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;

public enum PaymentType {
    @SerializedName("0")
    Rental(0),
    
    @SerializedName("1")
    TopUp(1);

    private final int value;

    PaymentType(int value) {
        this.value = value;
    }

    public int getValue() {
        return value;
    }
} 