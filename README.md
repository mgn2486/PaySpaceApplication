Clone the repo

Updated the connection string in the WebUi Project (AppSettings.json) to point to your Sql Server Database:

 "ConnectionStrings": {
    "PaySpaceDbConnection": "Data Source=GOMBA-LAPTOP\\MSSQLSERVER01;Initial Catalog=PaySpaceDb;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"
  }

Run the prestore migrations

Make sure the WebUI is the Startup project 
Make sore on the Pckage-Manager-Console The default database is set to 
src\PaySpaceDAL\PaySpaceDAL

Run the Command :

 - `updated-database`

 Run the integration tests in the Tests folder under
 IntegrationTests

 Run the tests Sequentially in the `TaxControllerTests`

 This will pre populate the database with the required postal Codes and ranges.

