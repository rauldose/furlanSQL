using System;
using System.Collections.Generic;
using System.Text;

namespace FurlanSql.Engine.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string from, string to)
        {
            return (from.Equals(to, StringComparison.InvariantCultureIgnoreCase));
        }

        public static bool IsEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static IEnumerable<string> SplitAndKeep(this string s, char[] delims)
        {
            int start = 0, index;

            while ((index = s.IndexOfAny(delims, start)) != -1)
            {
                if (index - start > 0)
                    yield return s.Substring(start, index - start);
                yield return s.Substring(index, 1);
                start = index + 1;
            }

            if (start < s.Length)
            {
                yield return s.Substring(start);
            }
        }
    }
}
