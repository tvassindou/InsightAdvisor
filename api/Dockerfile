# Uses the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
COPY *.sln ./
COPY AdvisorProject/*.csproj AdvisorProject/
COPY AdvisorProject.Core/*.csproj AdvisorProject.Core/
COPY AdvisorProject.Infrastructure/*.csproj AdvisorProject.Infrastructure/
COPY AdvisorProject.Application/*.csproj AdvisorProject.Application/
RUN dotnet restore AdvisorProject

COPY . .
RUN dotnet publish AdvisorProject -c Release -o /out


# Uses the .NET Runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "AdvisorProject.dll"]