This is a basic overview of the proof of concept for the CookBook
Asp.net Core v3.1
bootstrap
EF core


This was designed as a proof of concept, to help management in the hospitality industry by keeping a digital record of Drinks and ingredients for recipes that can be accessed by staff via the Order Display Monitor(ODM)
Later being expanded to keep track of departments, roles and tasks both recurrant and non-recurrant to help with the high turn over of staff that occurs and the loss of procedures and information that results.

This is only an appropriate solution for ODM's and these are currently only used in higher volume venues however a mobile friendly or app may be coming soon!

There is a test account that has full access to all areas of the application 
Username:  admin@admin.com
password: Admin!1


the database string will need to be changed in app settings.json in order to run the application. Currently the program is configured to run only in SQL.
A code first approach has been used to scaffold the database. Run 'update-database' command in visual studio to scaffold and seed information.

--UPDATE-- 04/02/2022
A DATABASE HAS BEEN HOSTED ON AZURE, CONNECTION STRING IS IN APPSETTINGS.JSON
ALL INFORMATION IS DUMMY DATA. HAVE FUN!

--Change the services in start up for another DBMS--

--IMAGES--
If files are uploaded as photos, they are currently stored in the application, as web storage has not been configured and serialised files are too large

--TASK MANAGEMENT--
The task management section is not finished.
Modifications are still underway and will be finished at some point after I return from brazil. If a task is modified to a recurring task it should change, however no information is currently displayed. This can be viewed in the JobsController

--Logging--
Logging has not been added however will be added soon


