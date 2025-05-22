FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/API/AppHost/AppHost.csproj", "API/AppHost/"]
COPY ["src/Shared/SharedFramework/SharedFramework.csproj", "Shared/SharedFramework/"]
COPY ["src/Modules/Users/Users.API/Users.API.csproj", "Modules/Users/Users.API/"]
COPY ["src/Modules/Users/Users.Application/Users.Application.csproj", "Modules/Users/Users.Application/"]
COPY ["src/Modules/Users/Users.Domain/Users.Domain.csproj", "Modules/Users/Users.Domain/"]
COPY ["src/Modules/Users/Users.Infrastructure/Users.Infrastructure.csproj", "Modules/Users/Users.Infrastructure/"]
COPY ["src/Modules/CarRental/CarRental.csproj", "Modules/CarRental/"]
RUN dotnet restore "./API/AppHost/AppHost.csproj"

COPY ./src .
WORKDIR "/src/API/AppHost"
RUN dotnet build "./AppHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./AppHost.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppHost.dll"]
