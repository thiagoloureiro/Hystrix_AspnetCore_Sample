FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Hystrix_AspNetCore_Sample.sln ./
COPY Hystrix_AspNetCore_Sample/Hystrix_AspNetCore_Sample.csproj Hystrix_AspNetCore_Sample/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Hystrix_AspNetCore_Sample
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Hystrix_AspNetCore_Sample.dll"]
