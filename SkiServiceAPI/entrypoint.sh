#!/bin/bash

while [ ! -f /tmp/migration_done ]; do
  echo "Waiting for migration to complete..."
  sleep 5
done

echo "Update complete, starting API"

dotnet SkiServiceAPI.dll