FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Web.Api2/Web.Api2.csproj", "Web.Api2/"]
COPY ["App.Infra/App.Infra.csproj", "App.Infra/"]
COPY ["App.Application/App.Application.csproj", "App.Application/"]
RUN dotnet restore "Web.Api2/Web.Api2.csproj"
COPY . .
WORKDIR "/src/Web.Api2"
RUN dotnet build "Web.Api2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Api2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Api2.dll"]