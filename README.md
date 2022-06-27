# EmployeeAPI
## Description
This Program retrieves employee data and contact information.  

## Usage
Run this program by navigating to the EmployeeAPI directory and entering these commands:
	dotnet build
	dotnet run

Get employee contact information by entering this command:
	curl -X "GET" "https://localhost:7047/api/Employee/" -H "accept: text/plain"
	curl -X "GET" "https://localhost:7047/api/Employee/{id}" -H "accept: text/plain"

Test this route by navigating to the EmployeeAPI_Test directory and entering this command:
	dotnet test