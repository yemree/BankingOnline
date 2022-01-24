Simple online banking API that stores Customer , Account, Transaction informations. There is an opportunity that you can create an customer and manage it's transaction processes.

It developed with .Net Core 3.1, based on Event Driven Development, Mediator, MongoDB, FluentValidation, JWT.

You can easily control over via swagger documentation, Set as startup Project Api and run the project then you can see the swagger api page on  "localhost:5001/swagger/index.html"

The Application has 4 main layer; 
- Api
- Data
- Infrastructure
- Manager

it also has CQRS pattern that uses commands and queries. There are handlers to validate requests and send it to data layer. After successful changes the system publishes notification for event log.

Api sends data via Manager Layer. Manager has almost all logic, manages proceeses and uses units from Infrastructure layer (models, classes, settings, documents etc.)   