using System.Linq;

namespace Application.Common.Helper
{
    public static class StringHelper
    {
        public static bool NotContainsSpace(this string source)
        {
            if (string.IsNullOrEmpty(source)) return false;

            return !source.Any(char.IsWhiteSpace);
        }
    }
}
