# Ez console

Add colors and easily manage the console lines.

## Download:

[Last release](https://github.com/Tom60chat/Ez-console/releases)  
[NuGet Package](https://www.nuget.org/packages/Ez_console/1.0.1)

## How to use it?

```csharp
// Rename them watever you want
using ezConsole = Ez_console.Console;
using BC = Ez_console.Background_color; // Background color
using FC = Ez_console.Foreground_color; // Foreground color


// Write Hello in black with a white background
ezConsole.WriteLine($"{BC.white}{FC.black}Hello!");               

// Write to the line 7 with the maximum of 2 lines allowed and with Word Wrap enabled
ezConsole.WriteToLine($"This line is very long like {BC.red}very very {FC.yellow}long{BC.reset}{FC.reset} that is going to be ouside the window and the line below are going to be eaten! (or not)", 7, 2);

// Write a line with Word Wrap enabled
ezConsole.WriteLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
```


Under The Unlicense license.
