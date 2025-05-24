package com.example.easydrive;

import android.os.Bundle;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import com.example.easydrive.fragments.AuthCheckFragment;
import com.example.easydrive.fragments.SplashFragment;
import com.example.easydrive.utils.TokenManager;

import dagger.hilt.android.AndroidEntryPoint;
import javax.inject.Inject;

@AndroidEntryPoint
public class MainActivity extends AppCompatActivity {

    @Inject
    TokenManager tokenManager;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        if (savedInstanceState == null) {
            if (tokenManager.hasToken()) {
                // If token exists, go to AuthCheckFragment to validate it
                getSupportFragmentManager()
                        .beginTransaction()
                        .replace(R.id.fragmentContainer, new AuthCheckFragment())
                        .commit();
            } else {
                // No token, show splash screen
                getSupportFragmentManager()
                        .beginTransaction()
                        .replace(R.id.fragmentContainer, new SplashFragment())
                        .commit();
            }
        }
    }
}