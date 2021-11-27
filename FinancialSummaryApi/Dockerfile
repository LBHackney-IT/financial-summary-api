FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /app
# Install dependencies
RUN apt-get update -qq && apt-get -y install libpng16-16
# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet build -c Release -o out
RUN dotnet publish -c Release -o out
RUN chmod 755 /app/Rotativa/Linux/wkhtmltopdf
EXPOSE ${PORT:-3000}
CMD ASPNETCORE_URLS=http://+:${PORT:-3000} dotnet ./out/FinancialSummaryApi.dll
