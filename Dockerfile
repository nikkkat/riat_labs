# ���������� ������� ����� � .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# ������������� ������� ����������
WORKDIR /src

# �������� csproj ���� � ��������������� �����������
COPY ["AuthServiceMicroservice.csproj", "./"]
RUN dotnet restore "AuthServiceMicroservice.csproj"

# �������� ��� ��������� �����
COPY . .

# ��������� ������
RUN dotnet publish "AuthServiceMicroservice.csproj" -c Release -o /app

# ���������� ����� � .NET Runtime ��� ���������� ����������
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AuthService.dll"]
