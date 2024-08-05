# ARtExplorer

## Versions
Unity 2022.3.21f
Visual Studio 2022

## Run on Hololens
1. Unity > File > Build Settings > Build > Save inside Folder "Install"
2. Open Folder Install > open .sln file

Visual Studio 
1. Open Folder Install > open .sln file
2. Settings UI: Debug, ARM64, Remote Machine
3. On right side panel select Universal Windows
4. Visual Studio: Project > Properties > Configuration Properties > Debugging and put the IP of Hololens inside Machine Name field
5. Run

Hololens
- Info IP: Settings > WLAN eduroam > Properties > IPv4
- Info PIN: Settings > Update > For Developers > Under Device discovery > Pair
