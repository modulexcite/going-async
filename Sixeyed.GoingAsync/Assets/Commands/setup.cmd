mkdir c:\temp\going-async\in\app-nsb\
mkdir c:\temp\going-async\FPML\Schemas\
copy ..\FPML\Schemas\*.* c:\temp\going-async\FPML\Schemas\*.*

start ..\QueueCreator\qc.exe -acwu -n=Sixeyed.GoingAsync.AppV2.Producer
start ..\QueueCreator\qc.exe -acwu -n=Sixeyed.GoingAsync.AppV2.Consumer.trade-validate
start ..\QueueCreator\qc.exe -acwu -n=Sixeyed.GoingAsync.AppV2.Consumer.trade-enrich-party1
start ..\QueueCreator\qc.exe -acwu -n=Sixeyed.GoingAsync.AppV2.Consumer.trade-enrich-party2