## How to run:

### Database
This requires a local instance of sql server to be running.  The expected login details are `sa` and `Welcome1$`.  To achieve this you can run `InstallSqlServerDocker.cmd` which will create a docker container running SQL Server.  If you want to use an existing SQL Server instance, please update the connection strings in `appsettings.json` and `BaseTests.cs`.

### Launching
Once a SQL Server instance is available, you can just launch the app from visual studio.  Entity Framework will run migrations to ensure the schema is created and some test data is seeded.  There is no provided UI, but I have added Swagger to the app so you can easily access the API.

## Observations

### Entity Framework Migrations
For this, I have enabled EF Migrations.  This is suitable for running locally or in a dev environment.  However, it is not reccomended for production, so for deployment I would use EF to generate the scripts instead, which can be examined and tested before deploying.

### Password in source control
It is not good practice for passwords to be kept in source control.  However, as the only password stored is for running locally, this is ok.  Production passwords can be stored in SecretServer or similar.

### Using sa login
Normally you'd use a login with only the permissions required by the app, i.e. restricted to reading and writing to one database and no schema changes.  

### ISIN validation
It's not specified in the requirements, but I've added a check that the ISIN is 12 characters long.  Also, I've made it so the first two characters should be uppercase also.

## Improvements & Next steps

### Integration tests
There is not a full set of integration sets for the existing API, this obviously would need to be completed.  Additionally, it would be good to run on a different database to the app, this could be cleared before each run of the tests.

### Authentication
I envisage basic authentication plus token authentication to be used.  There should be a users table that contains salted and hashed passwords.

### Containerisation
The backen API could be combined with a UI and be used to create a docker image.  Multiple instances of this could be deployed using ECS or similar, with a load balancer routing requests and adding an SSL layer.
