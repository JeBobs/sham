# Sham!
#### Currently working on a major rewrite!
A C# command-line tool with various abilities to help Halo content creation by interfacing with various file formats (JMS, etc).
This app is still very early in development. Features and code structure is subject to change.

# Usage
## Installing & Running
<ul>
<li>Check the releases page for the latest stable version of Sham!</li>
<li>Download the executable archive & unzip the package into your H2EK or H3EK directory.</li>
<li>Run using your favorite command-line program (cmd, PowerShell, Windows Terminal, Hyper, Bash, etc).</li> 
</ul>
            
## Commands
#### Implemented:
            help <command> - Either shows the help screen or provides help for a command.
            GenerateH2Shaders [JMS File] - Generates empty Halo 2 compatible shader files for each material slot.
            JMCompress [JMS/JMA Variant File] <output path> - Takes an input JMS/JMA/JMM/JMO/JMI file and removes white-space & comments.
            
#### Planned:
            GenerateH3Shaders [JMS File] - Generates empty Halo 3 compatible shader files for each material slot.

## Compatibility
Sham! will run on any OS capable of running .NET Framework 4.0, including but not limited to:
<ul>
  <li>Windows 2000 (Extended Kernel Required)</li>
  <li>Windows XP</li>
  <li>Windows Vista</li>
  <li>Windows 7</li>
  <li>Windows 8/8.1</li>
  <li>Windows 10</li>
  <li>Windows 11</li>
  <li>Linux (Through Mono/Wine)</li>
</ul>

## Build Instructions
To build Sham!, you only need .NET 4.0 features installed. Newer versions of .NET all the way up to .NET 7 work, but if you're targeting the compatibility listed above, you will need to target .NET 4.0. 

Originally I wanted to reach a very broad set of operating systems with one small executable, but I may decide to bump the minimum version if significant improvements are brought using a newer C# version.

## Contributing
If you would like to contribute, feel free to create a pull request with your feature.
