FROM microsoft/dotnet:2.0-runtime-nanoserver-1709 AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY ActiveMQPublish/ActiveMQPublish.csproj ActiveMQPublish/
RUN dotnet restore ActiveMQPublish/ActiveMQPublish.csproj
COPY . .
WORKDIR /src/ActiveMQPublish
RUN dotnet build ActiveMQPublish.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ActiveMQPublish.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ActiveMQPublish.dll"]
