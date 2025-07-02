# Perch Group Technical Test - Azure Functions and REST API

David Grant, July 2025

# Prerequisites
- Visual Studio 2022 or later
- .NET 8.0
- Azure Functions Core Tools
- SQL Server 20 or later (or Azure SQL Database)

# Overview
This repository contains much of the functionality to parse an incoming JSON payload, and run a  extract the `id` and `name` fields, and return them in a JSON response. 

The code is designed to run as an Azure Function (v 4 Isolated Worker model), which should be able eventually call the HTTP GET and POST RESTful methods I have written.  However, I am unfamiliar with Azure Functions, and there would be a steep learning curve involved to make this work. 

In turn, these REST API methods call out to the database, using the DataAccessLibrary.dll WorkstepFlowRepository methods and Command objects, based on Microsoft.Sql.Client methods.

I have used Dapper, with which I am more familiar than Entity Framework, for the calls to the database.  However, this could be easily replaced with EF if needed.

I have NOT used DTO classes to transfer data between the Host app and the data layer, despite this being considered best practice.  As the DTO clases would have the same properties as the Models domain object classes, 
adding them would be redundant, and would require the extra overhead of a mapping library such as AutoMapper.  Instead, I have used the Models classes directly in the API methods.

I have written a small number of unit tests to verify the parsing of the JSON payload for the CustomerId and Property fields, which proved helpful in debugging these functions.

# Disclaimer
This code is all my own work, although I have noted where I have based it on code from other sources (e.g. Microsoft Learn, as I was unfamiliar with Azure Functions). 
I have NOT used GitHub Copilot to suggest the code for me - the code flow is all my own creation.

