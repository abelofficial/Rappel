FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["src/Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "Domain/Domain.csproj"
COPY . .
WORKDIR "/src/Domain"
RUN dotnet build "Domain.csproj" -c Release -o /app/build

COPY ["src/Application/Application.csproj", "Application/"]
RUN dotnet restore "Application/Application.csproj"
COPY . .
WORKDIR "/src/Application"
RUN dotnet build "Application.csproj" -c Release -o /app/build

COPY ["src/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Infrastructure/Infrastructure.csproj"
COPY . .
WORKDIR "/src/Infrastructure"
RUN dotnet build "Infrastructure.csproj" -c Release -o /app/build

COPY ["src/RestApi/RestApi.csproj", "RestApi/"]
RUN dotnet restore "RestApi/RestApi.csproj"
COPY . .
WORKDIR "/src/RestApi"
RUN dotnet build "RestApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet RestApi.dll