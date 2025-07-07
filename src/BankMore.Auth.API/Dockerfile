FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/BankMore.Auth.API/BankMore.Auth.API.csproj ./BankMore.Auth.API/
COPY src/BankMore.Auth.Application/BankMore.Auth.Application.csproj ./BankMore.Auth.Application/
COPY src/BankMore.Auth.Domain/BankMore.Auth.Domain.csproj ./BankMore.Auth.Domain/
COPY src/BankMore.Auth.Infrastructure/BankMore.Auth.Infrastructure.csproj ./BankMore.Auth.Infrastructure/

RUN dotnet restore ./BankMore.Auth.API/BankMore.Auth.API.csproj

COPY src/. .

WORKDIR /src/BankMore.Auth.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# 👇 Adicione esta linha
ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BankMore.Auth.API.dll"]
