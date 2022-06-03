FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY ["src/Users.Infra/Users.Infra.csproj","Users.Infra/"]
COPY ["src/Users.CrossCutting/Users.CrossCutting.csproj","Users.CrossCutting/"]
COPY ["src/Users.Application/Users.Application.csproj","Users.Application/"]
COPY ["src/Users.Domain/Users.Domain.csproj","Users.Domain/"]
COPY ["src/Users.API/Users.API.csproj", "Users.API/"]
COPY src/. .

RUN dotnet restore "Users.API/Users.API.csproj"


RUN dotnet build "Users.API/Users.API.csproj" -c Debug -o /app

FROM build AS publish
RUN dotnet publish "Users.API/Users.API.csproj" -c Debug -o /app

FROM base AS final
WORKDIR /app

COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Users.API.dll"]