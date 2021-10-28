FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.1-alpine3.10 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.101-alpine3.10 AS build
WORKDIR /src
COPY ["Utg.HR.Api/.", "Utg.HR.Api/"]
COPY ["Utg.HR.BL/.", "Utg.HR.BL/"]
COPY ["Utg.HR.Common/.", "Utg.HR.Common/"]
COPY ["Utg.HR.Dal/.", "Utg.HR.Dal/"]
RUN dotnet restore "Utg.HR.Api/Utg.HR.API.csproj"
COPY . .
WORKDIR "/src/Utg.HR.Api"
RUN dotnet build "Utg.HR.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Utg.HR.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Utg.HR.API.dll"]