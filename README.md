# DNN Pulse Module

## Content
- [Overview](#overview)
- [Requirements](#requirements)
- [Installation and configuration guide](#configuration)
  - [Module installation](#module-config)
  - [Webconfig](#webconfig)
  - [Programmed Task configuration](#programmedTask-config)
- [Verification](#verification)
- [Building the solution](#build)
  - [Requirements](#build-requirements)
  - [Build the module](#build-module)

<a name="overview"></a>
## Overview
The DNN Pulse is a module for DNN Platform able to track your DNN version, portal Aliases and all installed modules sending them to your Azure Application Insights.

<a name="requirements"></a>
## Requirements 

* **DNN Platform 9.0.0 or later**
* **Azure Application Insights resource**

<a name="configuration"></a>
### Installation and configuration

It's important to remember that you need a DNN deployment with **version 9.0.0 or later**  and an **Azure Application Insights resource** to continue.

<a name="module-config"></a>
#### Module installation

1. Download the DNN Pulse from the Release folder (i.e. DNNPulse_01.00.00_Install.zip).
2. Login into your DNN Platform website as a host user and install the module from the "Settings > Extensions" page.
3. Use the **Install Extension Wizard** to upload and install the file you downloaded on step 1.

<a name="webconfig"></a>
#### Webconfig

Once installed, you will need the **Instrumentation Key** of your **Azure Application Insights** resource.

1. Access your **DNN web.config file** (i.e Settings > Config Manager > web.config).
2. Go to **appSettings** section.
3. Add the following **keys** inside **appSettings**:

```
<add key="DNNPulse.Ikey" value="" />
<add key="DNNPulse.Name" value="" />
```

You will need to change the **value** field with the following:

* **DNNPulse.Ikey**: This is the **Instrumentation Key** of your **Azure Application Insights** resource. 
* **DNNPulse.Name**: "Microsoft.ApplicationInsights.[**Instrumentation Key**].Event" (Just the Key)

Example:

```
<add key="DNNPulse.Ikey" value="1234123-1fabsbf-ahjshf82716-123452" />
<add key="DNNPulse.Name" value="Microsoft.ApplicationInsights.12341231fabsbfahjshf82716123452.Event" />
```
4. Save the file.

<a name="programmedTask-config"></a>
#### Programmed Task configuration

1. In DNN go to Settings > Scheduler > Scheduler.
2. Add a new task.
3. Fill the following required fields:
* **Friendly Name**: The Task name, write one you want (i.e. DNN Pulse).
* **Full Class Name and Assembly**: "Intelequia.Modules.DNNPulse.Tasks.PulseTask, DNNPulse".
4. Choose the parameters you want for the programmed task (Once very day, week...).
5. Enable the task.

<a name="verification"></a>
## Verification

1. Go to your **Azure Application Insights resource**.
2. Access to **Logs**.
3. In **CustomEvents** you will see the results. (It can take up to 5 or more minutes to see the new data in your Application Insights resource).

<a name="build"></a>
## Building the solution
<a name="build-requirements"></a>
### Requirements
* Visual Studio 2019 (download from https://www.visualstudio.com/downloads/)
* npm package manager (download from https://www.npmjs.com/get-npm)
<a name="build-module"></a>
### Build the module
Now you can build the solution by opening the DNNPulse.sln file on Visual Studio 2019. Building the solution in "Release", will generate the installation zip file, created under the "\releases" folder.
