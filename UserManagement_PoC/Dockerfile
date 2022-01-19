FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["UserManagement_PoC/UserManagement_PoC.csproj", "UserManagement_PoC/"]
RUN dotnet restore "UserManagement_PoC/UserManagement_PoC.csproj"
COPY . .
WORKDIR "/src/UserManagement_PoC"
RUN dotnet build "UserManagement_PoC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement_PoC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement_PoC.dll"]
