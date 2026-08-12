FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ConnectFour.csproj .
RUN dotnet restore ConnectFour.csproj
COPY . .
RUN dotnet publish ConnectFour.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
EXPOSE 8080
CMD ["sh", "-c", "ASPNETCORE_URLS=http://+:$PORT dotnet ConnectFour.dll"]
