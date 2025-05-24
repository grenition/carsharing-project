package com.example.easydrive.adapters;

import android.app.AlertDialog;
import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.easydrive.R;
import com.example.easydrive.network.ApiService;
import com.example.easydrive.network.model.CarModel;
import com.example.easydrive.network.model.EndRentalRequest;
import com.example.easydrive.network.model.Location;
import com.example.easydrive.network.model.RentalModel;
import com.example.easydrive.network.model.RentalStatus;
import com.google.android.material.button.MaterialButton;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.List;
import java.util.Locale;
import java.util.UUID;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class RentalAdapter extends RecyclerView.Adapter<RentalAdapter.RentalViewHolder> {
    private static final String TAG = "RentalAdapter";
    private List<RentalModel> rentalList;
    private final SimpleDateFormat dateFormat;
    private final SimpleDateFormat inputFormat;
    private final ApiService apiService;
    private Context context;
    private OnRentalEndedListener onRentalEndedListener;

    public interface OnRentalEndedListener {
        void onRentalEnded();
    }

    public RentalAdapter(ApiService apiService, Context context, OnRentalEndedListener listener) {
        this.apiService = apiService;
        this.context = context;
        this.onRentalEndedListener = listener;
        this.dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm", Locale.getDefault());
        this.inputFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSS'Z'", Locale.getDefault());
    }

    public void setRentalList(List<RentalModel> rentalList) {
        Log.d(TAG, "setRentalList: Setting " + (rentalList != null ? rentalList.size() : 0) + " rentals");
        if (rentalList != null) {
            // Sort rentals: active ones first, then by start time
            rentalList.sort((r1, r2) -> {
                if (r1.getStatus() == RentalStatus.Active && r2.getStatus() != RentalStatus.Active) {
                    return -1;
                } else if (r1.getStatus() != RentalStatus.Active && r2.getStatus() == RentalStatus.Active) {
                    return 1;
                }
                // If both are active or both are not active, sort by start time
                try {
                    return inputFormat.parse(r2.getStartTime()).compareTo(inputFormat.parse(r1.getStartTime()));
                } catch (ParseException e) {
                    return 0;
                }
            });
        }
        this.rentalList = rentalList;
        notifyDataSetChanged();
    }

    @NonNull
    @Override
    public RentalViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_rental, parent, false);
        return new RentalViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull RentalViewHolder holder, int position) {
        try {
            RentalModel rental = rentalList.get(position);
            Log.d(TAG, "onBindViewHolder: Binding rental at position " + position + ", id: " + rental.getId());
            holder.bind(rental);
        } catch (Exception e) {
            Log.e(TAG, "onBindViewHolder: Error binding rental at position " + position, e);
        }
    }

    @Override
    public int getItemCount() {
        return rentalList != null ? rentalList.size() : 0;
    }

    class RentalViewHolder extends RecyclerView.ViewHolder {
        TextView textViewCarInfo;
        TextView textViewRentalPeriod;
        TextView textViewRentalStatus;
        TextView textViewRentalPrice;
        MaterialButton buttonEndRental;

        public RentalViewHolder(@NonNull View itemView) {
            super(itemView);
            textViewCarInfo = itemView.findViewById(R.id.textViewCarInfo);
            textViewRentalPeriod = itemView.findViewById(R.id.textViewRentalPeriod);
            textViewRentalStatus = itemView.findViewById(R.id.textViewRentalStatus);
            textViewRentalPrice = itemView.findViewById(R.id.textViewRentalPrice);
            buttonEndRental = itemView.findViewById(R.id.buttonEndRental);
        }

        public void bind(RentalModel rental) {
            try {
                Log.d(TAG, "bind: Starting to bind rental " + rental.getId());
                
                // Set background color based on status
                if (rental.getStatus() == RentalStatus.Active) {
                    itemView.setBackgroundResource(R.color.active_rental_background);
                } else {
                    itemView.setBackgroundResource(android.R.color.white);
                }
                
                // Fetch detailed car information
                if (rental.getCarId() != null) {
                    apiService.getCarById(rental.getCarId().toString()).enqueue(new Callback<CarModel>() {
                        @Override
                        public void onResponse(@NonNull Call<CarModel> call, @NonNull Response<CarModel> response) {
                            if (response.isSuccessful() && response.body() != null) {
                                CarModel car = response.body();
                                String carInfo = String.format("%s %s\nLicense: %s\nYear: %d\nFuel: %.1f%%\nPrice: $%.2f/day",
                                    car.getManufacturer(),
                                    car.getModel(),
                                    car.getLicensePlate(),
                                    car.getYear(),
                                    car.getFuelLevel(),
                                    car.getPricePerDay());
                                textViewCarInfo.setText(carInfo);
                            } else {
                                Log.w(TAG, "bind: Failed to fetch car details for rental " + rental.getId());
                                textViewCarInfo.setText("Car details unavailable");
                            }
                        }

                        @Override
                        public void onFailure(@NonNull Call<CarModel> call, @NonNull Throwable t) {
                            Log.e(TAG, "bind: Error fetching car details for rental " + rental.getId(), t);
                            textViewCarInfo.setText("Error loading car details");
                        }
                    });
                } else {
                    Log.w(TAG, "bind: Car ID is null for rental " + rental.getId());
                    textViewCarInfo.setText("Car information unavailable");
                }

                // Set rental period
                if (rental.getStartTime() != null) {
                    try {
                        String startTime = dateFormat.format(inputFormat.parse(rental.getStartTime()));
                        String periodInfo = String.format("Started: %s", startTime);
                        if (rental.getEndTime() != null) {
                            String endTime = dateFormat.format(inputFormat.parse(rental.getEndTime()));
                            periodInfo += String.format("\nEnded: %s", endTime);
                        }
                        textViewRentalPeriod.setText(periodInfo);
                    } catch (ParseException e) {
                         Log.e(TAG, "bind: Error parsing date for rental " + rental.getId(), e);
                         textViewRentalPeriod.setText("");
                    }
                } else {
                    textViewRentalPeriod.setText("");
                }

                // Set rental status
                String statusText = "Status: " + getStatusText(rental.getStatus());
                textViewRentalStatus.setText(statusText);

                // Set rental price
                if (rental.getPrice() != null) {
                    String priceInfo = String.format("Total Price: $%.2f", rental.getPrice().getAmount());
                    textViewRentalPrice.setText(priceInfo);
                } else {
                    Log.w(TAG, "bind: Price is null for rental " + rental.getId());
                    textViewRentalPrice.setText("Price information unavailable");
                }
                
                // Show/Hide End Rental button based on status
                if (rental.getStatus() == RentalStatus.Active) {
                    buttonEndRental.setVisibility(View.VISIBLE);
                    buttonEndRental.setOnClickListener(v -> showEndRentalConfirmation(rental.getId()));
                } else {
                    buttonEndRental.setVisibility(View.GONE);
                }

                Log.d(TAG, "bind: Successfully bound rental " + rental.getId());
            } catch (Exception e) {
                Log.e(TAG, "bind: Unexpected error binding rental " + rental.getId(), e);
                textViewCarInfo.setText("Error displaying rental");
                textViewRentalPeriod.setText("");
                textViewRentalStatus.setText("");
                textViewRentalPrice.setText("");
                buttonEndRental.setVisibility(View.GONE);
            }
        }

        private void showEndRentalConfirmation(UUID rentalId) {
            new AlertDialog.Builder(context)
                .setTitle("End Rental")
                .setMessage("Are you sure you want to end this rental?")
                .setPositiveButton("Yes", (dialog, which) -> endRental(rentalId))
                .setNegativeButton("No", null)
                .setIcon(android.R.drawable.ic_dialog_alert)
                .show();
        }

        private void endRental(UUID rentalId) {
            // *** IMPORTANT: Replace with actual user location ***
            Location endLocation = new Location();
            endLocation.setLatitude(0.0);
            endLocation.setLongitude(0.0);
            endLocation.setAddress("Unknown Location");
            // ***************************************************

            EndRentalRequest request = new EndRentalRequest();
            request.setRentalId(rentalId);
            request.setEndLocation(endLocation);

            apiService.endRental(request).enqueue(new Callback<RentalModel>() {
                @Override
                public void onResponse(@NonNull Call<RentalModel> call, @NonNull Response<RentalModel> response) {
                    if (response.isSuccessful()) {
                        Toast.makeText(context, "Rental ended successfully!", Toast.LENGTH_SHORT).show();
                        if (onRentalEndedListener != null) {
                            onRentalEndedListener.onRentalEnded();
                        }
                    } else {
                        String errorMessage = "Failed to end rental.";
                         if (response.code() == 401) {
                            errorMessage += " Please login again.";
                        } else if (response.code() == 404) {
                            errorMessage += " Rental not found.";
                        } else if (response.code() == 409) {
                            errorMessage += " Rental already ended.";
                        }
                        Toast.makeText(context, errorMessage, Toast.LENGTH_SHORT).show();
                    }
                }

                @Override
                public void onFailure(@NonNull Call<RentalModel> call, @NonNull Throwable t) {
                    Toast.makeText(context, "Network error ending rental.", Toast.LENGTH_SHORT).show();
                     Log.e(TAG, "Error ending rental", t);
                }
            });
        }

        private String getStatusText(RentalStatus status) {
            if (status == null) {
                Log.w(TAG, "getStatusText: Status is null");
                return "Unknown";
            }
            
            switch (status) {
                case Active:
                    return "Active";
                case Completed:
                    return "Completed";
                case Cancelled:
                    return "Cancelled";
                case Scheduled:
                    return "Scheduled";
                default:
                    return "Unknown";
            }
        }
    }
} 