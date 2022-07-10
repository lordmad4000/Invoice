FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
RUN apt update
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash -
RUN apt install -y build-essential nodejs

WORKDIR /src/Invoice.Api/ClientApp
RUN npm install -g npm@8.12.2
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