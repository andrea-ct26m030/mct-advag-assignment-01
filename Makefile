PROJECT := ConvexHull/ConvexHull.csproj

.PHONY: build restore release run clean

build:
	dotnet build $(PROJECT)

restore:
	dotnet restore $(PROJECT)

release:
	dotnet publish $(PROJECT) -c Release -o release

run:
	dotnet run --project $(PROJECT)

clean:
	dotnet clean $(PROJECT)