

start "Validator" /D C:\temp\going-async\Sixeyed.GoingAsync.AppV2.Consumer C:\temp\going-async\Sixeyed.GoingAsync.AppV2.Consumer\Sixeyed.GoingAsync.AppV2.Consumer.exe /e Sixeyed.GoingAsync.AppV2.Consumer.trade-validate
start "Party1Enricher" /D C:\temp\going-async\Sixeyed.GoingAsync.AppV2.Consumer C:\temp\going-async\Sixeyed.GoingAsync.AppV2.Consumer\Sixeyed.GoingAsync.AppV2.Consumer.exe /e Sixeyed.GoingAsync.AppV2.Consumer.trade-enrich-party1
start "Party2Enricher" /D C:\temp\going-async\Sixeyed.GoingAsync.AppV2.Consumer C:\temp\going-async\Sixeyed.GoingAsync.AppV2.Consumer\Sixeyed.GoingAsync.AppV2.Consumer.exe /e Sixeyed.GoingAsync.AppV2.Consumer.trade-enrich-party2


rem sqlcmd -S .\SqlExpress -d GoingAsync -Q "truncate table incomingtrades"
rem del /Q C:\temp\going-async\in\app-nsb\*.*
rem cd C:\temp\going-async\Sixeyed.GoingAsync.Tools.TradeGenerator\Sixeyed.GoingAsync.Tools.TradeGenerator.exe /a v2 /c 100
rem cd C:\temp\going-async\Sixeyed.GoingAsync.AppV2.Producer\Sixeyed.GoingAsync.AppV2.Producer.exe