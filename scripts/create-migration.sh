#!/bin/bash
set -e

MIGRATION_NAME=$1

if [ -z "$MIGRATION_NAME" ]; then
  echo "Usage: $0 <migration_name>"
  exit 1
fi

echo "Creating migration: $MIGRATION_NAME"

dotnet ef migrations add "$MIGRATION_NAME" --project src/TodoApp.Infrastructure --startup-project src/TodoApp.Web

echo "Migration $MIGRATION_NAME created successfully."
