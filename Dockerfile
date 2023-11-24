FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
RUN apt-get update \
    && apt-get install -y curl \
    && apt-get install -y wget \
    && rm -rf /var/lib/apt/lists/*
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Project 19.8/Project 19.8.csproj", "Project 19.8/"]
RUN dotnet restore "Project 19.8/Project 19.8.csproj"
COPY . .
WORKDIR "/src/Project 19.8"
RUN dotnet build "Project 19.8.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project 19.8.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project 19.8.dll"]