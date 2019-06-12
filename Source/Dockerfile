FROM microsoft/dotnet:2.1-runtime-stretch-slim as base
#RUN apt-get update && \
#    apt-get install -y --no-install-recommends unzip procps && \
#    rm -rf /var/lib/apt/lists/*

RUN useradd -ms /bin/bash moduleuser
USER moduleuser


#RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l ~/vsdbg


FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

VOLUME [ "/app/data" ]

COPY Build/MSBuild/* ./Build/MSBuild/
COPY Source/*.csproj ./Source/

WORKDIR /app/Source/

RUN dotnet restore

COPY Source/ ./
RUN dotnet publish -c Release -o out


FROM base

WORKDIR /app
COPY --from=build-env /app/Source/out ./
COPY --from=build-env /app/Source/data ./data

ENTRYPOINT ["dotnet", "Dolittle.TimeSeries.Modbus.dll"]