FROM mcr.microsoft.com/dotnet/sdk:5.0
ARG BUILD_CONFIGURATION=Debug
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:5001
ENV ASPNETCORE_URLS=https://+:5000
RUN dotnet dev-certs https --trust
COPY ["./MiniTwitApi", "."]
EXPOSE 5000
EXPOSE 5001
CMD dotnet run --project ./Server
