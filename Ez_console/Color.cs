using System;

namespace Ez_console
{
    public class Color
    {

        #region Properties
        /// <summary>Default</summary>
        public static string reset => "r";
        /// <summary>Black</summary>
        public static string black => console_color_to_hexadecimal(ConsoleColor.Black);
        /// <summary>Dark blue</summary>
        public static string dark_blue => console_color_to_hexadecimal(ConsoleColor.DarkBlue);
        /// <summary>Dark green</summary>
        public static string dark_green => console_color_to_hexadecimal(ConsoleColor.DarkGreen);
        /// <summary>Dark cyan/aqua</summary>
        public static string dark_cyan => console_color_to_hexadecimal(ConsoleColor.DarkCyan);
        /// <summary>Dark red</summary>
        public static string dark_red => console_color_to_hexadecimal(ConsoleColor.DarkRed);
        /// <summary>Dark purple</summary>
        public static string dark_magenta => console_color_to_hexadecimal(ConsoleColor.DarkMagenta);
        /// <summary>Gold</summary>
        public static string dark_yellow => console_color_to_hexadecimal(ConsoleColor.DarkYellow);
        /// <summary>Gray</summary>
        public static string gray => console_color_to_hexadecimal(ConsoleColor.Gray);
        /// <summary>Dark gra</summary>
        public static string dark_gray => console_color_to_hexadecimal(ConsoleColor.DarkGray);
        /// <summary>Blue</summary>
        public static string blue => console_color_to_hexadecimal(ConsoleColor.Blue);
        /// <summary>Green</summary>
        public static string green => console_color_to_hexadecimal(ConsoleColor.Green);
        /// <summary>Cyan/Aqua</summary>
        public static string cyan => console_color_to_hexadecimal(ConsoleColor.Cyan);
        /// <summary>Red</summary>
        public static string red => console_color_to_hexadecimal(ConsoleColor.Red);
        /// <summary>Purple</summary>
        public static string magenta => console_color_to_hexadecimal(ConsoleColor.Magenta);
        /// <summary>Gold</summary>
        public static string yellow => console_color_to_hexadecimal(ConsoleColor.Yellow);
        /// <summary>White</summary>
        public static string white => console_color_to_hexadecimal(ConsoleColor.White);
        #endregion

        #region Methods
        public static string console_color_to_hexadecimal(ConsoleColor console_color) =>
            Convert.ToString((int)console_color, 16);

        public static string get_color(char color_prefix, ConsoleColor color_code) =>
            color_prefix + console_color_to_hexadecimal(color_code);
        #endregion
    }
}
