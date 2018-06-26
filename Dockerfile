FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Wiki.Server/Wiki.Server.csproj Wiki.Server/
COPY Wiki.Shared/Wiki.Shared.csproj Wiki.Shared/
COPY Wiki.Client/Wiki.Client.csproj Wiki.Client/
RUN dotnet restore Wiki.Server/Wiki.Server.csproj
COPY . .
WORKDIR /src/Wiki.Server
RUN dotnet build Wiki.Server.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Wiki.Server.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Wiki.Server.dll"]
