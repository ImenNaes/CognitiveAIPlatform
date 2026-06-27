# ============================
# 1. Restore stage
# ============================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS restore
WORKDIR /src

COPY CognitiveAIPlatform/CognitiveAIPlatform.csproj CognitiveAIPlatform/
COPY Domain/Domain.csproj Domain/
COPY Application/Application.csproj Application/
COPY Infrastructure/Infrastructure.csproj Infrastructure/

# Restaurer les dépendances
RUN dotnet restore CognitiveAIPlatform/CognitiveAIPlatform.csproj

# ============================
# 2. Build + Publish stage
# ============================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copier tout le code
COPY . .

# Build + Publish
RUN dotnet publish CognitiveAIPlatform/CognitiveAIPlatform.csproj -c Release -o /app/publish

# ============================
# 3. Runtime stage
# ============================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "CognitiveAIPlatform.dll"]
