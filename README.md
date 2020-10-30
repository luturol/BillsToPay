# BIlls to Pay

It's a project that you send a bill with due date, pay date and the original value and it will save and return with the correct fine and interest.

The project is made using .Net Core 3.1 and PostgreSQL.

## How to run

You can run using:

```  docker
docker-compose up
```

It will build the project and publish to port 8080. To acess, just go to your browser and run http://localhost:8080/swagger

## Running without Docker

First, you need to have .Net Core 3.1 intalled and PostgreSQL.

After installed PostgreSQL go to [appsettings.json](./BillsToPay/appsettings.json) and change the ConnectionString to use your server (to localhost), user, password, database and port (default port is 5432) from your Postgres instance.

Now you need to compile the following script inside your postgres database. [RUN THIS SCRIPT](./Script/init.sql).

Now, enter  ./BillsToPay and run:

```
dotnet run
```

It will build to the default .Net Core port.