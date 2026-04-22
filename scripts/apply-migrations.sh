set -e

./EfCoreMigrationsBundle --connection "$ConnectionStrings__DefaultConnection"

dotnet TodoApp.Web.dll
