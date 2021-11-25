#FROM mcr.microsoft.com/dotnet/core/sdk:3.1
#
#WORKDIR /app
## Install dependencies
#RUN apt-get update -qq && apt-get -y install libgdiplus
## Copy csproj and restore as distinct layers
#COPY *.csproj ./
#RUN dotnet restore
#
## Copy everything else and build
#COPY . ./
#RUN dotnet build -c Release -o out
#RUN dotnet publish -c Release -o out
#base runtime image (Debian 10 w/ .NET runtime)
FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base

WORKDIR /app
# install necessary packages

RUN apt update \
    && apt install -y sudo libc6 libc6-dev libgtk2.0-0 libnss3 libatk-bridge2.0-0 libx11-xcb1 libxcb-dri3-0 libdrm-common libgbm1 libasound2 libappindicator3-1 libxrender1 libfontconfig1 xvfb

# update write permissions
RUN chmod 777 .

# base development image (Debian 10 w/ .NET SDK)
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src

# restore NuGet packages
COPY ["FinancialSummaryApi/FinancialSummaryApi.csproj", "FinancialSummaryApi/"]
RUN dotnet restore "FinancialSummaryApi/FinancialSummaryApi.csproj"

# build project
COPY . .
WORKDIR "/src/FinancialSummaryApi"
RUN dotnet build "FinancialSummaryApi.csproj" -c Release -o /app/build

# publish project
FROM build AS publish
RUN dotnet publish "FinancialSummaryApi.csproj" -c Release -o /app/publish

# run app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinancialSummaryApi.dll"]

EXPOSE ${PORT:-3000}
CMD ASPNETCORE_URLS=http://+:${PORT:-3000} dotnet ./out/FinancialSummaryApi.dll
