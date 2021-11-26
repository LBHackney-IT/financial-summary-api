FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app
RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev
EXPOSE ${PORT:-3000}

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["FinancialSummaryApi", "/src/FinancialSummaryApi/"]
WORKDIR "/src/FinancialSummaryApi"
RUN dotnet publish "RotFinancialSummaryApiativa.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
RUN chmod 755 /app/Rotativa/Linux/wkhtmltopdf
CMD ASPNETCORE_URLS=http://+:${PORT:-3000} dotnet ./out/FinancialSummaryApi.dll
