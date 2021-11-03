# Sham!
A C# command-line tool with various abilities to help Halo content creation by interfacing with various file formats (JMS, etc).
This app is still very early in development. Features and code structure is subject to change.

## Commands
#### Implemented:
            help <command> - Either shows the help screen or provides help for a command.
            GenerateH2Shaders [JMS File] - Generates empty Halo 2 compatible shader files for each material slot.
            
#### Planned:
            GenerateH3Shaders [JMS File] - Generates empty Halo 3 compatible shader files for each material slot.
            CompressJMS [JMS File] - Takes an input JMS file and removes white-space & comments.
            
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
  <li>Linux (Through Wine)</li>
</ul>

## Build Instructions
To build Sham!, you only need Visual Studio 2019 with .NET 4.0 features installed.

## Contributing
If you would like to contribute, feel free to create a pull request with your feature.
