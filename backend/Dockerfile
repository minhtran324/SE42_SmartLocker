FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY SLMS.sln .
COPY src/SLMS.Domain/SLMS.Domain.csproj src/SLMS.Domain/
COPY src/SLMS.Application/SLMS.Application.csproj src/SLMS.Application/
COPY src/SLMS.Infrastructure/SLMS.Infrastructure.csproj src/SLMS.Infrastructure/
COPY src/SLMS.Api/SLMS.Api.csproj src/SLMS.Api/
RUN dotnet restore src/SLMS.Api/SLMS.Api.csproj

COPY . .
RUN dotnet publish src/SLMS.Api/SLMS.Api.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "SLMS.Api.dll"]
