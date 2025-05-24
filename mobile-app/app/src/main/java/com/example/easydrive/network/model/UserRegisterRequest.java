package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;

public class UserRegisterRequest {
    @SerializedName("email")
    private String email;
    
    @SerializedName("password")
    private String password;

    public UserRegisterRequest(String email, String password) {
        this.email = email;
        this.password = password;
    }
} 