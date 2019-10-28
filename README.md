Listen to IoT Hub with Azure Function — Part 1 
==============================================

In this article, we walk through how to listen Azure IoT hub(D2C), with
a single Azure Function, without raising any host for listening.

* * * * *

### Listen to IoT Hub with Azure Function — Part 1 

In this article, we walk through how to listen Azure IoT hub(D2C), with
a single Azure Function, without raising any host for listening. 

In the last few days, I required to integrate with azure IoT to existing
system, and i decided to use Azure IoT hub trigger, because a various
reasons,

1.  It’s Cheaper than runs host up and running.
2.  It’s open the door to take the system into Microservices
    architecture then the Monolithic architecture

This is a part of article series on Azure Functions, Next articles will
be on: 

1.  **Listen to IoT Hub with Azure Function** — part 2 — Working with
    Autofac, Create controllers and functions for IoT, handle the data
    that come from the device, and how to invoke them dynamically.
2.  **Scheduler Function** — instead of using scheduler on the server
    itself, such Hangfire, Quarts etc we can use OrchestrationTrigger
    instead.



* * * * *

![Azure component
architecture.](https://cdn-images-1.medium.com/max/800/1*I__aBXxnJLpQSCepSUQ3xQ.png)

Azure component architecture.

A brief introduction:

#### IoT — Internet of Things.  

> The Internet of Things is a system of interrelated computing devices,
> mechanical and digital machines, objects, animals or people that are
> provided with unique identifiers and the ability to transfer data over
> a network without requiring human-to-human or human-to-computer
> interaction.
> [Wikipedia](https://en.wikipedia.org/wiki/Internet_of_things).

#### Azure IoT Hub.  
> Azure IoT Hub is the core Azure PaaS that both Azure IoT Central and
> Azure IoT solution accelerators use. IoT Hub supports reliable and
> secure bidirectional communications between millions of IoT devices
> and a cloud solution. [Microsoft
> docs](https://docs.microsoft.com/en-us/azure/iot-central/overview-iot-options).

#### Azure Function.

> Azure Functions is the serverless computing service hosted on the
> Microsoft Azure public cloud. Azure Functions, and serverless
> computing, in general, is designed to accelerate and simplify
> application development

* * * * *

In order to works on this project, we will need the
following prerequisites.

-   Active Azure account. 
-   Visual studio (Community edition is enough).
-   Basic C\# Acknowledge. 

> In case of Azure Function template project isn’t found on your Visual
> Studio, go to the Tools menu, choose Extensions and Updates. Expand
> Installed \> Tools and choose Azure Functions and Web Jobs Tools.

* * * * *

Ready? Let’s dive in!

***Step one*** — Creating Azure IoT Hub and device(if you already have
one, you can jump to step two).

1.  Signing to Azure portal [**here**](https://portal.azure.com)
2.  Create resource \> Search for IoT Hubs in the marketplace \> fill
    the required fields(Choose the free plan), and create it.
3.  Wait a few moments, once the Hub will create you will see a toast
    for it.
4.  Our hub is ready? let’s create a device to created hub.
5.  Navigate inside the hub menu \> IoT Device \> Add \> and fill the
    following fields: **DeviceId — **name of the device,
    **Authentication type — type of Authentication**choose the symmetric
    key(aka SAS) and leave the auto generate keys checked, and enable
    the connect this device to IoT hub.
6.  Press on save, wait a few and Walla!, our device is ready.

![Azure IoT device
craetion](https://cdn-images-1.medium.com/max/800/1*Kpg5ngGIkuoL82y4DMA1BA.png)

Azure IoT device creation.

* * * * *

***Step two*** - Creating the Hub listener that will receive all the
messages from the devices, with Azure Function(IoTHubTrigger function
type).

1.  Open Visual Studio ***\>*** Create Azure Function project.
2.  Select IoT hub trigger v2(The main difference between v2 and v1 is
    that the v2 is based on .Net core while v1 using .Net Framework and
    the connection string is from 2 difference places).
3.  Fields that need to fill:***Storage account*** — for now you can
    choose a simulator for local, ***Connection string
    setting**** — *the key of the connection string value from
    local.setting.json (**IMPORTANT! —**the connection string value need
    to take from, Go to created IoT hub \> Built-in Endpoints \>
    Event-hub compatible endpoint.****) ***Path ***— Leave as is
    (messages/events).
4.  Create the project.

* * * * *

Once the project is created we should see one created function, with the
following args: 

1.  **IoTHubTrigger**\> **1.** event hub name. **2.** connection string
    (v2 is the key of connection string value from local.setting.json as
    above).
2.  **EventData \>**Holding the data of the event from the device such
    the message payload, message properties i.e.** **
3.  **ILogger **— as is sound, represent a logger.

* * * * *

***Step three***  — Let’s create a device that responsible sending the
messages(D2C).

1.  Create a Console Application, name it and create.
2.  Install the package Microsoft.Azure.Devices &
    Microsoft.Azure.Devices.Client from NuGet.
3.  Add the following function into created class(it’s also withing the
    rest of the project).

![Generate SAS token
function](https://cdn-images-1.medium.com/max/800/1*XNtfDFNcdCsOWIhkTC05UA.png)

Generate SAS token code

​4. Add the following line to your main function

![](https://cdn-images-1.medium.com/max/800/1*j9jvLtK7gtrCqjiaJesweg.png)

Main function

​5. Add the following function for sending the event

![Send event
funciton](https://cdn-images-1.medium.com/max/800/1*2066Vwg0b14mtP_r3fMZbA.png)

Send event function

​6. Build the project (CTRL +SHIFT + B).

​7. To run the device withing the function, Right click on the solution
***\>*** Properties ***\>*** Startup Project **\>** Check the Multiple
startup projects \> Select the function and console and select the
Action — start. 

Hit F5 for running, it’s may take a while for the first time, it
downloading tools for the cli 



![](https://cdn-images-1.medium.com/max/800/1*zmZTQU6P6miH8qELZFXFpg.png)

Once it’t done, you should see the cli runs.

![](https://cdn-images-1.medium.com/max/800/1*fsbVr6-cy2UnJ9F_rqS2SA.png)



Congrats! your function is up and running, now you should receive the
messages from the devices.

So if you’ve come this far, I’m really happy, and thank you! for reading
this article, hope it helped.

**Next article will discuses on Listen to IoT Hub with Azure
Function** — part 2



[View original.](https://medium.com/p/743407b79163)

Exported from [Medium](https://medium.com) on October 28, 2019.
