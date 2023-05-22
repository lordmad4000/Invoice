FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
RUN apt update && apt install -y procps
RUN curl -sL https://deb.nodesource.com/setup_18.x | bash -
RUN apt install -y build-essential nodejs

WORKDIR /src/Invoice.Api/ClientApp
RUN npm install -g npm@8.19.3
RUN npm install zone.js

WORKDIR /src
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Invoice.Api.dll"]