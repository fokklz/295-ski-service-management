#!/bin/bash

# always run Update-Database on startup to ensure the database is up to date
if [ -f /tmp/migration_done ]; then
    rm -f /tmp/migration_done
fi

# Run the migration command
dotnet ef database update --connection "Server=mssql,1433;Database=JetStream;User ID=SA;Password=AStrong!Passw0rd;TrustServerCertificate=True;"

# Check if the migration was successful
if [ $? -eq 0 ]; then
    # Indicate that migration is done
    touch /tmp/migration_done
else
    echo "Migration failed"
    exit 1
fi