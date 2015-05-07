using System.Globalization;

namespace TEK.Recruit.Commons.Extensions
{
    public static class EnumExtensions
    {
        public static string NumericValue(this GitLabLevelAccess levelAccess)
        {
            return ((int)levelAccess).ToString(CultureInfo.InvariantCulture);
        }

        public static string NumericValue(this GitLabProjectVisibilityLevel visibilityLevel)
        {
            return ((int)visibilityLevel).ToString(CultureInfo.InvariantCulture);
        }
    }
}
