FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App


# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out


# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/out .
COPY --from=build-env /App/out/runme.sh /
RUN mkdir -p /App/toscan
RUN mkdir -p /App/staging
RUN apt-get update && apt-get install -y unzip
RUN chmod a+x /runme.sh
ENTRYPOINT ["/runme.sh"]