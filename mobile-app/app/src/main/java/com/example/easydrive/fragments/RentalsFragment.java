package com.example.easydrive.fragments;

import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.DialogFragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.easydrive.R;
import com.example.easydrive.adapters.RentalAdapter;
import com.example.easydrive.network.ApiService;
import com.example.easydrive.network.model.RentalModel;
import com.google.android.material.button.MaterialButton;

import java.util.List;

import javax.inject.Inject;

import dagger.hilt.android.AndroidEntryPoint;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

@AndroidEntryPoint
public class RentalsFragment extends DialogFragment implements RentalAdapter.OnRentalEndedListener {
    private static final String TAG = "RentalsFragment";
    private RecyclerView recyclerView;
    private RentalAdapter rentalAdapter;

    @Inject
    ApiService apiService;

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setStyle(DialogFragment.STYLE_NO_TITLE, R.style.DialogTheme);
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_rentals, container, false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        Log.d(TAG, "onViewCreated: Initializing views");

        try {
            recyclerView = view.findViewById(R.id.recyclerViewRentals);
            recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));
            rentalAdapter = new RentalAdapter(apiService, getContext(), this);
            recyclerView.setAdapter(rentalAdapter);

            MaterialButton buttonClose = view.findViewById(R.id.buttonClose);
            buttonClose.setOnClickListener(v -> dismiss());

            Log.d(TAG, "onViewCreated: Starting to fetch rentals");
            fetchRentals();
        } catch (Exception e) {
            Log.e(TAG, "onViewCreated: Error initializing views", e);
            Toast.makeText(getContext(), "Error initializing rentals view", Toast.LENGTH_SHORT).show();
            dismiss();
        }
    }

    @Override
    public void onStart() {
        super.onStart();
        Window window = getDialog().getWindow();
        if (window != null) {
            window.setLayout(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT);
        }
    }

    private void fetchRentals() {
        Log.d(TAG, "fetchRentals: Making API call");
        apiService.getRentals().enqueue(new Callback<List<RentalModel>>() {
            @Override
            public void onResponse(@NonNull Call<List<RentalModel>> call, @NonNull Response<List<RentalModel>> response) {
                Log.d(TAG, "fetchRentals: Received response with code " + response.code());
                if (response.isSuccessful() && response.body() != null) {
                    List<RentalModel> rentals = response.body();
                    Log.d(TAG, "fetchRentals: Received " + rentals.size() + " rentals");
                    try {
                        rentalAdapter.setRentalList(rentals);
                        if (rentals.isEmpty()) {
                            Toast.makeText(getContext(), "No rentals found.", Toast.LENGTH_SHORT).show();
                        }
                    } catch (Exception e) {
                        Log.e(TAG, "fetchRentals: Error setting rental list", e);
                        Toast.makeText(getContext(), "Error displaying rentals", Toast.LENGTH_SHORT).show();
                    }
                } else {
                    Log.e(TAG, "fetchRentals: Failed with code " + response.code());
                    Toast.makeText(getContext(), "Failed to fetch rentals.", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(@NonNull Call<List<RentalModel>> call, @NonNull Throwable t) {
                Log.e(TAG, "fetchRentals: Network error", t);
                Toast.makeText(getContext(), "Network error fetching rentals.", Toast.LENGTH_SHORT).show();
            }
        });
    }

    @Override
    public void onRentalEnded() {
        fetchRentals();
        
        if (getParentFragment() instanceof MainPageFragment) {
            ((MainPageFragment) getParentFragment()).refreshCarList();
        }
    }
} 