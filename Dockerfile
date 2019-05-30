# Build ASP.net core web app
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS stage1

WORKDIR /app
COPY SWTlib ./
RUN dotnet publish -c Release -o /dist

# Start service
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

WORKDIR /deploy
COPY --from=stage1 /dist /deploy

EXPOSE 5000
ENTRYPOINT ["dotnet"]
CMD ["SWTlib.dll"]
