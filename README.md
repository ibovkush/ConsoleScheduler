# ConsoleScheduler
Test Console Scheduler application

First you need to download all Nuget packages in case when VS not load it automatically 
To run application you need setup DB and set keys in App.config file

1) Setup DB.
- Make sure that you had already created empty Database for this test application
- Go to DataLib\App.config and replace DefaultConnectionString data with connection string to your test DB 
- Open Package manager console
- Select DataLib as Default project
- use Update-Database command to add DB structure.

2) Setup keys in ConsoleApp\App.config.
- setup SchedulerDelayInMinutes key to change Scheduler delay between data download calls. 
- setup GetDataForLastNDays key to change how many days of old data (from current monent) will be performed. 
- setup GetDataUrl key to change url where application should receive a data. 
- setup DebugAllData key (True or False) to show/hide received data in console output. 

Select ConsoleApp project as "Startup project" if it is not.
Run.

To Stop the scheduler hit Esc one time. After this you can close application by pressing any button.
