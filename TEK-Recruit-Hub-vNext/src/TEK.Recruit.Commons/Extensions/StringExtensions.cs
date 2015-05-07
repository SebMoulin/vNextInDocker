namespace TEK.Recruit.Commons.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveAllSpacesAnLower(this string textToClean)
        {
            return textToClean.ToLowerInvariant().Replace(" ", string.Empty);
        }
    }
}
