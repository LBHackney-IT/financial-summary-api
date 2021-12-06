FROM mcr.microsoft.com/dotnet/core/sdk:3.1
RUN apt-get update && apt-get -y install xvfb && apt-get -y install fontconfig && apt-get -y install libssl1.0-dev && apt-get -y install libx11-dev libx11-xcb-dev libxcb-icccm4-dev libxcb-image0-dev libxcb-keysyms1-dev libxcb-randr0-dev libxcb-render-util0-dev libxcb-render0-dev libxcb-shm0-dev libxcb-util0-dev libxcb-xfixes0-dev libxcb-xkb-dev libxcb1-dev libxfixes-dev libxrandr-dev libxrender-dev

WORKDIR /app
RUN apt-get update \ 
	&& apt-get install -y zlib1g fontconfig libfreetype6 \
        libx11-6 libxext6 libxrender1 \
		libgdiplus libxcb1 xfonts-75dpi \
		xfonts-base wget \
    && wget -nv -O /tmp/wkhtmltox.deb https://downloads.wkhtmltopdf.org/0.12/0.12.5/wkhtmltox_0.12.5-1.stretch_amd64.deb \
	&& apt-get -qy install /tmp/wkhtmltox.deb \
	&& cp /usr/local/bin/wkhtmlto* /usr/bin/ \
    && curl -o /usr/lib/libwkhtmltox.so \
        --location \
        https://github.com/rdvojmoc/DinkToPdf/blob/master/v0.12.4/64%20bit/libwkhtmltox.so?raw=true \
	&& curl -o /usr/lib/libwkhtmltox.dylib \
        --location \
        https://github.com/rdvojmoc/DinkToPdf/blob/master/v0.12.4/64%20bit/libwkhtmltox.dylib?raw=true \
	&& curl -o /usr/lib/libwkhtmltox.dll \
        --location \
        https://github.com/rdvojmoc/DinkToPdf/blob/master/v0.12.4/64%20bit/libwkhtmltox.dll?raw=true \
	&& cp /usr/lib/libwkhtmltox* /app \
	&& cp /usr/lib/libwkhtmltox* /app/PDFNative/64Bit/
# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet build -c Release -o out
RUN dotnet publish -c Release -o out
EXPOSE ${PORT:-3000}
CMD ASPNETCORE_URLS=http://+:${PORT:-3000} dotnet ./out/FinancialSummaryApi.dll
