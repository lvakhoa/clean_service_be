# Stage 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# restore
ENV HUSKY=0
COPY ["CleanService/CleanService.csproj", "CleanService/"]
RUN dotnet restore "CleanService/CleanService.csproj"

# Install EF Core CLI inside the build container
RUN dotnet tool install --global dotnet-ef

# Set PATH so that dotnet-ef is available
ENV PATH="$PATH:/root/.dotnet/tools"

# build
COPY ["CleanService/", "CleanService/"]
RUN dotnet build 'CleanService/CleanService.csproj' -c Release -o /app/build

# Stage 2: Publish Stage
FROM build AS publish
RUN dotnet publish 'CleanService/CleanService.csproj' -c Release -o /app/publish

# Statge 3: Run Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0
ENV ASPNETCORE_HTTP_PORTS=5011
EXPOSE 5011
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanService.dll"]

