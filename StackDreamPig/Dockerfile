# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /StackDreamPig

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Application/*.csproj ./Application/
COPY Entities/*.csproj ./Entities/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY Presentation/*.csproj ./Presentation/
COPY SpdCommon/*.csproj ./SpdCommon/
COPY Tests/*.csproj ./Tests/
COPY Valueobject/*.csproj ./Valueobject/

RUN dotnet restore

# copy everything else and build app

COPY Valueobject/. ./Valueobject/
WORKDIR /StackDreamPig/Valueobject
RUN dotnet publish -c release -o /app

COPY Entities/. ../Entities/
WORKDIR /StackDreamPig/Entities
RUN dotnet publish -c release -o /app

COPY Infrastructure/. ../Infrastructure/
WORKDIR /StackDreamPig/Infrastructure
RUN dotnet publish -c release -o /app


COPY Application/. ../Application/
WORKDIR /StackDreamPig/Application
RUN dotnet publish -c release -o /app


COPY SpdCommon/. ../SpdCommon/
WORKDIR /StackDreamPig/SpdCommon
RUN dotnet publish -c release -o /app


COPY Presentation/. ../Presentation/
WORKDIR /StackDreamPig/Presentation
RUN dotnet publish -c release -o /app


COPY Tests/. ../Tests/
WORKDIR /StackDreamPig/Tests
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Presentation.dll"]
