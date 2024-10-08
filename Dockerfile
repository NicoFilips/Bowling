FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . . 

RUN dotnet restore ./Bowling.API/Bowling.API.csproj

RUN dotnet publish ./Bowling.API/Bowling.API.csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "Bowling.API.dll"]

EXPOSE 80