#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN groupadd -g 1000 processexpress  \
&& useradd -g 1000 -s /bin/bash processexpress 

RUN chown processexpress:processexpress /app
ENV TZ=America/Sao_Paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone 

RUN mkdir /arquivos

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ProcessExpress.csproj", ""]
RUN dotnet restore "./ProcessExpress.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ProcessExpress.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProcessExpress.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProcessExpress.dll"]