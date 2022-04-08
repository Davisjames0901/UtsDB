FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Utsd.Console/Utsd.Console.csproj", "Utsd.Console/"]
RUN dotnet restore "Utsd.Console/Utsd.Console.csproj"
COPY . .
WORKDIR "/src/Utsd.Console"
RUN dotnet build "Utsd.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Utsd.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Utsd.Console.dll"]
