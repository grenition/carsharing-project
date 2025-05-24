package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;

public enum CarStatus {
    @SerializedName("0")
    Available(0),
    
    @SerializedName("1")
    Rented(1);

    private final int value;

    CarStatus(int value) {
        this.value = value;
    }

    public int getValue() {
        return value;
    }
} 