package com.example.easydrive.di;

import com.example.easydrive.api.AuthApiService;
import com.example.easydrive.api.AuthApiServiceImpl;
import dagger.Binds;
import dagger.Module;
import dagger.hilt.InstallIn;
import dagger.hilt.components.SingletonComponent;
import javax.inject.Singleton;

@Module
@InstallIn(SingletonComponent.class)
public abstract class ApiModule {
    @Binds
    @Singleton
    public abstract AuthApiService bindAuthApiService(AuthApiServiceImpl impl);
} 