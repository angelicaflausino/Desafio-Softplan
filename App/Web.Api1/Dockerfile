FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Web.Api1/Web.Api1.csproj", "Web.Api1/"]
COPY ["App.Application/App.Application.csproj", "App.Application/"]
COPY ["App.Infra/App.Infra.csproj", "App.Infra/"]
RUN dotnet restore "Web.Api1/Web.Api1.csproj"
COPY . .
WORKDIR "/src/Web.Api1"
RUN dotnet build "Web.Api1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Api1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Api1.dll"]