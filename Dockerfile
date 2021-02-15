FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY arviews-service.API/bin/Release/net5.0/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "arviews-service.API.dll"]