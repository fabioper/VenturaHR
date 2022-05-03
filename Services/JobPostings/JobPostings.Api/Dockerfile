FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JobPostings.Api/JobPostings.Api.csproj", "JobPostings.Api/"]
RUN dotnet restore "JobPostings.Api/JobPostings.Api.csproj"
COPY . .
WORKDIR "/src/JobPostings.Api"
RUN dotnet build "JobPostings.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobPostings.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobPostings.Api.dll"]
