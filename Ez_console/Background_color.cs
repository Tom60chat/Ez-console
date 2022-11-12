using System;
using System.Collections.Generic;
using System.Text;

namespace Ez_console
{
    public class Background_color
    {
        #region Variables
        /// <summary>
        /// Character that will trigger color code parsing
        /// </summary>
        public static char color_prefix = '¤';
        #endregion

        #region Properties
        /// <summary>Default</summary>
        public static string reset => color_prefix + "r";
        /// <summary>Black</summary>
        public static string black => color_prefix + Color.black;
        /// <summary>Dark blue</summary>
        public static string dark_blue => color_prefix + Color.dark_blue;
        /// <summary>Dark green</summary>
        public static string dark_green => color_prefix + Color.dark_green;
        /// <summary>Dark cyan/aqua</summary>
        public static string dark_cyan => color_prefix + Color.dark_cyan;
        /// <summary>Dark red</summary>
        public static string dark_red => color_prefix + Color.dark_red;
        /// <summary>Dark purple</summary>
        public static string dark_magenta => color_prefix + Color.dark_magenta;
        /// <summary>Gold</summary>
        public static string dark_yellow => color_prefix + Color.dark_yellow;
        /// <summary>Gray</summary>
        public static string gray => color_prefix + Color.gray;
        /// <summary>Dark gra</summary>
        public static string dark_gray => color_prefix + Color.dark_gray;
        /// <summary>Blue</summary>
        public static string blue => color_prefix + Color.blue;
        /// <summary>Green</summary>
        public static string green => color_prefix + Color.green;
        /// <summary>Cyan/Aqua</summary>
        public static string cyan => color_prefix + Color.cyan;
        /// <summary>Red</summary>
        public static string red => color_prefix + Color.red;
        /// <summary>Purple</summary>
        public static string magenta => color_prefix + Color.magenta;
        /// <summary>Gold</summary>
        public static string yellow => color_prefix + Color.yellow;
        /// <summary>White</summary>
        public static string white => color_prefix + Color.white;
        #endregion
    }
}
