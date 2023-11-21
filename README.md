# Ski-Service-Management-Projekt

## Installation mit Docker

### Voraussetzungen
- Docker
- Docker Compose

### Repository klonen & Ausführen
```bash
git clone https://github.com/fokklz/295-ski-service-management
cd 295-ski-service-management
docker-compose up -d --build
```
Dieser Befehl setzt die erforderlichen Dienste auf, einschließlich der Datenbank und der API. <br>
Eine migration wird mithilfe des `migration` containers erstellt
   
[http://localhost:8000/swagger](http://localhost:8000/swagger) um die API zu öffnen

## Installation ohne Docker

### Voraussetzungen
- .NET SDK (Version 7.0 oder höher)
- SQL Server
- Entity Framework Core CLI

### Schritte
1. **Repository klonen**
    ```bash
    git clone https://github.com/fokklz/295-ski-service-management
    cd 295-ski-service-management
    ```
2. **Datenbank-Update durchführen**
    ```bash
    dotnet tool install --global dotnet-ef
    cd SkiServiceAPI
    dotnet ef database update --connection "Server=localhost;Database=ASPDatabaseLocal;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;"
    ```
    Stellen sie sicher das ihre Datenbank verbindungen über SQL Auth zulässt
3. **Erstellen eines neuen Nutzers in der Datenbank:**
    <br>Führen Sie das Skript `files/create_user.sql` manuell in Ihrem SQL Server aus, um einen neuen Nutzer zu erstellen.
4. **Anwendung starten:**
   ```
   dotnet restore
   dotnet run
   ```

## Wichtige Dateien und Verzeichnisse

- `README.md`: Enthält grundlegende Informationen und Anweisungen zum Projekt.
- `MSSQL/`: 
  - `Dockerfile`: Dockerfile für die SQL Server-Datenbank.
  - `entrypoint.sh`: Einstiegspunkt-Skript für den SQL Server-Container.
  - `init.sql`: SQL-Skript zur Initialisierung der Datenbank.
- `SkiServiceAPI/`: Hauptverzeichnis für die API.
  - `Dockerfile`: Dockerfile für die API.
  - `Dockerfile.Migrate`: Spezielles Dockerfile für Datenbankmigrationen.
  - `appsettings.json`: Konfigurationsdatei für die Produktionsumgebung.
  - `appsettings.Development.json`: Konfigurationsdatei für die Entwicklungsumgebung.
  - `entrypoint.sh`: Einstiegspunkt-Skript für den API-Container.
  - `Program.cs`: Haupt-Einstiegspunkt der API-Anwendung.
  - `wwwroot/`: Verzeichnis für statische Webdateien.
- `docker-compose.yml`: Docker Compose-Konfigurationsdatei zur Orchestrierung der Container.
- `files/`: Zusätzliche Dateien und Dokumentation.
  - `create_user.sql`: SQL-Skript zum Erstellen eines neuen Nutzers.
  - `Auftrag.pdf`, `Dokumentation.docx`: Dokumentation und Projektbeschreibungen.
  - `Bilder/`: Enthält Diagramme und Entwürfe.
  - `Berichte/`: Enthält Test-Berichte.
  - `SkiService-Management.postman_collection.json`: Postman Collection für API-Tests.
  - `Dokumentation.pdf`: Dokumentation
  - `Präsentation.pdf`: Präsentation
