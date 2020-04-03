# Stream Watcher
## Basic Information
This project allows a Windows computer (tested only on Windows 10) to:
- Open an included browser to a specific site at a specified time daily
- Close the same brower at a specified time daily


## Prerequisites

1. **NOTE:** This project does NOT install anything onto a computer.
2. **NOTE:** This does NOT require any browser to be previously installed as it contains a local copy of chromium (sourced around 02/04/2020)
3. As stated earlier, this has only been tested on Windows 10. It may work for other Windows versions however it just hasn't been tested. It likely won't work for Mac and linux since it uses Windows Task Scheduler to achieve this

## How to Run
### Quickstart
First download the `StreamWatcherProduct.zip` which can be found [here](https://drive.google.com/drive/folders/1ETs3-628MX0-BNXk51u9sGjbR1vwzpHt?usp=sharing), move to your desired location and then unzip. Next, (configure if desired by seeing the *Configuration* section later, otherwise) run as shown below.

#### Run the program

To run the program simply run/double click on the file named `./RunStreamWatcher.bat`

#### Remove the program

To remove the program simply run/double click on the file named `./RemoveStreamWatcher.bat`

### Configuration
The program can be run as soon as it is downloaded  however to configure it to your purposes, the following **should be changed**:
- **To change what time the browser is launched and stopped**, the `StartTime` & `StopTime` values should be changed in the file `./config.txt`. These times can be found under the `[Times]` banner and must be in a 24-hour format. Below is an example of what it could look like in the config.txt file:

```
...
[Times]
StartTime=20:40
StopTime=20:45
...
```
- **To change what site is opened by the browser**, just change the value of the `targetSite` key in the `/LaunchTargetSite.dll.config' file. An example of what it could like in the file:
```
...
<configuration>
  <appSettings>
  ...
    <add key ="targetUrl" value="https://www.myUrl.com"/>
    ...
  </appSettings>
</configuration>
...
```
### Normal Start
To start using the files in this respository:
1. Start by downloading this repository
2. A copy of chromium will need to be added to `StreamWatcherProduct/chrome-win32/`. As of now (02/04/2020), chromium can be found [here](https://www.googleapis.com/download/storage/v1/b/chromium-browser-continuous/o/Win_x64%2F381909%2Fchrome-win32.zip?generation=1458311015992000&alt=media), however if that link is broken, try look for newer versions [here](http://commondatastorage.googleapis.com/chromium-browser-continuous/index.html). 
3. You can now continue using the *Quickstart* guide above. However if you are more technical, feel free to see the *Developer Information* section below which can guide people who want to do more with the code

## Known Potential Issues
Some potential problems are:
1. **Browser with multiple tabs not closing:** The Task Scheduled that closes the browser does not work if the site has been launched by a browser containing multiple tabs. If the program launches the default browser (which can be an issue), if the user already has the browser opened, then this will just add another tab to that browser and prevent the `CloseTargetSite` task to execute. This is why an alternative browser was chosen (chromium), as users won't use it (can run in the background) and thus they can continue browsing without needing to add tabs to the chromium browser. **However** If the user does add tabs to chromium, the `CloseTargetSite` task will not succeed.
2. **Tasks not executing:** If it is found that tasks are not executing as soon as they are installed (or at the scheduled time), then it may not be an issue of the program. As of currently (02/04/2020) for some reason if you schedule a daily task today, it doesn't always execute on time. When developing, it has been noticed that it will sometimes execute just 5/10 minutes later than the scheduled time on the first day. It is unkown what happens on further days as it has been tested to that level just yet. 
3. **Tasks look like they are not executing but are failing - Permissions:** The task can be checked if it suceeded or not by selecting the task in Windows Task Scheduler and viewing it's history. If it has failed to run, it may be a permissions issue. Permissions has been a struggle when developing this program. It should run as admin, however (I think) it may nto always create the task as admin. I didn't test this enough, but if there is an issue with a task failing (which is different to point 2), this could be a reason. To fix this, the powershell scripts may need to be run manually and adjiusted for specific user accounts

## Versions
Currently, only one version has been released but with no specific version number. Versioning is something in the future plans

## Advanced Information
### Technologies used:
The project uses:
- Batch files (.bat)
- Powershell scripts (.ps1)
- Basic C#, .NetCore 3.1 (as of 02/04/2020)
- Chromium browser

### Composition
The project (mainly) consists of:
- **Two batch files:** Which are the most abstract layer that acts as 'running'/'uninstalling' the program (I say acts because nothing is installed. Tasks are just added).
- **Two powershell files:** Which are called by the respective batch files to 'create'/'remove' the tasks.
- **Two executable files:** Which are the programs that are called by each scheduled task. These executables are responsible for actually launching the browser, saving the process, then later closing it.
- **Two ~.dll.config files:** Which are configurations for the respective executables. The only real thing requried for users to deal with in this is the URL to launch the program. 
- **A base config.txt file:** This is used by the powershell scripts when creating the tasks initially (and then eventually when they are closed - see future plans section).
- **A folder containing chromium:** This just contains a local copy of a runnable chromium executable. 
- **Other files:** These are references as well as metadata storage files for the executables

### How it works
The project works by adding two tasks to Windows Task Scheduler when the `./RunStreamWatcher` program is executed. Both tasks can be viewed by launching 'Windows Task Scheduler' and opening the tasks under the `StreamWatcher` dropdown. The first Task is called `LaunchTargetSite` which uses the `LaunchTargetSite.exe` to launch a browser with a specified URL. Using a local copy of the browser was desirable as it allows the user not to have the browser installed as otherwise it could launch a tab of their default browser (which could cause problems - see the issues section).


## Future Plans
1. Versioning needs to be added
2. Code needs to be cleaned
3. Logging needs to be added
4. One thing that could be improved in the future is compiling the three config files (`config.txt`,`LaunchTargetSite.dll.config`,`CloseTargetSite.dll.config`) into one config file.
5. Another is to make the Task Names in the `DeleteScheduledTasks.ps1` script extracted from the `config.txt` file instead of them being hardcoded. This can be done by copying the same config reading code in the `CreateScheduledTasks.ps1` script.
6. Make the chromium browser invisble to users.
7. Potentially get licensing

## Developer Information
To view/edit the C# code, clone the repository and launch the Visual Studio solution. In it, there will be three projects:
- **LaunchTargetSite**: The project that creates the excutable to launch the target site 
- **CloseTargetSite**: The project that creates the excutable to close the target site 
- **TargetSiteInteractor**: This project contains both the code of the LaunchTargetSite & CloseTargetSite in one program and uses a loop to allow developers to test functionality of both in one. This is useful for debugging. As of now (02/04/2020) this project may not contain all the up to date code from both projects, but can be updated if the code is just mixed.

To add the excutables made from Visual Studio to the normal project, simply take the files from the bin folder of each project (except the TargetSiteInteractor project) and copy them into the `StreamWatcherProduct`.

The same should be done for the powershell scripts and batch files if they are edited int the `CommandScripts` folder.

Make sure that a copy of chromium is included in the solution - see the *Normal Start* section
## License
*Future plans*
<!-- Licensed under the [Creative Commons Zero](http://creativecommons.org/publicdomain/zero/1.0/) making it [public domain](https://en.wikipedia.org/wiki/Public_domain) so you can do whatever you wish with it without worry (you can even remove this notice!)
<br/>Copyright &copy; 2011+ [Benjamin Lupton](http://balupton.com) -->