#!/bin/bash

echo "Checking if Docker is running..."
if ! docker info > /dev/null 2>&1; then
  echo "Docker is not running. Please start Docker Desktop."
  exit 1
fi

echo "Ensuring previous MSSQL container is stopped and removed (if it exists)..."
docker stop mssql-server-instance > /dev/null 2>&1
docker rm mssql-server-instance > /dev/null 2>&1

echo "Pulling database image (if not already present)..."
docker pull mcr.microsoft.com/mssql/server:2022-latest

echo "Starting the MSSQL Server container..."
docker run -d --name mssql-server-instance -p 1433:1433 \
    -e 'ACCEPT_EULA=Y' \
    -e 'SA_PASSWORD=!Bernibarni890' \
    -v $HOME/docker/mssql-data:/var/opt/mssql/data \
    mcr.microsoft.com/mssql/server:2022-latest

echo "Waiting for database to start (wait 30 seconds)..."
sleep 20

echo "Running database migrations for your .NET application..."
dotnet ef database update
sleep 20

echo "Starting the .NET application..."
dotnet run --project FRIchat.csproj

echo "Application should be accessible."
echo "To stop the MSSQL container, run: docker stop mssql-server-instance"
echo "To remove the MSSQL container, run: docker rm mssql-server-instance"