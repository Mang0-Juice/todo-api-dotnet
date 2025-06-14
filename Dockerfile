# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.sln .
COPY ToDoList.API/*.csproj ./ToDoList.API/
RUN dotnet restore

COPY . .
WORKDIR /app/ToDoList.API
RUN dotnet publish -c Release -o out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/ToDoList.API/out .

# Variável PORT do Railway
ENV ASPNETCORE_URLS=http://+:${PORT}

EXPOSE 5000

ENTRYPOINT ["dotnet", "ToDoList.API.dll"]

