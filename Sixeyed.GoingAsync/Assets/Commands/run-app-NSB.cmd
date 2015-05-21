start "Validator" /D C:\temp\going-async\Sixeyed.GoingAsync.AppV2.Consumer C:\temp\going-async\Sixeyed.GoingAsync.AppV2.Consumer\Sixeyed.GoingAsync.AppV2.Consumer.exe /e Sixeyed.GoingAsync.AppV2.Consumer.trade-validate
start "Party1Enricher" /D C:\temp\going-async\Sixeyed.GoingAsync.AppV2.Consumer C:\temp\going-async\Sixeyed.GoingAsync.AppV2.Consumer\Sixeyed.GoingAsync.AppV2.Consumer.exe /e Sixeyed.GoingAsync.AppV2.Consumer.trade-enrich-party1
start "Party2Enricher" /D C:\temp\going-async\Sixeyed.GoingAsync.AppV2.Consumer C:\temp\going-async\Sixeyed.GoingAsync.AppV2.Consumer\Sixeyed.GoingAsync.AppV2.Consumer.exe /e Sixeyed.GoingAsync.AppV2.Consumer.trade-enrich-party2


sqlcmd -S .\SqlExpress -d GoingAsync -Q "truncate table incomingtrades"
del /Q C:\temp\going-async\in\app-nsb\*.*
start C:\temp\going-async\Sixeyed.GoingAsync.Tools.TradeGenerator\Sixeyed.GoingAsync.Tools.TradeGenerator.exe /a v2 /c 1
start C:\temp\going-async\Sixeyed.GoingAsync.AppV2.Producer\Sixeyed.GoingAsync.AppV2.Producer.exe