# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files
COPY ["ChoreoSample.Host/ChoreoSample.Host.csproj", "ChoreoSample.Host/"]
COPY ["ChoreoSample.Contracts/ChoreoSample.Contracts.csproj", "ChoreoSample.Contracts/"]
COPY ["ChoreoSample.Blazor/ChoreoSample.Blazor.csproj", "ChoreoSample.Blazor/"]
COPY ["common.props", "./"]
COPY ["NuGet.Config", "./"]

# Install Node.js and npm (required for abp install-libs)
RUN apt-get update && \
    apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    npm --version && node --version && \
    rm -rf /var/lib/apt/lists/*

# Restore dependencies
RUN dotnet restore "ChoreoSample.Host/ChoreoSample.Host.csproj"

# Copy all source code
COPY . .

# Build and publish
WORKDIR "/src/ChoreoSample.Host"
RUN dotnet tool install -g Volo.Abp.Cli || dotnet tool update -g Volo.Abp.Cli
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN abp install-libs
RUN dotnet build "ChoreoSample.Host.csproj" -c Release -o /app/build
RUN dotnet publish "ChoreoSample.Host.csproj" -c Release -o /app/publish

# Generate OpenIddict certificate at build time
RUN dotnet dev-certs https -ep /app/publish/openiddict.pfx -p b97118c7-a13b-45d6-b079-d83557af6a89 --trust

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Install brotli for recompressing appsettings files
RUN apt-get update && \
    apt-get install -y brotli && \
    rm -rf /var/lib/apt/lists/*

# Create a non-root user for security
RUN groupadd -g 10014 choreo && \
    useradd --no-create-home --uid 10014 --gid 10014 choreouser

# Copy published files including the certificate
COPY --from=build /app/publish .

# Copy the entrypoint script
COPY ChoreoSample.Host/entrypoint.sh /app/entrypoint.sh

# Set correct ownership and permissions for all files including the certificate
RUN chown -R 10014:10014 /app && \
    chmod -R 755 /app && \
    chmod 644 /app/openiddict.pfx && \
    chmod +x /app/entrypoint.sh

# Expose ports (8080 for HTTP, Choreo typically uses 8080)
EXPOSE 8080

# Set user to non-root
USER 10014

# Set environment variables for production
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

# Health check - just check if the port is responding
HEALTHCHECK --interval=30s --timeout=10s --retries=3 \
  CMD wget --no-verbose --tries=1 --spider http://localhost:8080/ || exit 1

ENTRYPOINT ["/app/entrypoint.sh"]
