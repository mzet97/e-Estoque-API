#!/bin/bash

# Check if the required environment variables are set
if [ -z "$KEYCLOAK_URL" ] || [ -z "$KEYCLOAK_ADMIN" ] || [ -z "$KEYCLOAK_ADMIN_PASSWORD" ]; then
  echo "Please set the KEYCLOAK_URL, KEYCLOAK_ADMIN, and KEYCLOAK_ADMIN_PASSWORD environment variables."
  exit 1
fi

# Import the realm configuration
curl -X POST "$KEYCLOAK_URL/admin/realms" \
     -H "Content-Type: application/json" \
     -u "$KEYCLOAK_ADMIN:$KEYCLOAK_ADMIN_PASSWORD" \
     -d @realms/my-realm.json

if [ $? -eq 0 ]; then
  echo "Realm imported successfully."
else
  echo "Failed to import realm."
  exit 1
fi