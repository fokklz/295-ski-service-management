cd SkiServiceAPI
dotnet ef database update --connection "Server=localhost;Database=ASPDatabase;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;"
Read-Host