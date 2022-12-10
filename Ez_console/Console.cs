using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Ez_console
{
    public abstract class Console
    {
        // https://stackoverflow.com/a/1522972
        private static readonly object console_lock = new object();
        private static ConsoleColor? default_foreground_color;
        private static ConsoleColor? default_background_color;

        /// <summary>
        /// Writes the value to the specifique line
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="line">The line to write on</param>
        /// <param name="number_of_line">The number of line allowed</param>
        /// <param name="word_wraping">If we wrap the word in case the value go out of the screen</param>
        public static void WriteToLine(object value, int line, int number_of_line = 1, bool word_wraping = true)
        {
            value = value ?? string.Empty;
            int oldLeft = System.Console.CursorLeft;
            int oldTop = System.Console.CursorTop;
            string message = value.ToString();

            // If the line is too big we trim it.
            if (message.Length > System.Console.BufferWidth * number_of_line)
                message = message.Substring(0, System.Console.BufferWidth - 3) + "...";

            // Maybe it's not a good idea
            /*// If we go further thant the previous used line
            if (line + number_of_line > oldTop)
                oldTop = line + number_of_line;*/

            // Clear lines before
            for (int i = 0; i < number_of_line; i++)
                ClearLine(line + i);

            // Write lines
            lock (console_lock)
            {
                System.Console.SetCursorPosition(0, line);

                Write(message, word_wraping);

                System.Console.SetCursorPosition(oldLeft, oldTop);
            }
        }

        /*private static void WriteLineWordWrap(string paragraph, int tabSize = 8) =>
            WriteWordWrap((paragraph ?? string.Empty) + Environment.NewLine);*/

        /// <summary>
        ///     Writes the specified data, followed by the current line terminator, to the standard output stream, while wrapping lines that would otherwise break words.
        /// </summary>
        /// <param name="paragraph">The value to write.</param>
        /// <param name="tabSize">The value that indicates the column width of tab characters.</param>
        // https://stackoverflow.com/a/33508914
        private static void WriteWordWrap(string paragraph, int tabSize = 8)
        {
            if (paragraph == null) return;

            string[] lines = paragraph
                .Replace("\t", new string(' ', tabSize))
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                string process = lines[i];
                List<string> wrapped = new List<string>();

                while (process.Length > System.Console.WindowWidth)
                {
                    int wrapAt = process.LastIndexOf(' ', Math.Min(System.Console.WindowWidth - 1, process.Length));
                    if (wrapAt <= 0) break;

                    wrapped.Add(process.Substring(0, wrapAt));
                    process = process.Remove(0, wrapAt + 1);
                }

                foreach (string wrap in wrapped)
                    WriteLine(wrap, false);

                if (i == lines.Length - 1)
                    Write(process, false);
                else
                    WriteLine(process, false);
            }
        }

        public static void WriteLine() =>
            Write(Environment.NewLine, false);
        public static void WriteLine(object value, bool word_wrap = true) =>
            Write((value ?? string.Empty) + Environment.NewLine, word_wrap);

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream.
        /// </summary>
        /// <param name="value">The value to write, or null</param>
        public static void Write(object value, bool word_wrap = true)
        {
            if (value == null) return;

            if (word_wrap)
            {
                WriteWordWrap(value.ToString());
                return;
            }

            lock (console_lock)
            {
                var message = value.ToString();
                int character;

                using (StringReader reader = new StringReader(message))
                {
                    while ((character = reader.Read()) > 0)
                    {
                        if (character == Background_color.color_prefix)
                        {
                            if (!default_background_color.HasValue)
                                default_background_color = System.Console.BackgroundColor;

                            character = reader.Read();
                            if (character == 'r' && default_background_color.HasValue)
                                System.Console.BackgroundColor = default_background_color.Value;
                            if (int.TryParse(((char)character).ToString(), NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out var backgound_color))
                                System.Console.BackgroundColor = (ConsoleColor)backgound_color;
                        }
                        else if (character == Foreground_color.color_prefix)
                        {
                            if (!default_foreground_color.HasValue)
                                default_foreground_color = System.Console.ForegroundColor;

                            character = reader.Read();
                            if (character == 'r' && default_foreground_color.HasValue)
                                System.Console.ForegroundColor = default_foreground_color.Value;
                            if (int.TryParse(((char)character).ToString(), NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out var foreground_color))
                                System.Console.ForegroundColor = (ConsoleColor)foreground_color;
                        }
                        else
                            System.Console.Write((char)character);
                    }
                };

                // Avoid some issues
                System.Console.ResetColor();
            }
        }


        /// <summary>
        /// Clear the specified line
        /// </summary>
        /// <param name="top">Line</param>
        public static void ClearLine(int line)
        {
            // You can't use System.Console with xUnit
            if (Environment.OSVersion.Platform == PlatformID.Win32NT && System.Console.Title.EndsWith("testhost.exe"))
                return;

            int oldLeft = System.Console.CursorLeft;
            int oldTop = System.Console.CursorTop;

            lock (console_lock)
            {
                System.Console.SetCursorPosition(0, line);
                System.Console.ResetColor();
                Write(new string(' ', System.Console.BufferWidth)); // https://stackoverflow.com/a/15421600/11873025
                System.Console.SetCursorPosition(oldLeft, oldTop);
            }
        }
    }
}
