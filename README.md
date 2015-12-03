# Create a simple REST API using ASP.NET

# Summary

When you build the project the nuget package restore the packages from the solution (EntityFramework, Moq, Ninject...).
When you run the first time the localSQL is created. I have selected the option to recreate the dabase each time you run the project.

The database contains two rows to test the endpoints.

- TransactionId: 1
- TransactionId: 2

##The solution contain two projects:
- MoneyBox.Api
- MoneyBox.Api.Tests

###The project MoneyBox.Api contain:

- IndexController: simple controller to redirect to a 200 Http Code.
- TransactionController: contains all endpoints. The controller manipulates the data through the TransactionRepository.
- TransactionRepository: (implement ITransactionRepository) class to manipulate the data.
- MoneyBoxContext: class to support Entity Framework Code First approach.

I am using Ninject to inject my repository object (TransactionRepository) to my controller.

###The project MoneyBox.Api.Test:

- Contain all my tests of the REST api.
- I am using Moq to implement Moq from my Repository object to avoid using the database.
- I have used MSTest.



To access the api : http://localhost:SERVERPORT/api/transaction 

## REST Api Endpoints:

| Description         | Http Verb | EndPoint           			|Comments      |
| :--------------------- | :------------:| :-----------------------------------|:-------------|
| Get All Transactions| GET       | /api/transaction   			|	       |
| Get Transaction     | GET       | /api/transaction/{transactionId}    |	       |
| Create Transaction  | POST      | /api/transaction   			|Header Param to pass - Content-Type: application/json	       |
| Update Transaction  | PUT       | /api/transaction   			|Header Param to pass - Content-Type: application/json	       |
| Delete Transaction  | DELETE    | /api/transaction/{transactionId}   	|	       |


A Postman script has been included in the root of this repository to executing the requests detailed below. (It's neccesary to modify the URL and PORT to point the calls correctly.

## Tools Used

- VS 2013 Community
- .Net 4.5.2
- WebApi
- Entity Framework 6 (installed with NuGet)
- Ninject (installed with NuGet)
- Moq (installed with NuGet)

##Time

I spent around 1.5 hours on this. I did during my breaks

##Improvements

Things to include in future versions:

- Implement Swagger.
- Async actions
- Logging
- Validation
- Error Handling

