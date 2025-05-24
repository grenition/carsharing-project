package com.example.easydrive.network.model;

import com.google.gson.annotations.SerializedName;
import java.util.List;
import java.util.UUID;

public class UserModel {
    @SerializedName("id")
    private UUID id;
    
    @SerializedName("fullName")
    private String fullName;
    
    @SerializedName("phoneNumber")
    private String phoneNumber;
    
    @SerializedName("driverLicense")
    private DriverLicense driverLicense;
    
    @SerializedName("balance")
    private double balance;
    
    @SerializedName("status")
    private UserStatus status;
    
    @SerializedName("registeredAt")
    private String registeredAt;
    
    @SerializedName("identityId")
    private String identityId;
    
    @SerializedName("rentals")
    private List<RentalModel> rentals;
    
    @SerializedName("payments")
    private List<PaymentModel> payments;

    // Getters and setters
    public UUID getId() { return id; }
    public void setId(UUID id) { this.id = id; }
    public String getFullName() { return fullName; }
    public void setFullName(String fullName) { this.fullName = fullName; }
    public String getPhoneNumber() { return phoneNumber; }
    public void setPhoneNumber(String phoneNumber) { this.phoneNumber = phoneNumber; }
    public DriverLicense getDriverLicense() { return driverLicense; }
    public void setDriverLicense(DriverLicense driverLicense) { this.driverLicense = driverLicense; }
    public double getBalance() { return balance; }
    public void setBalance(double balance) { this.balance = balance; }
    public UserStatus getStatus() { return status; }
    public void setStatus(UserStatus status) { this.status = status; }
    public String getRegisteredAt() { return registeredAt; }
    public void setRegisteredAt(String registeredAt) { this.registeredAt = registeredAt; }
    public String getIdentityId() { return identityId; }
    public void setIdentityId(String identityId) { this.identityId = identityId; }
    public List<RentalModel> getRentals() { return rentals; }
    public void setRentals(List<RentalModel> rentals) { this.rentals = rentals; }
    public List<PaymentModel> getPayments() { return payments; }
    public void setPayments(List<PaymentModel> payments) { this.payments = payments; }
} 