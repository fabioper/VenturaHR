FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Webhooks/Webhooks/Webhooks.csproj", "Webhooks/"]
RUN dotnet restore "Services/Webhooks/Webhooks/Webhooks.csproj"
COPY . .
WORKDIR "/src/Webhooks"
RUN dotnet build "Webhooks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Webhooks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Webhooks.dll"]
