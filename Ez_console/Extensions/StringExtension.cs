namespace Ez_console.Extensions
{
    internal static class StringExtension
    {
        internal static int IndexOfNth(this string str, string value, uint nth = 0)
        {
            int offset = str.IndexOf(value);

            for (int i = 0; i < nth; i++)
            {
                if (offset == -1) return -1;

                offset = str.IndexOf(value, offset + 1);
            }

            return offset;
        }
    }
}
