#!/bin/sh

# Check if APP_URL environment variable is set
if [ -z "$APP_URL" ]; then
    echo "ERROR: APP_URL environment variable is not set"
    echo "Please set APP_URL to your application URL (e.g., https://your-app.choreoapps.dev)"
    exit 1
fi

# Check if MONGODB_URI environment variable is set
if [ -z "$MONGODB_URI" ]; then
    echo "ERROR: MONGODB_URI environment variable is not set"
    echo "Please set MONGODB_URI to your MongoDB connection string"
    exit 1
fi

echo "Configuring application with APP_URL: $APP_URL"
echo "Configuring MongoDB connection..."

# Escape special characters in MongoDB URI for sed
MONGODB_URI_ESCAPED=$(echo "$MONGODB_URI" | sed 's/[&/\]/\\&/g')

# Replace placeholders in appsettings.Production.json
if [ -f "/app/appsettings.Production.json" ]; then
    echo "Updating /app/appsettings.Production.json..."
    sed -i "s|{{APP_URL}}|$APP_URL|g" /app/appsettings.Production.json
    sed -i "s|{{MONGODB_URI}}|$MONGODB_URI_ESCAPED|g" /app/appsettings.Production.json
fi

# Replace placeholders in Blazor wwwroot appsettings files
if [ -f "/app/wwwroot/appsettings.Production.json" ]; then
    echo "Updating /app/wwwroot/appsettings.Production.json..."
    sed -i "s|{{APP_URL}}|$APP_URL|g" /app/wwwroot/appsettings.Production.json
fi

if [ -f "/app/wwwroot/appsettings.json" ]; then
    echo "Updating /app/wwwroot/appsettings.json..."
    sed -i "s|{{APP_URL}}|$APP_URL|g" /app/wwwroot/appsettings.json
fi

echo "Configuration complete. Starting application..."

# Start the application
exec dotnet ChoreoSample.Host.dll
