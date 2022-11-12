// Rename them watever you want
using ezConsole = Ez_console.Console;
using BC = Ez_console.Background_color;
using FC = Ez_console.Foreground_color;
using C = Ez_console.Color;

// Print some exemples :
ezConsole.WriteLine($"{BC.white}{FC.black}Black");      // Background_color         Foreground_color
ezConsole.WriteLine($"¤{C.black}§{C.blue}Blue");        // ¤ + Color                § + Color
ezConsole.WriteLine($"\u00A4f\u00A7bCyan");             // \u00A4 + Hexadecimal     \u00A7 + Hexadecimal
ezConsole.WriteLine($"¤2§1DarkBlue");                   // ¤ + Hexadecimal          § + Hexadecimal

// Changing the prefix                                     ¤ -> $                   § -> &
FC.color_prefix = '&';
BC.color_prefix = '$';
ezConsole.WriteLine();

ezConsole.WriteLine($"$1&2DarkGreen");                  // $ + Hexadecimal          & + Hexadecimal

ezConsole.WriteToLine($"Hello {FC.blue}World{FC.red}!", 5);
ezConsole.WriteToLine($"This line is very long like {BC.red}very very {FC.yellow}long{BC.reset}{FC.reset} that is going to be ouside the window and the line below are going to be eaten! (or not)", 7, 2);

ezConsole.WriteLine();
ezConsole.WriteLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");


// End
Console.SetCursorPosition(0, Console.WindowHeight - 6);
Console.ResetColor();