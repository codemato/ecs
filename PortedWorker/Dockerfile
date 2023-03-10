FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
# Copy everything
COPY *.sln .
COPY FileRead/*.csproj ./FileRead/
COPY FileRead/*.config ./FileRead/
# Restore as distinct layers
RUN dotnet restore
# copy everything else and build app
COPY FileRead/. ./FileRead/
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build /App/out .

ENTRYPOINT ["FileRead.exe"]