using System.Text.RegularExpressions;

namespace Art.Web.Server.Extensions
{
    public static class SearchExtensions
    {
        private static Regex SearchPatternAsterisk { get; } = new Regex(@"(?<!\\)\*", RegexOptions.Compiled);

        private static Regex SearchPatternEscapedAsterisk { get; } = new Regex(@"\\\*", RegexOptions.Compiled);

        private static Regex SearchPatternQuestionMark { get; } = new Regex(@"(?<!\\)\?", RegexOptions.Compiled);

        private static Regex SearchPatternEscapedQuestionMark { get; } = new Regex(@"\\\?", RegexOptions.Compiled);

        public static string ConvertToSqlLikePattern(this string input)
        {
            var result = SearchPatternAsterisk.Replace(input, "%");
            result = SearchPatternQuestionMark.Replace(result, "_");
            result = SearchPatternEscapedAsterisk.Replace(result, "*");
            result = SearchPatternEscapedQuestionMark.Replace(result, "?");
            return result;
        }
    }
}