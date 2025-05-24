package com.example.easydrive.fragments;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.DialogFragment;

import com.example.easydrive.R;
import com.example.easydrive.network.ApiService;
import com.example.easydrive.network.model.CarModel;
import com.example.easydrive.network.model.Location;
import com.example.easydrive.network.model.RentalModel;
import com.example.easydrive.network.model.StartRentalRequest;
import com.google.android.material.button.MaterialButton;

import javax.inject.Inject;

import dagger.hilt.android.AndroidEntryPoint;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

import java.util.UUID;

@AndroidEntryPoint
public class RentFragment extends DialogFragment {
    private static final String ARG_CAR = "car";
    private CarModel car;
    private OnRentConfirmedListener listener;

    @Inject
    ApiService apiService;

    public interface OnRentConfirmedListener {
        void onRentConfirmed(CarModel car);
    }

    public static RentFragment newInstance(CarModel car) {
        RentFragment fragment = new RentFragment();
        Bundle args = new Bundle();
        args.putSerializable(ARG_CAR, car);
        fragment.setArguments(args);
        return fragment;
    }

    public void setOnRentConfirmedListener(OnRentConfirmedListener listener) {
        this.listener = listener;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setStyle(DialogFragment.STYLE_NO_TITLE, R.style.DialogTheme);
        if (getArguments() != null) {
            car = (CarModel) getArguments().getSerializable(ARG_CAR);
        }
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_rent, container, false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        TextView textViewCarDetails = view.findViewById(R.id.textViewCarDetails);
        TextView textViewPriceDetails = view.findViewById(R.id.textViewPriceDetails);
        MaterialButton buttonCancel = view.findViewById(R.id.buttonCancel);
        MaterialButton buttonConfirm = view.findViewById(R.id.buttonConfirm);

        // Set car details
        String carDetails = String.format("%s %s\nLicense Plate: %s\nYear: %d\nFuel Level: %.1f%%",
                car.getManufacturer(),
                car.getModel(),
                car.getLicensePlate(),
                car.getYear(),
                car.getFuelLevel());
        textViewCarDetails.setText(carDetails);

        // Set price details
        String priceDetails = String.format("Price: $%.2f per day", car.getPricePerDay());
        textViewPriceDetails.setText(priceDetails);

        // Set up button click listeners
        buttonCancel.setOnClickListener(v -> dismiss());

        buttonConfirm.setOnClickListener(v -> {
            buttonConfirm.setEnabled(false);
            buttonConfirm.setText("Processing...");
            
            // Create rental request
            Location startLocation = new Location();
            startLocation.setLatitude(car.getLocation().getLatitude());
            startLocation.setLongitude(car.getLocation().getLongitude());
            startLocation.setAddress(car.getLocation().getAddress());
            
            StartRentalRequest request = new StartRentalRequest(car.getId(), startLocation);

            // Call API to start rental
            apiService.startRental(request).enqueue(new Callback<RentalModel>() {
                @Override
                public void onResponse(@NonNull Call<RentalModel> call, @NonNull Response<RentalModel> response) {
                    if (response.isSuccessful() && response.body() != null) {
                        RentalModel rental = response.body();
                        String successMessage = String.format("Car rented successfully! Rental ID: %s", rental.getId());
                        Toast.makeText(getContext(), successMessage, Toast.LENGTH_SHORT).show();
                        if (listener != null) {
                            listener.onRentConfirmed(car);
                        }
                        dismiss();
                    } else {
                        String errorMessage = "Failed to rent car. ";
                        if (response.code() == 401) {
                            errorMessage += "Please login again.";
                        } else if (response.code() == 403) {
                            errorMessage += "Insufficient balance or invalid driver's license.";
                        } else if (response.code() == 404) {
                            errorMessage += "Car not found.";
                        } else if (response.code() == 409) {
                            errorMessage += "Car is already rented.";
                        } else {
                            errorMessage += "Please try again.";
                        }
                        Toast.makeText(getContext(), errorMessage, Toast.LENGTH_SHORT).show();
                        buttonConfirm.setEnabled(true);
                        buttonConfirm.setText("Confirm");
                    }
                }

                @Override
                public void onFailure(@NonNull Call<RentalModel> call, @NonNull Throwable t) {
                    String errorMessage = "Network error: " + t.getMessage();
                    Toast.makeText(getContext(), errorMessage, Toast.LENGTH_SHORT).show();
                    buttonConfirm.setEnabled(true);
                    buttonConfirm.setText("Confirm");
                }
            });
        });
    }
} 