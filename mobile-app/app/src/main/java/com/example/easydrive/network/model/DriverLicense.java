package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;

public class DriverLicense {
    @SerializedName("number")
    private String number;
    
    @SerializedName("issuedDate")
    private String issuedDate;
    
    @SerializedName("expirationDate")
    private String expirationDate;
}