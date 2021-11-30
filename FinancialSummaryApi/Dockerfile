FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /app
# For Rotativa SDK
#RUN apt-get update && apt-get install -y --no-install-recommends fontconfig libfreetype6 libjpeg-turbo8 libpng16-16 libjpeg.so.62 libx11-6 libxcb1 libxext6 libxrender1 xfonts-75dpi xfonts-base libgdiplus libc6-dev
RUN  yum install -y yum-utils rpmdevtools
RUN yum install freetds


# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet build -c Release -o out
RUN dotnet publish -c Release -o out
#RUN ["apt-get", "update"]
#RUN ["apt-get", "-y", "install", "libgdiplus"]
RUN chmod 755 /app/wwwroot/Rotativa/wkhtmltopdf
RUN chmod 755 /app/wwwroot/Rotativa/wkhtmltoimage
RUN chmod 755 /app/wkhtmltopdf/wkhtmltopdf
RUN chmod 755 /app/wkhtmltopdf/libjpeg.so.62
RUN chmod 755 /app/wkhtmltopdf/fonts/
RUN chmod 755 /app/wkhtmltopdf/lib/
EXPOSE ${PORT:-3000}
CMD ASPNETCORE_URLS=http://+:${PORT:-3000} dotnet ./out/FinancialSummaryApi.dll
