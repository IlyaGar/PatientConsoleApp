# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory for the build
WORKDIR /app

# Copy the csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy all other files and build the application
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official .NET Runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Copy the built application from the previous step
WORKDIR /app
COPY --from=build /app/out ./

# Set the command that will be launched when the container starts
ENTRYPOINT ["dotnet", "YourConsoleApp.dll"]