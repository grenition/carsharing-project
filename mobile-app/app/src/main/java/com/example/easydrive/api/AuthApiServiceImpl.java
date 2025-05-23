package com.example.easydrive.api;

import android.os.Handler;
import android.os.Looper;
import androidx.annotation.NonNull;
import javax.inject.Inject;
import javax.inject.Singleton;

@Singleton
public class AuthApiServiceImpl implements AuthApiService {
    private final Handler mainHandler;

    @Inject
    public AuthApiServiceImpl() {
        this.mainHandler = new Handler(Looper.getMainLooper());
    }

    @Override
    public void login(@NonNull String email, @NonNull String password, @NonNull AuthCallback callback) {
        // Simulate network delay
        new Thread(() -> {
            try {
                Thread.sleep(1000); // Simulate network delay
                
                // Mock validation
                if (email.isEmpty() || password.isEmpty()) {
                    mainHandler.post(() -> callback.onError("Email and password cannot be empty"));
                    return;
                }

                // Mock successful login
                if (email.equals("test@example.com") && password.equals("password123")) {
                    mainHandler.post(() -> callback.onSuccess("mock_token_" + System.currentTimeMillis()));
                } else {
                    mainHandler.post(() -> callback.onError("Invalid credentials"));
                }
            } catch (InterruptedException e) {
                mainHandler.post(() -> callback.onError("Login operation was interrupted"));
            }
        }).start();
    }

    @Override
    public void register(@NonNull String email, @NonNull String password, @NonNull String name, @NonNull AuthCallback callback) {
        // Simulate network delay
        new Thread(() -> {
            try {
                Thread.sleep(1000); // Simulate network delay
                
                // Mock validation
                if (email.isEmpty() || password.isEmpty() || name.isEmpty()) {
                    mainHandler.post(() -> callback.onError("All fields are required"));
                    return;
                }

                // Mock successful registration
                if (email.equals("test@example.com")) {
                    mainHandler.post(() -> callback.onError("Email already exists"));
                } else {
                    mainHandler.post(() -> callback.onSuccess("mock_token_" + System.currentTimeMillis()));
                }
            } catch (InterruptedException e) {
                mainHandler.post(() -> callback.onError("Registration operation was interrupted"));
            }
        }).start();
    }
} 