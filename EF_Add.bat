@echo off

if "%~1"=="" goto blank
dotnet ef migrations add %1 --context EfContext -p src/Invoice.Infra -s src\Invoice.Api\Invoice.Api.csproj

:blank