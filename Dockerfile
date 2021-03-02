FROM mcr.microsoft.com/dotnet/sdk:5.0
ARG BUILD_CONFIGURATION=Debug
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:443
ENV ASPNETCORE_URLS=https://+:80
RUN dotnet dev-certs https --trust
COPY ["./MiniTwitApi", "."]
EXPOSE 443
EXPOSE 80
CMD dotnet run --project ./Server
