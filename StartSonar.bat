dotnet test Tests/Quote.Service.API.Data.Test/Quote.Service.API.Data.Test.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

dotnet test Tests/Quote.Service.API.Service.Test/Quote.Service.API.Service.Test.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

dotnet test Tests/Quote.Service.API.Test/Quote.Service.API.Test.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

dotnet build-server shutdown

dotnet sonarscanner begin /k:Pension.Plan.Quote.Management /d:sonar.host.url=http://localhost:9000 /d:sonar.cs.opencover.reportsPaths=Tests/*/coverage.opencover.xml /d:sonar.coverage.exclusions=”**Test*.cs”

dotnet build

dotnet sonarscanner end