FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
# EXPOSE 5001

# ENV ASPNETCORE_URLS=http://+:5001

# # Creates a non-root user with an explicit UID and adds permission to access the /app folder
# # For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
# RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
# USER appuser

# FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
# WORKDIR /src

# # When we try to build, `docker build --tag plant-swap .` We're getting errors from the RUN line below. The error: "failed to compute cache key: "/PlantSwap/PlantSwap.csproj" not found: not found"

# COPY ["PlantSwap/PlantSwap.csproj", "PlantSwap/"]
# RUN dotnet restore "PlantSwap/PlantSwap.csproj"
# COPY . .
# WORKDIR "/src/PlantSwap"
# RUN dotnet build "PlantSwap.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "PlantSwap.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "PlantSwap.dll"]

COPY . .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet plant-swap.dll