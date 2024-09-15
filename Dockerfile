FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.sln .
COPY Gyneco.Api/*.csproj ./Gyneco.Api/
COPY Gyneco.Application/*.csproj ./Gyneco.Application/
COPY Gyneco.Domain/*.csproj ./Gyneco.Domain/
COPY Gyneco.Infrastructure/*.csproj ./Gyneco.Infrastructure/
COPY Gyneco.Persistence/*.csproj ./Gyneco.Persistence/
RUN dotnet restore

COPY Gyneco.Api/*.csproj ./Gyneco.Api/
COPY Gyneco.Application/*.csproj ./Gyneco.Application/
COPY Gyneco.Domain/*.csproj ./Gyneco.Domain/
COPY Gyneco.Infrastructure/*.csproj ./Gyneco.Infrastructure/
COPY Gyneco.Persistence/*.csproj ./Gyneco.Persistence/
WORKDIR /source/Gyneco.Api
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Gyneco.Api.dll"]
