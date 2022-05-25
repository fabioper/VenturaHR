FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateway/Web.Bff/Web.Bff.csproj", "Web.Bff/"]
RUN dotnet restore "ApiGateway/Web.Bff/Web.Bff.csproj"
COPY . .
WORKDIR "/src/Web.Bff"
RUN dotnet build "Web.Bff.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Bff.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Bff.dll"]
