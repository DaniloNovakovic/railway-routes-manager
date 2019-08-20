# RVA_SchoolProject

[![Build Status](https://dev.azure.com/dakenzi97/RvaProjekat/_apis/build/status/DaniloNovakovic.RVA_SchoolProject?branchName=master)](https://dev.azure.com/dakenzi97/RvaProjekat/_build/latest?definitionId=1&branchName=master)
[![Board Status](https://dev.azure.com/dakenzi97/d60d784a-e122-4c92-a447-3ccb352a5663/4248bfb9-fe72-4b6f-a6e7-193b9bfd7451/_apis/work/boardbadge/da74dcbe-8edf-4e01-a49a-5540bd8b75da?columnOptions=1)](https://dev.azure.com/dakenzi97/d60d784a-e122-4c92-a447-3ccb352a5663/_boards/board/t/4248bfb9-fe72-4b6f-a6e7-193b9bfd7451/Microsoft.RequirementCategory/)

School Project from Multi-tier applications development class in Faculty of Technical Sciences - University of Novi Sad 🏫

## Getting Started

Use these instructions to get the project up and running.

### Prerequisites

You will need the following tools:

* [Visual Studio 2017-2019](https://www.visualstudio.com/downloads/)
* [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/net472)

### Setup

Follow these steps to get your development environment set up:

1. Restore NuGet Packages & Build solution
1. (Optional) Set solution to multiple startup projects ( *Right Click Solution > Properties > Multiple Startup Perojects*) where `Server.WCF` project will be loaded first and then `Client.DesktopUI`
1. Run solution


> Note: For best user experience (without interruptions from debugger) it is advised to run project without debugging (either with `CTRL+F5` or by running it in `Release` Mode).


## Usage Guide

This section will focus on how to use this application, as well as give brief explanation on what each display does.

### Server

Console Application which hosts `WCF` services. It displays log information which can be configured in App.config by setting `level` value to one of the following: `(ALL, DEBUG, INFO, WARN, ERROR, FATAL, OFF)`

```xml
<log4net>
 <root>
  <level value="INFO"/>
 </root>
</log4net>
```

User can close *Server* by pressing `ENTER` on keyboard.


### Client

WPF Desktop Application through which User can interract with [Server](#server).

Once the application has been run, user will be greeted with Login screen after which he will have different view options based on his role (*Administrator* or *Regular User*). 

![Login Screen](doc/login-screen.PNG)

Each user has options to view Routes, Stations, Log information, Profile and to Sign Out:

![User Nav](doc/user-nav.PNG)

Administrator has special view dedicated to him which is to see the list of *Users* and to add them:

![User List](doc/user-list.PNG)


#### Routes

Displays railway routes as list of expandable elements that (once clicked) themselves display list of stations that are included in that route. 

![Routes View](doc/routes-view.PNG)

User can Add, Duplicate, Edit and Delete routes upon which modal popup window would be shown.

![Edit Route View](doc/edit-route-view.PNG)

Additional available commands are Undo, Redo and Refresh.

#### Stations

Similar to Routes this view presents list of expandable items, but unlike in previous view, user can modify and delete Railway Platforms directly in the expanded table.

![Stations View](doc/stations-view.PNG)


#### Log

Displays Log information in table. User can see potential conflicts, information and errors in this view. Just like in [Server](#server) log level can be modified in `App.config` file

![Log View](doc/log-view.PNG)



#### Profile

Each user can edit their first and last name.

![Edit Profile View](doc/edit-profile-view.PNG)