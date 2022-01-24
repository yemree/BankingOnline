#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mrc.microsoft/dotnet:3.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mrc.microsoft/dotnet:3.1-aspnetcore-runtime AS build
WORKDIR /src
COPY ["Banking.Api/Banking.Api.csproj", "Banking.Api/"]
COPY ["Banking.Data/Banking.Data.csproj", "Banking.Data/"]
COPY ["Banking.Infrastructure/Banking.Infrastructure.csproj", "Banking.Infrastructure/"]
COPY ["Banking.Manager/Banking.Manager.csproj", "Banking.Manager/"]
RUN dotnet restore "Banking.API/Banking.API.csproj"
COPY . .
WORKDIR "/src/Banking.Api"
RUN dotnet build "Banking.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Banking.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Banking.Api.dll"]