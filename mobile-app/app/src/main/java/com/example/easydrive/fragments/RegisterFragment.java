package com.example.easydrive.fragments;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import com.example.easydrive.R;
import com.example.easydrive.api.AuthApiService;
import com.example.easydrive.databinding.FragmentRegisterBinding;
import dagger.hilt.android.AndroidEntryPoint;
import javax.inject.Inject;

@AndroidEntryPoint
public class RegisterFragment extends Fragment {
    private FragmentRegisterBinding binding;
    
    @Inject
    AuthApiService authApiService;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        binding = FragmentRegisterBinding.inflate(inflater, container, false);
        return binding.getRoot();
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        
        setupRegisterButton();
        setupLoginButton();
    }

    private void setupRegisterButton() {
        binding.registerButton.setOnClickListener(v -> {
            String email = binding.emailInput.getText().toString();
            String password = binding.passwordInput.getText().toString();
            String name = binding.nameInput.getText().toString();
            
            authApiService.register(email, password, name, new AuthApiService.AuthCallback() {
                @Override
                public void onSuccess(String token) {
                    if (getActivity() != null) {
                        getActivity().runOnUiThread(() -> {
                            Toast.makeText(getContext(), "Registration successful!", Toast.LENGTH_SHORT).show();
                            // TODO: Navigate to main screen or store token
                        });
                    }
                }

                @Override
                public void onError(String error) {
                    if (getActivity() != null) {
                        getActivity().runOnUiThread(() -> {
                            Toast.makeText(getContext(), error, Toast.LENGTH_SHORT).show();
                        });
                    }
                }
            });
        });
    }

    private void setupLoginButton() {
        binding.loginButton.setOnClickListener(v -> {
            // Navigate back to login fragment
            if (getActivity() != null) {
                getActivity().getSupportFragmentManager().popBackStack();
            }
        });
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }
} 