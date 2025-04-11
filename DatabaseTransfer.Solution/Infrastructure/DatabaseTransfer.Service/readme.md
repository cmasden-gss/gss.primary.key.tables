# Database.Transfer Solution
A .NET Framework/C# implementation for Actian to MicrosoftSQL and/or PostgreSQL

| Name | Resources |
| ------ | ------ |
| Front End| DatabaseTransfer.SetupWizardUi |
| Back End| DatabaseTransfer.Service |

## Service

### Flow
```mermaid
graph LR
A(Startup) --> B(States\StartupState & States\UtilityState)
A --> C(Setup Wizard Configuration Exists)
C --> D{Exists}
D --> E(F: Run the Setup Wizard Application. Exiting.)
D --> F(T: Loaded Setup Wizard configuration.)
F --> G(Transfer Routine)
```