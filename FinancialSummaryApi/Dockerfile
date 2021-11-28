FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /app
# Install dependencies
RUN wget -q -O /tmp/libpng12.deb http://security.ubuntu.com/ubuntu/pool/main/libp/libpng/libpng12-0_1.2.54-1ubuntu1.1_amd64.deb
RUN dpkg -i /tmp/libpng12.deb
RUN rm /tmp/libpng12.deb
# Install wkhtmltopdf
RUN apk add --no-cache wkhtmltopdf

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
