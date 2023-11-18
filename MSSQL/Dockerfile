FROM mcr.microsoft.com/mssql/server:2019-latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=AStrong!Passw0rd
ENV MSSQL_PID=developer

WORKDIR /usr/src/app

COPY init.sql .
COPY entrypoint.sh .

ENTRYPOINT ["./entrypoint.sh"]
