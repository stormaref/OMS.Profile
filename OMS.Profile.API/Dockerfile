FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OMS.Profile.API/OMS.Profile.API.csproj", "OMS.Profile.API/"]
RUN dotnet restore "OMS.Profile.API/OMS.Profile.API.csproj"
COPY . .
WORKDIR "/src/OMS.Profile.API"
RUN dotnet build "OMS.Profile.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OMS.Profile.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OMS.Profile.API.dll"]
