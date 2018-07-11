FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 31670
EXPOSE 44321

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY devhub/devhub.csproj devhub/
RUN dotnet restore devhub/devhub.csproj
COPY . .
WORKDIR /src/devhub
RUN dotnet build devhub.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish devhub.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "devhub.dll"]
