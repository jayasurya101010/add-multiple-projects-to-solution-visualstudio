# add-multiple-projects-to-solution-visualstudio

[![Build status](https://ci.appveyor.com/api/projects/status/e7ayw7dq4alvdte2/branch/master?svg=true)](https://ci.appveyor.com/project/jayasurya101010/add-multiple-projects-to-solution-visualstudio)

Adds projects that are in a different folder to the solution file.

## How to use

1. Enter the path where new solution file has to be created along with the solution file name. Do not use existing file names as tool will create a new file and overrides existing file if any.
(Example : C:\Users\User\Documents\Visual Studio 2017\Projects\MyApplication.sln)

2. Enter the folder path where the projects to be added are present. If projects in multiple folders need to be added, enter all the folder paths separated by comma.
(Example : C:\\CoreLibraries\\, C:\\DataLibraries)

3. Press 'A' if you wish to append projects to existing projects or press 'O' to overrite.
(Append adds to existing projects. Overrite will remove all existing projects and adds new ones.)

Process will take time if the project directory has many folders. Wait until you get the success message. Now you can open the newly generated solution file and can see all your projects loaded already. 