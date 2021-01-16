#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ProcessExpress/ProcessExpress.csproj", "ProcessExpress/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Languages/Languages.csproj", "Languages/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infraestrutura/Infraestrutura.csproj", "Infraestrutura/"]
COPY ["OFX/OFX.csproj", "OFX/"]
RUN dotnet restore "ProcessExpress/ProcessExpress.csproj"
COPY . .
WORKDIR "/src/ProcessExpress"
RUN dotnet build "ProcessExpress.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProcessExpress.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProcessExpress.dll"]