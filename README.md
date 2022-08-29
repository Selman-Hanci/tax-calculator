# Congestion Tax Calculator

Volvo Cars Congestion Tax Calculator assignment.

This repository contains a developer [assignment](ASSIGNMENT.md) used as a basis for candidate intervew and evaluation.

Projects main aim is to calculate congestion taxes for given vehicle. Calculation is done using vehicles daily toll pass history and in the end total toll fee value is returned. Incase vehicle is considered to be toll free vehicle the toll fee is set to 0.

A new file structure has been created to have a better readability. API, Business and Test projects have been used. Open API Swagger UI is used for user interaction. 

As requested tax rules are taken as a parameter to have a more generic tax calculation. All the tax rules and vehicle toll passing date can be entered as a swagger parameter.

Project has a three parameters;
-vehicleType
-stringDates
-taxRules

vehicleType is the string parameter that accepts string value, to have a better functionality vehicle type is converted into a string parameter
Example: Car, Motorcycle, Tractor, Emergency, Diplomat, Foreign, Military

stringDates is the string array parameter that accepts vehicles toll passing dates line by line and needs to be entered one by one. Values on given scenario can be used directly
Example: 
2013-01-14 21:00:00
2013-01-15 21:00:00
2013-02-07 06:23:27
2013-02-07 15:27:00
.
.

taxRules is the string array parameter added to have a functionality to calculate congestion tax without being limited to a single city. All the rules needs be entered to a separate line. Storing the values to an external storage or caching previously entered values is an option but because of time limitations swagger UI is used. Tax values on given scenario can be used directly. 
Example:
06:00–06:29	SEK 8
06:30–06:59	SEK 13
.
.

Application can be improved but because of time limitations decided to have only current features. Separate todo list including missing and additional features is created incase the application needs to be improved.
