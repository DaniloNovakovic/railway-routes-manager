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









