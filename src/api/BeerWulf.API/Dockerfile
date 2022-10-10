#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EverybodyCodes.API/EverybodyCodes.API.csproj", "EverybodyCodes.API/"]
RUN dotnet restore "EverybodyCodes.API/EverybodyCodes.API.csproj"
COPY . .
WORKDIR "/src/EverybodyCodes.API"
RUN dotnet build "EverybodyCodes.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EverybodyCodes.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EverybodyCodes.API.dll"]