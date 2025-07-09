# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
  
COPY *.sln .
COPY src/BankMore.Auth.Api/*.csproj ./src/BankMore.Auth.Api/
COPY src/BankMore.Auth.Application/*.csproj ./src/BankMore.Auth.Application/
COPY src/BankMore.Auth.Domain/*.csproj ./src/BankMore.Auth.Domain/
COPY src/BankMore.Auth.Infrastructure/*.csproj ./src/BankMore.Auth.Infrastructure/

RUN dotnet restore

 
COPY . .

WORKDIR /src/src/BankMore.Auth.Api

RUN dotnet publish -c Release -o /app --no-restore
 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "BankMore.Auth.Api.dll"]
