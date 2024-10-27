# Welcome to the basic Customer API

This is a basic .NET WebAPI that demonstrates CRUD endpoints, unit and integration tests utilizing XUnit, and SQL Server / EntityFramework backend

# Disclosures

I am using Visual Studio 2022 that has many templates built-in that help get initial applications off the ground quickly.  The basic structure of the Web API is initialized by creating a new Web API project within visual studio.


# Data

The Test project replaces the DbContext in the Web API application with an InMemory database and seeds the data on test run.  To run the actual you would need SQL Express or any other SQL server and replace the connection in the appSettings.json file

The SQL DB is as follows:
-- Create Customer table
CREATE TABLE Customers (
    CustomerId UNIQUEIDENTIFIER PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    MiddleName NVARCHAR(50),
    LastName NVARCHAR(50) NOT NULL,
    EmailAddress NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20)
);

-- Create PhoneNumber table
CREATE TABLE PhoneNumbers (
    PhoneNumberId INT PRIMARY KEY IDENTITY(1,1),
    Number NVARCHAR(20) NOT NULL,
    Type NVARCHAR(20) NOT NULL,
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_PhoneNumber_Customer FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

# Unit and Integration Testing

The Test project contains both unit tests and integration tests to the API endpoints.

The unit tests provide basic CRUD testing of the CustomerRepository
The integration tests utilize the Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory object to load an application in memory to send the HTTP requests and validate the responses

Only a few tests were created and some just stubbed due to time constraints.

The tests can be run inside visual studio in the Test Explorer or by running:

dotnet test CustomerAPI01.Test.csproj in a command prompt

# Observability

The WebAPI is simply using the basic OpenTelemetry with Tracing, Metrics, and Logging.  I don't have a vast amount of experience beyond the basics for OTLP.  It's currently just using a Console Exporter but it provides insight into the controller actions, errors, request/response and trace data. Some possible areas that would possibly need additional insights like saving to the database were highlighted. 

# Containerization

The CustomerAPI contains a dockerfile to containerize the API.  Another disclosure, this is simply a checkbox when creating the project in Visual Studio 2022.  The docker file exposes ports 8080/8081 for http/https traffic, utilizes a Windows build, and publishes the release version of the csproj to the /app directory

The container can be ran and debugged directly from Visual Studio 2022.  It can also be ran from a command line using:

docker build -t CustomerAPI:dev .

docker run CustomerAPI:dev

# Kubernetes

Due to time constraints I was not able to complete cluster portion.  I only created the YAML file.  

To deploy the container to MiniKube:
1. Build the docker image within the Minikube VM ()
2. Run: kubectl apply -f customer_api_deploy.yaml
3. Run: minikube service customerapi-service

But this is all untested

# CI / CD

Also due to time constraints I was not able to complete the CI / CD portion.  With more time I would implement utilizing Azure DevOps
1. Create a new Pipeline utilizing this Git repo
2. Use a check-in trigger to automatically pull and build the solution
3. Run the Tests contained in the Test project (this is a built-in task in AzureDev Ops)
4. Publish the Test and Code coverage results to Azure Pipelines (this is also a built-in task in AzureDev Ops)
