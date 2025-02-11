>>>>>>Functional Aspects
Functional design (FlowChart): CommissioningManager/Flowchart.pdf (check this file first)

What is this program about?
It happens often that the data which are delivered by your business partners, doesn’t meet the requirements. 
Standard solution, e.g XSD is not the solution for the specific problem, as it validates only data type and value, but we need to validate dynamic data value 
which depends on other dynamic data value.  A specific validation process is therefore needed. That’s why this program comes into the play.
This program process the input data (MS Access, icf and json). There are three classes with related Models which represents each input data type with 
related custom attributes and action logic which are being processed centrally based on its type. Each Model contains its custom data conversion logic.

When an user selects certain input data file in Dashboard. The related Models will convert its input data and map the results into the Dashboard (Grid).
When an user trys to validate the results in the Dashboard, the ModelValidator will be called and validate the data based on the selected type and its custom 
attributes which are defined in its Model. The values of all custom attributes of each data will be evaluated in the ModelValidator. If there is one or more 
errors in the grid cells, the related cell will be highlighted. 
The user can correct the cell value manually, ModelValidator will validate the update cell value automatically. 
If the ModelValidator cannot find any error, user can save the input data into database.



>>>>>>Technical Aspects
Technical design (UML): CommissioningManager/TD diagram (UML notation).pdf (check this file first)

This program demonstrates following design patterns/principles/technologies: 
C#, SOLID, multi-threading(Asyn), Generic Delegates, Singleton, MVC, EF, .NET 4.5 and Winform. 

This is a small C# project which I created many years ago, some of the functionalities/classes might be a little outdated now and they might be better 
replaced by new features of latest C# version, but during the time this project was created, the older C# version (especially Winform) had limited features. 
This project illustrates the advanced/generic approach of C# programming. You can also see how those generic "Converter" and "Compare" classes/components 
were made in details, which are currently being used in XAML/WPF/MAUI. 
You can also reuse those components easily in ASP.NET, Webform/MVC, REST API or evens integrate with Javascript framework. (Angular, ReactJs, etc)
(Microsoft might not developed those components in the similar way, but surely in the similar direction)
