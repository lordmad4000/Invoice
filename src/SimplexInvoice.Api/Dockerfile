#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/SimplexInvoice.Api/SimplexInvoice.Api.csproj", "src/SimplexInvoice.Api/"]
COPY ["src/SimplexInvoice.Application/SimplexInvoice.Application.csproj", "src/SimplexInvoice.Application/"]
COPY ["src/SimplexInvoice.Domain/SimplexInvoice.Domain.csproj", "src/SimplexInvoice.Domain/"]
COPY ["src/SimplexInvoice.Infra/SimplexInvoice.Infra.csproj", "src/SimplexInvoice.Infra/"]
RUN dotnet restore "src/SimplexInvoice.Api/SimplexInvoice.Api.csproj"
COPY . .
WORKDIR "/src/src/SimplexInvoice.Api"
RUN dotnet build "SimplexInvoice.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimplexInvoice.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimplexInvoice.Api.dll"]
