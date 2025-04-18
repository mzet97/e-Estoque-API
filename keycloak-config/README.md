# Keycloak Configuration Project

This project contains the necessary files to configure a Keycloak realm and import it into a Keycloak server.

## Project Structure

- `realms/my-realm.json`: Contains the configuration for the Keycloak realm, including settings for users, roles, clients, and other realm-specific configurations.
  
- `scripts/import-realm.sh`: A script that imports the realm configuration into Keycloak using the Keycloak Admin CLI or REST API.

## Setup Instructions

1. **Prerequisites**
   - Ensure you have a running instance of Keycloak.
   - Install the Keycloak Admin CLI or ensure you can access the Keycloak REST API.

2. **Configure the Realm**
   - Edit the `realms/my-realm.json` file to customize your realm settings, including users, roles, and clients.

3. **Import the Realm**
   - Run the import script to create the realm in your Keycloak server:
     ```bash
     ./scripts/import-realm.sh
     ```

4. **Verify the Import**
   - Log in to the Keycloak Admin Console and verify that the realm has been created successfully.

## Additional Information

For more details on Keycloak configuration and management, refer to the [Keycloak Documentation](https://www.keycloak.org/documentation).