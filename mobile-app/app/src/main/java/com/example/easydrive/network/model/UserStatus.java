package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;

public enum UserStatus {
    @SerializedName("0")
    Active(0),
    
    @SerializedName("1")
    Suspended(1),
    
    @SerializedName("2")
    Banned(2);

    private final int value;

    UserStatus(int value) {
        this.value = value;
    }

    public int getValue() {
        return value;
    }
} 