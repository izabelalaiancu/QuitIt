using System.Linq;
using System.Text.RegularExpressions;

namespace WebApi.Helpers
{
    public static class ExtensionMethods
    {
        public static bool CheckValidPassword(this string password)
        {
            var containsUpper = password.Any(x => char.IsUpper(x));
            var containsDigit = password.Any(x => char.IsNumber(x));
            var containsNonAlphanumeric = Regex.Replace(password, @"[a-zA-Z0-9]", "").Length != 0;

            return containsNonAlphanumeric && containsDigit && containsUpper;
        }

        public static bool CheckValidEmail(this string email)
        {
            return !(Regex.Replace(email.Replace('_', 'a').Replace('.', 'a').Replace('@', 'a'), @"[a-zA-Z0-9]", "").Length != 0);
        }
    }
}
