FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HR/HR.API/HR.API.csproj", "HR/HR.API/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "HR/HR.API/HR.API.csproj"
COPY . .
WORKDIR "/src/HR/HR.API"
RUN dotnet build "HR.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HR.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HR.API.dll"]