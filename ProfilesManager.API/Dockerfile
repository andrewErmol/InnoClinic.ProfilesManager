#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProfilesManager.API/ProfilesManager.API/ProfilesManager.API.csproj", "ProfilesManager.API/ProfilesManager.API/"]
COPY ["ProfilesManager.API/ProfilesManager.Service/ProfilesManager.Services.csproj", "ProfilesManager.API/ProfilesManager.Service/"]
COPY ["ProfilesManager.API/ProfilesManager.Contracts/ProfilesManager.Contracts.csproj", "ProfilesManager.API/ProfilesManager.Contracts/"]
COPY ["ProfilesManager.API/ProfilesManager.Domain/ProfilesManager.Domain.csproj", "ProfilesManager.API/ProfilesManager.Domain/"]
COPY ["ProfilesManager.API/ProfilesManager.Persistence/ProfilesManager.Persistence.csproj", "ProfilesManager.API/ProfilesManager.Persistence/"]
COPY ["ProfilesManager.API/ProfilesManager.Services.Abstraction/ProfilesManager.Services.Abstraction.csproj", "ProfilesManager.API/ProfilesManager.Services.Abstraction/"]
COPY ["ProfilesManager.API/ProfilesManager.Presentation/ProfilesManager.Presentation.csproj", "ProfilesManager.API/ProfilesManager.Presentation/"]
COPY ["ProfilesManager.API/ModelsToRequest/ModelsToRequest.csproj", "ProfilesManager.API/ModelsToRequest/"]
COPY ["ProfilesManager.API/ProfilesManager.Messaging/ProfilesManager.Messaging.csproj", "ProfilesManager.API/ProfilesManager.Messaging/"]
RUN dotnet restore "ProfilesManager.API/ProfilesManager.API/ProfilesManager.API.csproj"
COPY . .
WORKDIR "/src/ProfilesManager.API/ProfilesManager.API"
RUN dotnet build "ProfilesManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfilesManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProfilesManager.API.dll"]