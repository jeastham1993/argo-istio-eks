FROM mcr.microsoft.com/dotnet/sdk:8.0-preview-alpine AS build-env
WORKDIR /app
    
# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore
    
# Copy everything else and build
COPY ./ ./
RUN dotnet publish --runtime alpine-x64 -c Release --self-contained true -o out

# Build runtime image
FROM alpine:3.16

# Add some libs required by .NET runtime 
RUN apk add --no-cache libstdc++ libintl icu
    
EXPOSE 80
EXPOSE 443
    
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["./DotnetSQLServer.Blueprint", "--urls", "http://0.0.0.0:8080"]