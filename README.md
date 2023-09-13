# Simplex Invoice (WIP)

A basic Invoice App example in Angular 15 for the Frontend using the following:

    - HTTP Interceptor class.
    - Functional route guard.

A basic Invoice API example in .NET 7 for the Backend using the following:

    - DDD.
    - Repository pattern.
    - UnitOfWork pattern.
    - JWT Authentication.
    - Entity Framework Code First.
    - AutoMapper.
    - MemoryCache.
    - CQRS.
    - Mediator.
    - Serilog.

How to run in a Docker container
---

First you need [Docker](www.docker.com) and [Docker Compose](https://docs.docker.com/compose/) installed in your machine, then run this command:

    docker-compose up --build -d

Once the container is created to recreate de BBDD run this command:

    dotnet ef database update --context EfContext --startup-project src/SimplexInvoice.Api/SimplexInvoice.Api.csproj

Finally open the following url in you browser:

    http://localhost:5000

The default login is:

    Email: admin@gmail.com
    Password: admin1234
