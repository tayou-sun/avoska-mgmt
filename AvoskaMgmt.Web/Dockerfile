# syntax=docker/dockerfile:1
  FROM mcr.microsoft.com/dotnet/aspnet:5.0
  COPY bin/Release/net5.0/ App/
  WORKDIR /App
  ENTRYPOINT ["dotnet", "AvoskaMgmt.Web.dll"]