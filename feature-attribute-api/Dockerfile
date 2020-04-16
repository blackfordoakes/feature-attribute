#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["feature-attribute-api.csproj", ""]
RUN dotnet restore "./feature-attribute-api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "feature-attribute-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "feature-attribute-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "feature-attribute-api.dll"]