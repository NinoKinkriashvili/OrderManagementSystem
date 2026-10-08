Order Management System

A small ASP.NET Core Web API application that processes order data from a JSON file.

Technologies

* C#
* .NET 8
* ASP.NET Core Web API
* xUnit
* Swagger
* System.Text.Json

How to Run

Make sure .NET 8 SDK is installed.

From the solution root directory:

dotnet restore
dotnet build
dotnet run --project OrderManagementSystem.Api

The API will be available at the URL shown in the terminal.

Open /swagger to access Swagger UI and test the API.

To run tests:

dotnet test

AI Tools Used

ChatGPT and Gemini were used as development assistants.

AI helped with:

* Discussing project structure and architecture
* Reviewing implementation and edge cases
* Creating and reviewing tests
* Clarifying technical details and implementation choices
* Reviewing potential issues and assumptions

All AI-generated suggestions were reviewed and verified manually.

AI-Generated Code Issues

* An incorrect test calculation was identified and corrected during verification.
* The most popular product logic initially returned only one product in case of a tie. It was updated to return all products with the highest quantity.
* A JSON deserialization issue caused a 500 error. It was fixed by adding JsonStringEnumConverter.

Verification and Fixes

* Manually reviewed the code and logic.
* Tested the API using Swagger.
* Ran the automated tests to verify the results.

Ambiguous Cases and Assumptions

* If multiple products have the same highest sold quantity, all of them are considered most popular and returned.
* Cancelled orders are excluded from statistics.
* Customer search is case-insensitive.
* decimal is used for monetary calculations.
* An unknown or empty customer name returns an empty result.

### Author
**Nino Kinkriashvili**  
.NET | Backend Developer
