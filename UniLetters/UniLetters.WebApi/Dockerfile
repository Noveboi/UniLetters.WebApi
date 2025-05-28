FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UniLetters.WebApi/UniLetters.WebApi.csproj", "UniLetters.WebApi/"]
RUN dotnet restore "UniLetters.WebApi/UniLetters.WebApi.csproj"
COPY . .
WORKDIR "/src/UniLetters.WebApi"
RUN dotnet build "./UniLetters.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UniLetters.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniLetters.WebApi.dll"]
