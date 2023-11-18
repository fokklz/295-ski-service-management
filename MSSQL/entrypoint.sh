#!/bin/bash

# to not create the user and database if they already exist
INIT_FLAG="/var/opt/mssql/init_done.flag"

is_sql_server_ready() {
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "AStrong!Passw0rd" -Q "SELECT 1" &> /dev/null
    return $?
}

/opt/mssql/bin/sqlservr &

if [ ! -f "$INIT_FLAG" ]; then
    for i in {1..30}; do
        if is_sql_server_ready; then
            echo "MSSQL is ready!"
            break
        else
            countdown=$((30 - $i))
            echo "Waiting for MSSQL... (${countdown})"
            sleep 1s
        fi
    done

    echo "Running initialization script..."

    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "AStrong!Passw0rd" -d master -i /usr/src/app/init.sql

    touch "$INIT_FLAG"
fi

tail -f /dev/null
