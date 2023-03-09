FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY FileRead/*.csproj ./FileRead/
COPY FileRead/*.config ./FileRead/
RUN nuget restore

# copy everything else and build app
COPY FileRead/. ./FileRead/
WORKDIR /app/FileRead
RUN msbuild  /t:publish /p:OutDir="/app/FileRead/build" /p:Configuration=Release /p:DeployOnBuild=true


# copy build artifacts into runtime image
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8 AS runtime
WORKDIR /app/
COPY --from=build /app/FileRead/build/. ./
ENTRYPOINT ["FileRead.exe"]