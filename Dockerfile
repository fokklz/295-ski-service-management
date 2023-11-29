FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["./SkiServiceAPI/SkiServiceAPI.csproj", "./SkiServiceAPI/"]
COPY ["./SkiServiceModels/SkiServiceModels.csproj", "./SkiServiceModels/"]
RUN dotnet restore "./SkiServiceAPI/SkiServiceAPI.csproj"
RUN dotnet restore "./SkiServiceModels/SkiServiceModels.csproj"

COPY . .

WORKDIR "/src/SkiServiceAPI"
RUN dotnet build "SkiServiceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SkiServiceAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "SkiServiceAPI.dll"]
