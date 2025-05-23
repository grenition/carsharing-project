package com.example.easydrive.api;

import androidx.annotation.NonNull;

public interface AuthApiService {
    interface AuthCallback {
        void onSuccess(String token);
        void onError(String error);
    }

    void login(@NonNull String email, @NonNull String password, @NonNull AuthCallback callback);
    void register(@NonNull String email, @NonNull String password, @NonNull String name, @NonNull AuthCallback callback);
} 