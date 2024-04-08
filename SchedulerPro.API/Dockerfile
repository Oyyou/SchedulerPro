#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV TZ=Europe/London
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["SchedulerPro.DAL/SchedulerPro.DAL.csproj", "SchedulerPro.DAL/"]
RUN dotnet restore "SchedulerPro.DAL/SchedulerPro.DAL.csproj"

COPY ["SchedulerPro.API/SchedulerPro.API.csproj", "SchedulerPro.API/"]
RUN dotnet restore "SchedulerPro.API/SchedulerPro.API.csproj"

COPY . .

WORKDIR "/src/SchedulerPro.API"
RUN dotnet build "SchedulerPro.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SchedulerPro.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "SchedulerPro.API.dll"]