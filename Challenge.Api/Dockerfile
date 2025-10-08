FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Challenge.Api.csproj", "./"]
COPY ["../Challenge.Application/Challenge.Application.csproj", "../Challenge.Application/"]
RUN dotnet restore "Challenge.Api.csproj"

COPY . .
WORKDIR /src
RUN dotnet publish "Challenge.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS="http://+:80"
ENV DOTNET_RUNNING_IN_CONTAINER=true

ENTRYPOINT ["dotnet", "Challenge.Api.dll"]
