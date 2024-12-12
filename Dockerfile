# Используем базовый образ с .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Устанавливаем рабочую директорию
WORKDIR /src

# Копируем csproj файл и восстанавливаем зависимости
COPY ["AuthServiceMicroservice.csproj", "./"]
RUN dotnet restore "AuthServiceMicroservice.csproj"

# Копируем все остальные файлы
COPY . .

# Публикуем проект
RUN dotnet publish "AuthServiceMicroservice.csproj" -c Release -o /app

# Используем образ с .NET Runtime для финального контейнера
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AuthService.dll"]
