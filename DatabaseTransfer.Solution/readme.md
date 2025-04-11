# Database.Transfer Solution
A .NET Framework/C# implementation for Actian to MicrosoftSQL and/or PostgreSQL

## DatabaseTranser.SetupWizard

Views
+ StartPageUserControl
  + Information only, no code behind the scenes
+ ConnectionPageUserControl
  + Checkes connection strings to selected db(s), a hard stop until a connection is confirmed
+ ScheduleBrowserPageUserControl
  + Holds the list of schedules that holds the list of selected schemas
+ SchedulePageuserControl
  + Databinding schedule information
+ SchemaBrowserPageUserControl
  + The meat of the schema selection process, used to select tables/columns/data
+ SchemaPageUserControl
  + Databinding schema information + where clause / data preview
+ FinishPageUserControl
  + Saves the configuration file for the service

Configurations
> These are used for AutoMapper, since a IsSelected property is required for binding a checkbox to the UI and then the objects are mapped back to the expected types

### Debugging a client's configuration file
+ If you attempt to load a client's configuration file it will blow up the program
  + You'll need to replace the actian/transfer connection hash with your own in the json file since it's machine specific

## DatabaseTransfer.Service

+ TransferService
  + Setups the windows service environment, with the main meat being in TransferState
+ TransferState
  + Loads the application configuration, checkes the datetime to see if a schedule needs to be processed and processes the schemas selected, the comments/flow is/are straight forward

### Deployment
 + Increment the File.Version in both SetupWizard & Service to know which version is released
   + It spits the version into the log to know off the bat if they are running the latest version
 + Right Click both projects and Build in Release

## DatabaseTransfer.DatabaseprovidersConsoleApp
> Chris wanted a program to dump all DbProviderFactories

## DatabaseTransfer.PostgresConnection.CoreConsoleApp
> Debugging with a client to identity postgres connection on Windows/Linux

## DatabaseTransfer.DependenciesCheck.CoreConsoleApp
> Identified conflicts / DLLs that are being pushed into plugins which caused ETL to have METHODNOTFOUND exceptions in the SetupWizard and Service