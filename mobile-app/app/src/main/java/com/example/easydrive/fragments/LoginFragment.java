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
import com.example.easydrive.network.ApiService;
import com.example.easydrive.databinding.FragmentLoginBinding;
import com.example.easydrive.network.model.LoginResponse;
import com.example.easydrive.network.model.UserAuthRequest;
import com.example.easydrive.utils.TokenManager;

import javax.inject.Inject;

import dagger.hilt.android.AndroidEntryPoint;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

@AndroidEntryPoint
public class LoginFragment extends Fragment {
    private FragmentLoginBinding binding;
    
    @Inject
    ApiService apiService;
    @Inject
    TokenManager tokenManager;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        binding = FragmentLoginBinding.inflate(inflater, container, false);
        return binding.getRoot();
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        
        setupLoginButton();
        setupRegisterButton();
    }

    private void setupLoginButton() {
        binding.loginButton.setOnClickListener(v -> {
            String email = binding.emailInput.getText().toString();
            String password = binding.passwordInput.getText().toString();
            
            if (email.isEmpty() || password.isEmpty()) {
                Toast.makeText(getContext(), "Email and password cannot be empty", Toast.LENGTH_SHORT).show();
                return;
            }

            UserAuthRequest loginRequest = new UserAuthRequest(email, password);
            apiService.login(loginRequest).enqueue(new Callback<LoginResponse>() {
                @Override
                public void onResponse(@NonNull Call<LoginResponse> call, @NonNull Response<LoginResponse> response) {
                    if (response.isSuccessful() && response.body() != null) {
                        // Handle successful login
                        String token = response.body().getToken();
                        tokenManager.saveToken(token);

                         if (getActivity() != null) {
                            getActivity().runOnUiThread(() -> {
                                Toast.makeText(getContext(), "Login successful!", Toast.LENGTH_SHORT).show();
                                 getActivity().getSupportFragmentManager()
                                    .beginTransaction()
                                    .setCustomAnimations(android.R.anim.fade_in, android.R.anim.fade_out)
                                    .replace(R.id.fragmentContainer, new MainPageFragment())
                                    .commit();
                            });
                         }
                    } else {
                        // Handle login error
                         if (getActivity() != null) {
                            getActivity().runOnUiThread(() -> {
                                // Since the response body is Void on success, we can't get a specific error message from it on failure directly
                                 Toast.makeText(getContext(), "Login failed. Please check credentials.", Toast.LENGTH_SHORT).show();
                            });
                         }
                    }
                }

                @Override
                public void onFailure(@NonNull Call<LoginResponse> call, @NonNull Throwable t) {
                    // Handle network errors
                    if (getActivity() != null) {
                        getActivity().runOnUiThread(() -> {
                             Toast.makeText(getContext(), "Network Error: " + t.getMessage(), Toast.LENGTH_SHORT).show();
                        });
                    }
                }
            });
        });
    }

    private void setupRegisterButton() {
        binding.registerButton.setOnClickListener(v -> {
            if (getActivity() != null) {
                getActivity().getSupportFragmentManager()
                    .beginTransaction()
                    .replace(R.id.fragmentContainer, new RegisterFragment())
                    .addToBackStack(null)
                    .commit();
            }
        });
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }
} 