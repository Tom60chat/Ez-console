using Ez_console.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Ez_console
{
    public abstract class Console
    {
        #region Variables
        // https://stackoverflow.com/a/1522972
        private static readonly object console_lock = new object();
        private static ConsoleColor? default_foreground_color;
        private static ConsoleColor? default_background_color;
        #endregion

        #region Methods
        /// <summary>
        /// Writes the value to the specifique line
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="line">The line to write on</param>
        /// <param name="number_of_line">The number of line allowed</param>
        /// <param name="word_wraping">If we wrap the word in case the value go out of the screen</param>
        public static void WriteToLine(object value, int line, uint number_of_line = 1, bool word_wraping = true)
        {
            // No window attached
            if (System.Console.LargestWindowWidth + System.Console.LargestWindowHeight == 0)
            {
                System.Console.Write(Utility.Remove_color_code(value));
                return;
            }

            value = value ?? string.Empty;
            int oldLeft = System.Console.CursorLeft;
            int oldTop = System.Console.CursorTop;
            string message = value.ToString();

            // We writing 0 line, so this mean we don't write at all
            if (number_of_line == 0)
                return;

            if (word_wraping)
                message = ToWordWrap(message);

            // If the line is too big we trim it.
            if (message.Length > System.Console.BufferWidth * number_of_line)
                message = message.Substring(0, message.IndexOfNth(Environment.NewLine, number_of_line - 1) - 3) + "...";

            // Clear lines before
            for (int i = 0; i < number_of_line; i++)
                ClearLine(line + i);

            // Write lines
            lock (console_lock)
            {
                System.Console.SetCursorPosition(0, Math.Max(0, line));

                Write(message, false);

                System.Console.SetCursorPosition(oldLeft, oldTop);
            }
        }

        /// <summary>
        ///     Return the specified data, followed by the current line terminator, to the standard output stream, while wrapping lines that would otherwise break words.
        /// </summary>
        /// <param name="paragraph">The value to write.</param>
        /// <param name="tabSize">The value that indicates the column width of tab characters.</param>
        // https://stackoverflow.com/a/33508914
        private static string ToWordWrap(string paragraph, int tabSize = 8)
        {
            if (paragraph == null) return string.Empty;

            // No window attached
            if (System.Console.LargestWindowWidth + System.Console.LargestWindowHeight == 0)
                return paragraph;

            string[] lines = paragraph
                .Replace("\t", new string(' ', tabSize))
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var wrapped_paragraph = new StringBuilder();

            for (int i = 0; i < lines.Length; i++)
            {
                string process = lines[i];
                List<string> wrapped = new List<string>();

                while (process.Length > System.Console.BufferWidth)
                {
                    int wrapAt = process.LastIndexOf(' ', Math.Min(System.Console.BufferWidth - 1, process.Length));
                    if (wrapAt <= 0) break;

                    wrapped.Add(process.Substring(0, wrapAt));
                    process = process.Remove(0, wrapAt + 1);
                }

                foreach (string wrap in wrapped)
                    wrapped_paragraph.AppendLine(wrap);

                if (i == lines.Length - 1)
                    wrapped_paragraph.Append(process);
                else
                    wrapped_paragraph.AppendLine(process);
            }

            return wrapped_paragraph.ToString();
        }

        /// <summary>
        /// Writes the text representation of the specified object, followed by the current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="value">The value to write, or null</param>
        /// <param name="word_wrap"></param>
        public static void WriteLine(object value = null, bool word_wrap = true) =>
            Write((value ?? string.Empty) + Environment.NewLine, word_wrap);

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream.
        /// </summary>
        /// <param name="value">The value to write, or null</param>
        /// <param name="word_wrap"></param>
        public static void Write(object value, bool word_wrap = true)
        {
            if (value == null) return;

            var message = value.ToString();
            int character;

            // No window attached
            if (System.Console.LargestWindowWidth + System.Console.LargestWindowHeight == 0)
            {
                System.Console.Write(Utility.Remove_color_code(value));
                return;
            }

            if (word_wrap)
                message = ToWordWrap(message);

            lock (console_lock)
            {
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
            // No window attached
            if (System.Console.LargestWindowWidth + System.Console.LargestWindowHeight == 0) return;

            // You can't use System.Console with xUnit
            if (Environment.OSVersion.Platform == PlatformID.Win32NT && System.Console.Title.EndsWith("testhost.exe"))
                return;

            int oldLeft = System.Console.CursorLeft;
            int oldTop = System.Console.CursorTop;

            lock (console_lock)
            {
                System.Console.SetCursorPosition(0, Math.Max(0, line));
                System.Console.ResetColor();
                Write(new string(' ', System.Console.BufferWidth)); // https://stackoverflow.com/a/15421600/11873025
                System.Console.SetCursorPosition(oldLeft, oldTop);
            }
        }
        #endregion
    }
}
