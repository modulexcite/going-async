# going-async with NServiceBus

Demo code for the Webinar: `Asynchronous Messaging with NServiceBusg`.

The Webinar was hosted by [@ParticularSW](http://particular.net/), the makers of NServiceBus, ServiceMatrix, ServicePulse etc.

It was presented on 21st May 2015 by [Daniel Marbach](https://twitter.com/danielmarbach) and [Mauro Servienti](https://twitter.com/mauroservienti), Solution Architects @ Particular.

# The Code

* Looks at a very simple Validate-Enrich problem.
* All versions use SQL Server (configs expect a local, `.\SqlExpress` instance).
* the solution uses MSMQ for the message queues.

## Usage

`setup.cmd` to create the queues & input directories. 

Build the solution, then `run-app-NSB.cmd` to generate some input files and process them. 