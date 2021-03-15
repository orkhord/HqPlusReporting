# HqPlusReporting

This is a .Net 5 Console Application.

Just open the solution in Visual Studio and press F5.

By Default, the application takes the input file from <project_folder>/input-data folder.

Alternatively, you can pass file path as an argument in command line, for example:

> HqPlusReporting.exe "C:/hotelrates.json"

*Suggest an architecture on how to automate the process that an email is sent at time x attaching the
report (frameworks, libraries, ready made solutions etc.: free of choice):*

1) The easiest way to do it is to create a task in Windows **Task Scheduler**, specify the conditions that will trigger the task (at time x) and the action (starting console application).
2) Alternatively, create a long-running application such as **Worker Service** or **Windows Service** and use **Quartz.NET** or **Hangfire** to set schedule.
