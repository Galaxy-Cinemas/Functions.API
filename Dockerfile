FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Galaxi.Functions.API/Galaxi.Functions.API.csproj", "Galaxi.Functions.API/"]
COPY ["Galaxi.Functions.Domain/Galaxi.Functions.Domain.csproj", "Galaxi.Functions.Domain/"]
COPY ["Galaxi.Bus.Message/Galaxi.Bus.Message.csproj", "Galaxi.Bus.Message/"]
COPY ["Galaxi.Functions.Persistence/Galaxi.Functions.Persistence.csproj", "Galaxi.Functions.Persistence/"]
COPY ["Galaxi.Functions.Data/Galaxi.Functions.Data.csproj", "Galaxi.Functions.Data/"]
RUN dotnet restore "./Galaxi.Functions.API/./Galaxi.Functions.API.csproj"
COPY . .
WORKDIR "/src/Galaxi.Functions.API"
RUN dotnet build "./Galaxi.Functions.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Galaxi.Functions.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Galaxi.Functions.API.dll"]