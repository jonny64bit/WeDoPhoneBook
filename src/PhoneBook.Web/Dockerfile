FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PhoneBook.Web/PhoneBook.Web.csproj", "PhoneBook.Web/"]
RUN dotnet restore "PhoneBook.Web/PhoneBook.Web.csproj"
COPY . .
WORKDIR "/src/PhoneBook.Web"
RUN dotnet build "PhoneBook.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhoneBook.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhoneBook.Web.dll"]
