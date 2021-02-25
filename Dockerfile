FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY MinitwitApi/* app 
EXPOSE 5000 5001
CMD dotnet run app/Server
