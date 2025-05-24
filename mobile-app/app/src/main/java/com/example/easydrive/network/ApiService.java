package com.example.easydrive.network;

import com.example.easydrive.network.model.CarModel;
import com.example.easydrive.network.model.LoginResponse;
import com.example.easydrive.network.model.RentalModel;
import com.example.easydrive.network.model.UserAuthRequest;
import com.example.easydrive.network.model.UserModel;
import com.example.easydrive.network.model.UserRegisterRequest;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;

public interface ApiService {

    @POST("/api/users/auth/login")
    Call<Void> login(@Body UserAuthRequest request);

    @POST("/api/users/auth/register")
    Call<Void> register(@Body UserRegisterRequest request);

    @GET("/api/carsharing/cars")
    Call<List<CarModel>> getCars();

    @GET("/api/carsharing/cars/{id}")
    Call<CarModel> getCarById(@Path("id") String carId);

    @GET("/api/carsharing/users")
    Call<List<UserModel>> getUsers();

    @GET("/api/carsharing/users/{id}")
    Call<UserModel> getUserById(@Path("id") String userId);

    @GET("/api/carsharing/rentals")
    Call<List<RentalModel>> getRentals();

    @GET("/api/carsharing/rentals/{id}")
    Call<RentalModel> getRentalById(@Path("id") String rentalId);
} 