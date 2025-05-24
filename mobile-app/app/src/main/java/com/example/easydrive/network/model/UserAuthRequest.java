package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;

public class UserAuthRequest {
    @SerializedName("email")
    private String email;
    
    @SerializedName("password")
    private String password;
    
    @SerializedName("baseUrl")
    private String baseUrl;

    public UserAuthRequest(String email, String password) {
        this.email = email;
        this.password = password;
    }
}