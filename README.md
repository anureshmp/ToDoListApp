# ToDoListApp

A simple to-do list app: an ASP.NET Core Web API backend (`ToDoList.API`) and an Angular frontend (`ToDoList.client`).

## Prerequisites

- **.NET 10 SDK** — the API targets `net10.0`, which is a preview/RC-era TFM. Run `dotnet --list-sdks` and confirm a `10.0.x` SDK is present; the standard .NET 8/9 SDK will not build this project.
- **Node.js** compatible with Angular CLI 22 (Node 20.19+ or 22.12+) and npm 11.
- No database — data is stored in-memory (`ConcurrentDictionary` in `ToDoService`).

## Running the API

```bash
cd ToDoList.API
dotnet run
```

If your machine has not trusted the local ASP.NET Core dev certificate yet, run:

```bash
dotnet dev-certs https --trust
```

## Running the client

```bash
cd ToDoList.client
npm start
```

## Running tests

```bash
dotnet test ToDoListApp.slnx
```

runs the API unit tests (`ToDoListApi.Tests`). 