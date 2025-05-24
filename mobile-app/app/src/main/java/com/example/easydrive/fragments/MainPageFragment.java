package com.example.easydrive.fragments;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.util.Log;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.easydrive.R;
import com.example.easydrive.adapters.CarAdapter;
import com.example.easydrive.databinding.FragmentMainPageBinding;
import com.example.easydrive.network.ApiService;
import com.example.easydrive.network.model.CarModel;
import com.example.easydrive.network.model.CarStatus;

import java.util.List;
import java.util.stream.Collectors;
import javax.inject.Inject;

import dagger.hilt.android.AndroidEntryPoint;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

import com.google.android.material.button.MaterialButton;

@AndroidEntryPoint
public class MainPageFragment extends Fragment {
    private FragmentMainPageBinding binding;

    @Inject
    ApiService apiService;

    private RecyclerView recyclerView;
    private CarAdapter carAdapter;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        binding = FragmentMainPageBinding.inflate(inflater, container, false);
        return binding.getRoot();
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        recyclerView = view.findViewById(R.id.recyclerViewCars);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));
        carAdapter = new CarAdapter(requireActivity());
        recyclerView.setAdapter(carAdapter);

        MaterialButton buttonShowRentals = view.findViewById(R.id.buttonShowRentals);
        buttonShowRentals.setOnClickListener(v -> {
            RentalsFragment rentalsFragment = new RentalsFragment();
            rentalsFragment.show(getChildFragmentManager(), "rentals_dialog");
        });

        // Set up rent confirmation listener
        carAdapter.setOnRentConfirmedListener(car -> {
            // Refresh the car list after successful rental
            fetchCars();
        });

        fetchCars();
    }

    private void fetchCars() {
        apiService.getCars().enqueue(new Callback<List<CarModel>>() {
            @Override
            public void onResponse(@NonNull Call<List<CarModel>> call, @NonNull Response<List<CarModel>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    List<CarModel> cars = response.body().stream()
                            .filter(car -> car.getStatus() == CarStatus.Available)
                            .collect(Collectors.toList());
                    carAdapter.setCarList(cars);
                    Log.d("MainPageFragment", "Fetched " + cars.size() + " available cars");
                    if (cars.isEmpty()) {
                        Toast.makeText(getContext(), "No available cars at the moment.", Toast.LENGTH_SHORT).show();
                    }
                } else {
                    Log.e("MainPageFragment", "Failed to fetch cars: " + response.code());
                    Toast.makeText(getContext(), "Failed to fetch cars.", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(@NonNull Call<List<CarModel>> call, @NonNull Throwable t) {
                Log.e("MainPageFragment", "Error fetching cars: " + t.getMessage());
                Toast.makeText(getContext(), "Network error fetching cars.", Toast.LENGTH_SHORT).show();
            }
        });
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }

    public void refreshCarList() {
        fetchCars();
    }
} 