package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;

public class DriverLicense {
    @SerializedName("number")
    private String number;
    
    @SerializedName("issuedDate")
    private String issuedDate;
    
    @SerializedName("expirationDate")
    private String expirationDate;

    // Getters and setters
    public String getNumber() { return number; }
    public void setNumber(String number) { this.number = number; }
    public String getIssuedDate() { return issuedDate; }
    public void setIssuedDate(String issuedDate) { this.issuedDate = issuedDate; }
    public String getExpirationDate() { return expirationDate; }
    public void setExpirationDate(String expirationDate) { this.expirationDate = expirationDate; }
} 