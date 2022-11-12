using System.IO;
using System.Text;

namespace Ez_console
{
    public class Utility
    {
        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream.
        /// </summary>
        /// <param name="value">The value to write, or null</param>
        public static string Remove_color_code(object value)
        {
            if (value == null) return string.Empty;

            var message = value.ToString();
            int character;
            var string_builder = new StringBuilder();

            using (StringReader reader = new StringReader(message))
            {
                while ((character = reader.Read()) > 0)
                {
                    if (character == Background_color.color_prefix)
                        character = reader.Read();
                    else if (character == Foreground_color.color_prefix)
                        character = reader.Read();
                    else
                        string_builder.Append((char)character);
                }
            };

            return string_builder.ToString();
        }
    }
}
