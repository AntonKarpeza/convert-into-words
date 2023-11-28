using ConvertIntoWords.Data.NumberValues.Interfaces;

namespace ConvertIntoWords.Data.NumberValues
{
    public class EnNumberValues: IEnNumberValues
    {
        private static readonly Dictionary<int, string> numbers = new ()
            {
                { 0, "zero" },
                { 1, "one" },
                { 2, "two" },
                { 3, "three" },
                { 4, "four" },
                { 5, "five" },
                { 6, "six" },
                { 7, "seven" },
                { 8, "eight" },
                { 9, "nine" },
                { 10, "ten" },
                { 11, "eleven" },
                { 12, "twelve" },
                { 13, "thirteen" },
                { 14, "fourteen" },
                { 15, "fifteen" },
                { 16, "sixteen" },
                { 17, "seventeen" },
                { 18, "eighteen" },
                { 19, "nineteen" },
                { 20, "twenty" },
                { 30, "thirty" },
                { 40, "forty" },
                { 50, "fifty" },
                { 60, "sixty" },
                { 70, "seventy" },
                { 80, "eighty" },
                { 90, "ninety" }
            };

        public string? GetNumberName(int number)
        {
            if (numbers.TryGetValue(number, out var outputResult))
            {
                if (outputResult == null)
                {
                    throw new ArgumentNullException("The result is null.");
                }

                return outputResult;
            }

            throw new ArgumentNullException("Number not found in the dictionary.");
        }
    }
}
