FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MessageConsumer/MessageConsumer.WebApi/MessageConsumer.WebApi.csproj", "src/MessageConsumer/MessageConsumer.WebApi/"]
COPY ["src/MessageConsumer/MessageConsumer.Infrastructure/MessageConsumer.Infrastructure.csproj", "src/MessageConsumer/MessageConsumer.Infrastructure/"]
COPY ["src/MessageConsumer/MessageConsumer.Domain/MessageConsumer.Domain.csproj", "src/MessageConsumer/MessageConsumer.Domain/"]
RUN dotnet restore "src/MessageConsumer/MessageConsumer.WebApi/MessageConsumer.WebApi.csproj"
COPY . .
WORKDIR "/src/src/MessageConsumer/MessageConsumer.WebApi"
RUN dotnet build "./MessageConsumer.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MessageConsumer.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageConsumer.WebApi.dll"]
