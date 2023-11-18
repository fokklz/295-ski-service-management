FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SkiServiceAPI.csproj", ""]
RUN dotnet restore "./SkiServiceAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SkiServiceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SkiServiceAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
COPY ./entrypoint.sh .

ENTRYPOINT ["./entrypoint.sh"]
