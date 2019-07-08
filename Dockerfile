# Build ASP.net core web app
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
Copy *.sln ./
COPY ["SWTlib/SWTlib.csproj", "SWTlib/"]
COPY ["SWTlib/LibraryContext.csproj", "LibraryContext/"]
RUN dotnet restore
COPY . .
WORKDIR "/src/SWTlib"
RUN dotnet build "SWTlib.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SWTlib.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SWTlib.dll"]
