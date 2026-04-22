FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

COPY Directory.Packages.props ./
COPY TodoApp.slnx ./
COPY NuGet.Config ./
COPY src/TodoApp.Domain/TodoApp.Domain.csproj              src/TodoApp.Domain/
COPY src/TodoApp.Application/TodoApp.Application.csproj    src/TodoApp.Application/
COPY src/TodoApp.Infrastructure/TodoApp.Infrastructure.csproj src/TodoApp.Infrastructure/
COPY src/TodoApp.Web/TodoApp.Web.csproj                    src/TodoApp.Web/

RUN dotnet restore src/TodoApp.Web/TodoApp.Web.csproj

COPY src/ src/

RUN dotnet publish src/TodoApp.Web/TodoApp.Web.csproj \
    -c Release \
    -o /app/publish

RUN dotnet ef migrations bundle \
    --project src/TodoApp.Infrastructure/TodoApp.Infrastructure.csproj \
    --startup-project src/TodoApp.Web/TodoApp.Web.csproj \
    --self-contained \
    -r linux-x64 \
    -o /app/publish/EfCoreMigrationsBundle

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

COPY scripts/apply-migrations.sh ./apply-migrations.sh

RUN chmod +x ./apply-migrations.sh ./EfCoreMigrationsBundle

EXPOSE 8080

ENTRYPOINT ["/bin/sh", "./apply-migrations.sh"]
