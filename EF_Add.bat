@echo off

if "%~1"=="" goto blank
dotnet ef migrations add %1 --context EfContext -p src/SimplexInvoice.Infra -s src\SimplexInvoice.Api\SimplexInvoice.Api.csproj

:blank