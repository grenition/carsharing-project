package com.example.easydrive.fragments;

import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import com.example.easydrive.R;
import com.example.easydrive.databinding.FragmentSplashBinding;

public class SplashFragment extends Fragment {
    private FragmentSplashBinding binding;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        binding = FragmentSplashBinding.inflate(inflater, container, false);
        return binding.getRoot();
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        new Handler(Looper.getMainLooper()).postDelayed(() -> {
            if (binding != null && binding.getRoot() != null) {
                binding.getRoot().animate()
                    .alpha(0f)
                    .setDuration(250)
                    .withEndAction(() -> {
                        if (getActivity() != null) {
                            getActivity().getSupportFragmentManager()
                                .beginTransaction()
                                .replace(R.id.fragmentContainer, new LoginFragment())
                                .commit();
                        }
                    })
                    .start();
            }
        }, 1500);
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }
} 