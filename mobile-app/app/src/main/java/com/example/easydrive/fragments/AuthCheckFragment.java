package com.example.easydrive.fragments;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import com.example.easydrive.R;
import com.example.easydrive.databinding.FragmentAuthCheckBinding;
import com.example.easydrive.network.ApiService;
import com.example.easydrive.network.model.CarModel;
import com.example.easydrive.utils.TokenManager;
import dagger.hilt.android.AndroidEntryPoint;
import java.util.List;
import javax.inject.Inject;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

@AndroidEntryPoint
public class AuthCheckFragment extends Fragment {

    private FragmentAuthCheckBinding binding;
    @Inject
    ApiService apiService;
    @Inject
    TokenManager tokenManager;

    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        binding = FragmentAuthCheckBinding.inflate(inflater, container, false);
        return binding.getRoot();
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        binding.buttonAuthorizeAgain.setOnClickListener(v -> {
            tokenManager.deleteToken();
            navigateToLogin();
        });

        binding.buttonContinue.setOnClickListener(v -> {
            // User explicitly chose to continue with the existing token
            navigateToMainPage();
        });

        // Perform token validation on startup
        checkAuthToken();
    }

    private void checkAuthToken() {
        String token = tokenManager.getToken();
        if (token != null) {
            // Validate token by calling a protected API endpoint (getCars)
            apiService.getCars().enqueue(new Callback<List<CarModel>>() {
                @Override
                public void onResponse(@NonNull Call<List<CarModel>> call, @NonNull Response<List<CarModel>> response) {
                    if (response.isSuccessful()) {
                        // Token is valid. Do NOT navigate immediately.
                        // The UI with continue/authorize again buttons will be shown.
                    } else {
                        // Token is invalid or expired, delete it and navigate to login
                        tokenManager.deleteToken();
                        Toast.makeText(getContext(), "Session expired. Please login again.", Toast.LENGTH_SHORT).show();
                        navigateToLogin();
                    }
                }

                @Override
                public void onFailure(@NonNull Call<List<CarModel>> call, @NonNull Throwable t) {
                    // Network error or other failure, treat as invalid token for now
                    tokenManager.deleteToken();
                     Toast.makeText(getContext(), "Network error. Cannot validate session.", Toast.LENGTH_SHORT).show();
                    navigateToLogin();
                }
            });
        } else {
            // No token found, navigate to login immediately
            navigateToLogin();
        }
    }

    private void navigateToLogin() {
        FragmentManager fragmentManager = requireActivity().getSupportFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragmentContainer, new LoginFragment());
        fragmentTransaction.commit();
    }

    private void navigateToMainPage() {
         FragmentManager fragmentManager = requireActivity().getSupportFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragmentContainer, new MainPageFragment());
        fragmentTransaction.commit();
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }
} 