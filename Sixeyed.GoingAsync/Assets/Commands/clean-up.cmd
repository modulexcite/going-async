sqlcmd -S .\SqlExpress -d GoingAsync -Q "truncate table incomingtrades"
del /Q C:\temp\going-async\in\app-nsb\*.*