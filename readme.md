# DataFramework
Data modeling framework for C#

This framework can be used to generate arfifacts for your data layer.
In other words: Database schema, repositories and enties.

It is built on top of CrossCutting.Data and QueryFramework, which use plain old ADO.NET.

Note that currently, only MS SQL Server is supported ;-)

# Code generation

I am currently not storing generated files in the code repository.
To generate, simply run the console project from either Visual Studio (hit F5) or a command prompt.
This will replace almost all generated code.