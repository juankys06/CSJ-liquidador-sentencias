# Liquidador de Sentencias Judiciales

Breve descripción del proyecto.

## Configuration

You can use the file _appsettings.json_ as a template. Just fill the fields with the necessary values that meet your requirements.

A brief overview of the properties used:

- DefaultConnection: The default database connection string to be used by the webpage.
- Default Log level: Defines the amount of information that will be shown by the process of the webpage. Can be *Debug*, _Warning_ and *Error*.
- Mail: The SMTP configuration.
- EmailDomains: An array of strings of the allowed domains for the user's email addresses. The dots (.) in the string, must be scaped by two backslash (\\\\), 'cause each string will be interpreted as part of a regular expression (regex).

Also, you can have different configurations, for different environments (Development, and Production respectively). So for your development environment you can set a file named _appsettings.Development.json_ with some values, and for production you can set one named _appsettings.Production.json_ with other values. These files will use the _appsettings.json_ as their base, so you only have to specify the properties that will be override. (NOTE: This is a ASP.NET Core feature)

## TODO
- Acomodar los estilos en la hoja de excel.
- Definir estilo de la tabla para la liquidación de hipotecarios, debido la gran cantidad de columnas que requiere su tabla.

## Bugs
- El campo de _Valor Máximo_ en la tabla de VIS (hipotecarios), no se ordena según la cantidad representada, debido a que se convierte a string.
- Indexacion Da problemas tanto en la app original, como en la web. El problema está con la variable ÚltimoMes.
- Liquidador Múltiple no funciona con autoNumeric. Problemático.

## Notas
- En liquidación de créditos hipotecarios, está la columna de Abono a Capital en UVR (abonoCapitalUVR en el código), la cual en la aplicación nunca muestra valor alguno
- Hacer clave primaria T927TIPOLIQUID y T921DATASAINTE.


## Code Annotations

- JSON serialization for audits.
- Action Dependency Injection. (_HomeController.cs_)
- Load configuration properties from file, without having to cast them to a class object. (_Services/EmailSender.cs_)

##### Unique Key with Nullable Values

_Migrations/20191205205812_WebServiceId.cs_: Line 14 - 19

##### Enable Session and cache

In the _Startup.cs_ file, I had to add some extension services like `services.AddMemoryCache()` and `services.AddSession()` due to problems when re-using the data generated by the liquidation, to create the documents (pdf or xlsx). It seems that the default session configuration couldn't re-use large data between requests. So the solution was to save the objects on the server's cache (as shown in _Controllers/LiquidadorController.cs_), and load them when generating the files.

##### Example of anonymous objects thrown on a JSON Response

_LiquidadorController.cs_: Line 56, 66, 146, 429

##### Add Image from Stream to PdfSharp (MigraDoc)

This feature was quite of tricky to achieve, as MigraDoc only has functions to load images from files.

_Utilities.cs_: Line 986 - 994

##### Cast JSON Configuration array to String array

_AdminController.cs_: Line 80

##### Use current Web URL instead of fixed value

To use the current site domain URI (localhost, example.org, etc) instead of a fixed URI, use the function provided in line 107 from _AdminController.cs_.

