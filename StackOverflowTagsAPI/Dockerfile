FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StackOverflowTagsAPI/StackOverflowTags.API.csproj", "StackOverflowTagsAPI/"]
RUN dotnet restore "./StackOverflowTagsAPI/StackOverflowTags.API.csproj"
COPY . .
WORKDIR "/src/StackOverflowTagsAPI"
RUN dotnet build "./StackOverflowTags.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StackOverflowTags.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StackOverflowTags.API.dll"]