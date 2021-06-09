using System;

namespace Ects.Web.Api.Extensions
{
    public static class LevenshteinExtensions
    {
        /// <summary>
        /// Находим редакционное расстояние и делим его на длинну большего слова.
        /// </summary>
        /// <param name="firstWord"></param>
        /// <param name="secondWord"></param>
        /// <returns>Похожесть заданий.</returns>
        public static double LevenshteinSimilarity(this string firstWord, string secondWord)
        {
            var n = firstWord.Length + 1;
            var m = secondWord.Length + 1;
            var matrixD = new int[n, m];

            const int deletionCost = 1;
            const int insertionCost = 1;

            for (var i = 0; i < n; i++) matrixD[i, 0] = i;

            for (var j = 0; j < m; j++) matrixD[0, j] = j;

            for (var i = 1; i < n; i++)
            for (var j = 1; j < m; j++)
            {
                var substitutionCost = firstWord[i - 1] == secondWord[j - 1] ? 0 : 1;

                matrixD[i, j] = Math.Min(
                    Math.Min(
                        matrixD[i - 1, j] + deletionCost,
                        matrixD[i, j - 1] + insertionCost),
                    matrixD[i - 1, j - 1] + substitutionCost);
            }

            return (double) matrixD[n - 1, m - 1] / Math.Max(firstWord.Length, secondWord.Length);
        }
    }
}