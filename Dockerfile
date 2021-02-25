FROM mcr.microsoft.com/dotnet/aspnet:5.0

RUN apt-get -y install git
RUN git clone https://github.com/jokk-itu/PythonKindergarten.git
RUN cd PythonKindergarten/MiniTwitApi/Server 
EXPOSE 5000 5001
CMD dotnet run
