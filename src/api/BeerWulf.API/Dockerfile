#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WhatDoesTheWulfSay.API/WhatDoesTheWulfSay.API.csproj", "WhatDoesTheWulfSay.API/"]
RUN dotnet restore "WhatDoesTheWulfSay.API/WhatDoesTheWulfSay.API.csproj"
COPY . .
WORKDIR "/src/WhatDoesTheWulfSay.API"
RUN dotnet build "WhatDoesTheWulfSay.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhatDoesTheWulfSay.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WhatDoesTheWulfSay.API.dll"]