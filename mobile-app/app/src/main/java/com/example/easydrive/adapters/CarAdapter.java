package com.example.easydrive.adapters;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.google.android.material.button.MaterialButton;

import com.example.easydrive.R;
import com.example.easydrive.network.model.CarModel;

import java.util.List;

public class CarAdapter extends RecyclerView.Adapter<CarAdapter.CarViewHolder> {

    private List<CarModel> carList;

    public void setCarList(List<CarModel> carList) {
        this.carList = carList;
        notifyDataSetChanged();
    }

    @NonNull
    @Override
    public CarViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_car, parent, false);
        return new CarViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull CarViewHolder holder, int position) {
        CarModel car = carList.get(position);
        holder.bind(car);
    }

    @Override
    public int getItemCount() {
        return carList != null ? carList.size() : 0;
    }

    static class CarViewHolder extends RecyclerView.ViewHolder {
        TextView textViewCarModel;
        TextView textViewCarManufacturer;
        TextView textViewLicensePlate;
        TextView textViewCarYear;
        TextView textViewFuelLevel;
        MaterialButton buttonRent;

        public CarViewHolder(@NonNull View itemView) {
            super(itemView);
            textViewCarModel = itemView.findViewById(R.id.textViewCarModel);
            textViewCarManufacturer = itemView.findViewById(R.id.textViewCarManufacturer);
            textViewLicensePlate = itemView.findViewById(R.id.textViewLicensePlate);
            textViewCarYear = itemView.findViewById(R.id.textViewCarYear);
            textViewFuelLevel = itemView.findViewById(R.id.textViewFuelLevel);
            buttonRent = itemView.findViewById(R.id.buttonRent);
        }

        public void bind(CarModel car) {
            textViewCarModel.setText(car.getModel());
            textViewCarManufacturer.setText(car.getManufacturer());
            textViewLicensePlate.setText(car.getLicensePlate());
            textViewCarYear.setText("Year: " + car.getYear());
            // Assuming fuel level is already a percentage value (e.g., 78.5 for 78.5%)
            textViewFuelLevel.setText("Fuel: " + String.format("%.1f%%", car.getFuelLevel()));
            // TODO: Get actual price data from CarModel if available
            buttonRent.setText("$XX.XX/day"); // Placeholder price
        }
    }
} 