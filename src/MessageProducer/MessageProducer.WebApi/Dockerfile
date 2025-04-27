FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MessageProducer/MessageProducer.WebApi/MessageProducer.WebApi.csproj", "src/MessageProducer/MessageProducer.WebApi/"]
COPY ["src/MessageProducer/MessageProducer.Infrastructure/MessageProducer.Infrastructure.csproj", "src/MessageProducer/MessageProducer.Infrastructure/"]
COPY ["src/MessageProducer/MessageProducer.Domain/MessageProducer.Domain.csproj", "src/MessageProducer/MessageProducer.Domain/"]
RUN dotnet restore "src/MessageProducer/MessageProducer.WebApi/MessageProducer.WebApi.csproj"
COPY . .
WORKDIR "/src/src/MessageProducer/MessageProducer.WebApi"
RUN dotnet build "./MessageProducer.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MessageProducer.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageProducer.WebApi.dll"]
